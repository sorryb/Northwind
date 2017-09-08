using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;
using NorthwindWeb.ViewModels.Order;
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace NorthwindWeb.Controllers
{

    /// <summary>
    /// Orders controller. For table Orders.
    /// </summary>
    public class OrderController : Controller
    {
        private NorthwindModel db = new NorthwindModel();

        /// <summary>
        /// the list of current user orders
        /// </summary>
        /// <param name="orderID">Returns selected Order</param>
        /// <param name="productID">Returns selected Product</param>
        /// <param name="page">Returns the current page</param>
        /// <param name="search">The search string</param>
        /// <param name="currentFilter">Curent search</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Home(int? orderID, int? productID, int? page, string search, string currentFilter)
        {
            var viewModel = new OrderIndexData();
            string curentUser = User.Identity.GetUserName();
            // test null if in search control
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            ViewBag.CurrentFilter = search;
            viewModel.Order = Orders(search, curentUser);
            viewModel.Command = BigComand(curentUser);

            viewModel.OrderTen = LastTenOrder(curentUser);
            // test null if orders not selected in page
            if (orderID == 0) { orderID = null; }
            if (orderID != null)
            {
                ViewBag.OrderID = orderID.Value;
                viewModel.Order_Detail = Details(ViewBag.OrderID);


            }
            //test null if OrdersDetails not selected in page
            if (productID == 0) { productID = null; }
            if (productID != null)
            {
                ViewBag.ProductID = productID.Value;

                viewModel.Product = ProductCategory(ViewBag.ProductID);
            }
            //pagination
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            viewModel.Page = viewModel.Order.ToPagedList(pageNumber, pageSize);
            viewModel.Order = viewModel.Order.ToPagedList(pageNumber, pageSize);
            return View(viewModel);

        }

        /// <summary>
        /// list of all orders viewable only by admin
        /// </summary>
        /// <param name="orderID">Returns selected Order</param>
        /// <param name="productID">Returns selected Product</param>
        /// <param name="page">Returns the current page</param>
        /// <param name="search">The search string</param>
        /// <param name="currentFilter">Curent search</param>
        /// <returns></returns>
        [Authorize(Roles="Managers, Admins")]
        
        public ActionResult HomeAdmin(int? orderID, int? productID, int? page, string search, string currentFilter)
        {
            var viewModel = new OrderIndexData();

            // test null if in search control
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            ViewBag.CurrentFilter = search;
            viewModel.Order = Orders(search);
            viewModel.Command = BigComand();

            viewModel.OrderTen = LastTenOrder();
            // test null if orders not selected in page
            if (orderID == 0) { orderID = null; }
            if (orderID != null)
            {
                ViewBag.OrderID = orderID.Value;
                viewModel.Order_Detail = Details(ViewBag.OrderID);


            }
            //test null if OrdersDetails not selected in page
            if (productID == 0) { productID = null; }
            if (productID != null)
            {
                ViewBag.ProductID = productID.Value;

                viewModel.Product = ProductCategory(ViewBag.ProductID);
            }
            //pagination
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            viewModel.Page = viewModel.Order.ToPagedList(pageNumber, pageSize);
            viewModel.Order = viewModel.Order.ToPagedList(pageNumber, pageSize);
            return View(viewModel);

        }
        //last ten of all orders 
        private List<OrderTen> LastTenOrder()
        {
            var orderTen = (from o in db.Orders
                            join od in db.Order_Details on o.OrderID equals od.OrderID
                            group od by o.OrderID into x
                            select new { OrderID = x.Key, Cost = x.Sum(o => o.UnitPrice * o.Quantity) })
                              .OrderByDescending(x => x.Cost)
                              .Take(10);
            ;
            List<OrderTen> lastTenOrderData = new List<OrderTen>();
            //IQueryable->list
            foreach (var itemOrderTen in orderTen)
            {
                OrderTen lastTenOrdeElementr = new OrderTen();

                lastTenOrdeElementr.OrderID = itemOrderTen.OrderID;
                lastTenOrdeElementr.Cost = decimal.Round(itemOrderTen.Cost, 2);
                lastTenOrderData.Add(lastTenOrdeElementr);

            }
            return lastTenOrderData;
        }

        //last ten of current user orders  
        private List<OrderTen> LastTenOrder(string user)
        {
            var orderTen = (from o in db.Orders
                            join od in db.Order_Details on o.OrderID equals od.OrderID
                            join e in db.Employees on o.EmployeeID equals e.EmployeeID
                            where (e.FirstName + e.LastName == user)
                            group od by o.OrderID into x
                            select new { OrderID = x.Key, Cost = x.Sum(o => o.UnitPrice * o.Quantity) })
                              .OrderByDescending(x => x.Cost)
                              .Take(10);
            ;
            List<OrderTen> lastTenOrderData = new List<OrderTen>();
            //IQueryable->list
            foreach (var itemOrderTen in orderTen)
            {
                OrderTen lastTenOrdeElementr = new OrderTen();

                lastTenOrdeElementr.OrderID = itemOrderTen.OrderID;
                lastTenOrdeElementr.Cost = decimal.Round(itemOrderTen.Cost, 2);
                lastTenOrderData.Add(lastTenOrdeElementr);

            }
            return lastTenOrderData;
        }
        //all orders
        private List<OrderInfo> Orders(string search)
        {
            List<OrderInfo> orders = new List<OrderInfo>();
            var order = (from o in db.Orders
                         join c in db.Customers on o.CustomerID equals c.CustomerID
                         join s in db.Shippers on o.ShipVia equals s.ShipperID
                         select new { o.OrderID, o.OrderDate, c.CompanyName, ShipperName = s.CompanyName })
                       .OrderBy(i => i.OrderID);
            //Filter orders if a text has been entered
            if (!String.IsNullOrEmpty(search))
            { //IQueryable->list
                foreach (var item in order)
                {//filter the results
                    if ((Convert.ToString(item.OrderID).Contains(search)) || (item.ShipperName.ToLower().Contains(search.ToLower())))
                    {
                        OrderInfo x = new OrderInfo();

                        x.OrderID = item.OrderID;
                        DateTime t = Convert.ToDateTime(item.OrderDate);
                        x.OrderDate = t.Day.ToString() + "." + t.Month + "." + t.Year;
                        x.CompanyName = item.CompanyName;
                        x.ShipperName = item.ShipperName;
                        orders.Add(x);
                    }
                }
            }
            else
            {
                //IQueryable->list
                foreach (var item in order)
                {
                    OrderInfo x = new OrderInfo();

                    x.OrderID = item.OrderID;
                    DateTime t = Convert.ToDateTime(item.OrderDate);
                    x.OrderDate = t.Day.ToString() + "." + t.Month + "." + t.Year;
                    x.CompanyName = item.CompanyName;
                    x.ShipperName = item.ShipperName;
                    orders.Add(x);

                }
            }
            return orders;
        }
        //current user orders 
        private List<OrderInfo> Orders(string search, string user)
        {
            List<OrderInfo> orders = new List<OrderInfo>();
            var order = (from o in db.Orders
                         join c in db.Customers on o.CustomerID equals c.CustomerID
                         join s in db.Shippers on o.ShipVia equals s.ShipperID
                         join e in db.Employees on o.EmployeeID equals e.EmployeeID
                         where (e.FirstName + e.LastName == user)
                         select new { o.OrderID, o.OrderDate, c.CompanyName, ShipperName = s.CompanyName })
                       .OrderBy(i => i.OrderID);
            //Filter orders if a text has been entered
            if (!String.IsNullOrEmpty(search))
            {//IQueryable->list
                foreach (var item in order)
                {//filter the results
                    if ((Convert.ToString(item.OrderID).Contains(search)) || (item.ShipperName.ToLower().Contains(search.ToLower())))
                    {
                        OrderInfo x = new OrderInfo();

                        x.OrderID = item.OrderID;
                        DateTime t = Convert.ToDateTime(item.OrderDate);
                        x.OrderDate = t.Day.ToString() + "." + t.Month + "." + t.Year;
                        x.CompanyName = item.CompanyName;
                        x.ShipperName = item.ShipperName;
                        orders.Add(x);
                    }
                }
            }
            else
            {
                //IQueryable->list
                foreach (var item in order)
                {
                    OrderInfo x = new OrderInfo();

                    x.OrderID = item.OrderID;
                    DateTime t = Convert.ToDateTime(item.OrderDate);
                    x.OrderDate = t.Day.ToString() + "." + t.Month + "." + t.Year;
                    x.CompanyName = item.CompanyName;
                    x.ShipperName = item.ShipperName;
                    orders.Add(x);

                }
            }
            return orders;
        }
        //big order of all orders 
        private BigOrder BigComand()
        {
            var order = (from o in db.Orders
                         join od in db.Order_Details on o.OrderID equals od.OrderID
                         group od by o.OrderID into x
                         select new { OrderID = x.Key, max = x.Sum(o => o.Quantity) })
                                  .OrderByDescending(x => x.max)
                                  .Take(1);
            BigOrder bigOrder = new BigOrder();
            //IQueryable->BigOrder
            foreach (var itemOrder in order)
            {
                bigOrder.OrderID = itemOrder.OrderID;
                bigOrder.Produse = itemOrder.max;
            }
            return bigOrder;
        }
        //big order of current user orders  
        private BigOrder BigComand(string user)
        {
            var order = (from o in db.Orders
                         join od in db.Order_Details on o.OrderID equals od.OrderID
                         join e in db.Employees on o.EmployeeID equals e.EmployeeID
                         where (e.FirstName + e.LastName == user)
                         group od by o.OrderID into x
                         select new { OrderID = x.Key, max = x.Sum(o => o.Quantity) })
                                  .OrderByDescending(x => x.max)
                                  .Take(1);
            BigOrder bigOrder = new BigOrder();
            //IQueryable->BigOrder
            foreach (var itemOrder in order)
            {
                bigOrder.OrderID = itemOrder.OrderID;
                bigOrder.Produse = itemOrder.max;
            }
            return bigOrder;
        }

        private List<OrderProduct> Details(int orderID)
        {
            var details = (from o in db.Order_Details
                          .Where(x => x.OrderID == orderID)
                           join p in db.Products on o.ProductID equals p.ProductID
                           select new { p.ProductName, o.UnitPrice, o.Quantity, o.Discount, o.ProductID })

            ;
            List<OrderProduct> detailsData = new List<OrderProduct>();
            //IQueryable->list
            foreach (var itemDetails in details)
            {
                OrderProduct orderDetails = new OrderProduct();
                orderDetails.ProductID = itemDetails.ProductID;
                orderDetails.ProductName = itemDetails.ProductName;
                orderDetails.UnitPrice = itemDetails.UnitPrice;
                orderDetails.Quantity = itemDetails.Quantity;
                orderDetails.Discount = itemDetails.Discount;
                detailsData.Add(orderDetails);

            }
            return detailsData;
        }

        private List<ProductCategory> ProductCategory(int id)
        {
            var details = (from p in db.Products
                          .Where(x => x.ProductID == id)
                           join c in db.Categories on p.CategoryID equals c.CategoryID
                           select new { p.ProductName, c.CategoryName, p.UnitsInStock })

            ;
            List<ProductCategory> productCategoryData = new List<ProductCategory>();
            //IQueryable->list
            foreach (var itemDetails in details)
            {
                ProductCategory productCategoryElement = new ProductCategory();
                productCategoryElement.ProductName = itemDetails.ProductName;
                productCategoryElement.CategoryName = itemDetails.CategoryName;
                productCategoryElement.UnitsInStock = itemDetails.UnitsInStock;

                productCategoryData.Add(productCategoryElement);

            }
            return productCategoryData;
        }

    }
}
