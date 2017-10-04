using System;
using System.Collections.Generic;
using System.Linq;
using NorthwindWeb.Models.ServerClientCommunication;
using Microsoft.AspNetCore.Mvc;
using NorthwindWeb.Core.Context;
using Microsoft.AspNetCore.Authorization;
using NorthwindWeb.Core.ViewModels.Order;
using NorthwindWeb.ViewModels.Order;

namespace NorthwindWeb.Controllers
{

    /// <summary>
    /// Orders controller. For table Orders.
    /// </summary>
    public class OrderController : Controller
    {
        private NorthwindDatabase db = new NorthwindDatabase(new Microsoft.EntityFrameworkCore.DbContextOptions<NorthwindDatabase>());

        public OrderController(NorthwindDatabase context)
        {
            db = context;
        }
        //private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(OrderController));  //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.
        private static string Search;

        /// <summary>
        /// the list of current Employees orders
        /// </summary>
        /// <param name="orderID">Returns selected Order</param>
        /// <param name="productID">Returns selected Product</param>
        /// <param name="page">Returns the current page</param>
        /// <param name="search">The search string</param>
        /// <param name="currentFilter">Curent search</param>
        /// <returns></returns>
        [Authorize(Roles = "Employees, Managers, Admins")]
        public ActionResult Home(int? orderID, int? productID, int? page, string search, string currentFilter)
        {
            var viewModel = new OrderIndexData();
            string curentUser = User.Identity.Name;
            int employeerId = db.Employees.Where(e => e.FirstName + e.LastName == curentUser).Select(e => e.EmployeeID).FirstOrDefault();

            // test null if in search control
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            Search = search;
            viewModel.Order = Orders(search, employeerId);
            viewModel.Command = BigComand(employeerId);

            viewModel.OrderTen = LastTenOrder(employeerId);
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
            int pageSize;
            try
            {
                pageSize = int.Parse("10");
            }
            catch
            {
                //logger.Error("Exista o eroare in configurare, key pageSize trebuie sa fie un numar");
                pageSize = 10;
            }
            int pageNumber = (page ?? 1);
            viewModel.Page = 1;
            viewModel.Order = viewModel.Order.ToList();


            return View(viewModel);

        }

        /// <summary>
        /// the list of current Employees orders
        /// </summary>
        /// <param name="orderID">Returns selected Order</param>
        /// <param name="productID">Returns selected Product</param>
        /// <param name="page">Returns the current page</param>
        /// <param name="search">The search string</param>
        /// <param name="currentFilter">Curent search</param>
        /// <returns>Return Orders for curent customers</returns>
        [Authorize]
        public ActionResult HomeCustomers(int? orderID, int? productID, int? page, string search, string currentFilter)
        {
            var viewModel = new OrderIndexData();
            string curentUser = User.Identity.Name;

            // test null if in search control
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            Search = search;
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
            int pageSize;
            try
            {
                pageSize = int.Parse("10");
            }
            catch
            {
                //logger.Error("Exista o eroare in configurare, key pageSize trebuie sa fie un numar");
                pageSize = 10;
            }
            int pageNumber = (page ?? 1);
            viewModel.Page = 1;
            viewModel.Order = viewModel.Order.ToList();


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
        [Authorize(Roles = "Managers, Admins")]
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
            Search = search;
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
            int pageSize;
            try
            {
                pageSize = int.Parse("10");
            }
            catch
            {
                //logger.Error("Exista o eroare in configurare, key pageSize trebuie sa fie un numar");
                pageSize = 10;
            }
            int pageNumber = (page ?? 1);
            viewModel.Page = 1;
            viewModel.Order = viewModel.Order.ToList();
            return View(viewModel);

        }

        /// <summary>
        /// Return a list of users to complete table
        /// </summary>
        /// <param name="draw">Draw order. Client send a draw id in request to keep track of asyncron response</param>
        /// <param name="start">Start from this item</param>
        /// <param name="length">Take a list with "lenght" (if exists) objects inside.</param>
        /// <returns>Returns json for datatable with users</returns>        
#warning "sortColumn and sortDirection will not work";
        public JsonResult JsonTableFill(int draw, int start, int length, [FromQuery]string sortColumn = "-1", [FromQuery]string sortDirection = "asc")
        {
            const int TOTAL_ROWS = 999;

            //int sortColumn = -1;
            //string sortDirection = "asc";

            if (length == -1)
            {
                length = TOTAL_ROWS;
            }

            string curentUser = User.Identity.Name;
            int employeerId = db.Employees.Where(e => e.FirstName + e.LastName == curentUser).Select(e => e.EmployeeID).FirstOrDefault();

            List<OrderInfo> orders = Orders(Search, employeerId);



            //order list
            switch (int.Parse(sortColumn))
            {
                case -1: //sort by first column
                    goto OrderID;
                case 0: //OrderID column
                    OrderID:
                    if (sortDirection == "asc")
                    {
                        orders = orders.OrderBy(x => x.OrderID).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(x => x.OrderID).ToList();
                    }
                    break;
                case 1: //OrderDate column
                    if (sortDirection == "asc")
                    {
                        orders = orders.OrderBy(x => x.OrderDate).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(x => x.OrderDate).ToList();
                    }
                    break;
                case 2: // CompanyName column
                    if (sortDirection == "asc")
                    {
                        orders = orders.OrderBy(x => x.CompanyName).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(x => x.CompanyName).ToList();
                    }
                    break;
                case 3:// ShipperName column
                    if (sortDirection == "asc")
                    {
                        orders = orders.OrderBy(x => x.ShipperName).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(x => x.ShipperName).ToList();
                    }
                    break;

            }
            //object that will be sent to client
            JsonDataTableOrderList dataTableData = new JsonDataTableOrderList()
            {
                draw = draw,
                recordsTotal = db.Orders.Where(o => o.EmployeeID == employeerId).Count(),
                data = new List<OrderInfo>(),
                recordsFiltered = orders.Count(), //need to be below data(ref recordsFiltered)
                //aLengthMenu = length,
            };
            foreach (var itemOderInfo in orders.Skip(start).Take(length))
            {
                OrderInfo orderInfo = new OrderInfo
                {
                    OrderID = itemOderInfo.OrderID,
                    OrderDate = itemOderInfo.OrderDate,
                    CompanyName = itemOderInfo.CompanyName,
                    ShipperName = itemOderInfo.ShipperName
                };
                dataTableData.data.Add(orderInfo);
            }
            return Json(dataTableData);
        }
        /// <summary>
        /// Return a list of users to complete table
        /// </summary>
        /// <param name="draw">Draw order. Client send a draw id in request to keep track of asyncron response</param>
        /// <param name="start">Start from this item</param>
        /// <param name="length">Take a list with "lenght" (if exists) objects inside.</param>
        /// <returns>Returns json for datatable with users</returns>        
#warning "sortColumn and sortDirection will not work";
        public JsonResult JsonTableAdminFill(int draw, int start, int length, [FromQuery]string search = "", [FromQuery]string sortColumn = "-1", [FromQuery]string sortDirection = "asc")
        {
            const int TOTAL_ROWS = 999;

            if (length == -1)
            {
                length = TOTAL_ROWS;
            }

            List<OrderInfo> orders = Orders(Search);

            //order list
            switch (int.Parse(sortColumn))
            {
                case -1: //sort by first column
                    goto OrderID;
                case 0: //OrderID column
                    OrderID:
                    if (sortDirection == "asc")
                    {
                        orders = orders.OrderBy(x => x.OrderID).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(x => x.OrderID).ToList();
                    }
                    break;
                case 1: //OrderDate column
                    if (sortDirection == "asc")
                    {
                        orders = orders.OrderBy(x => x.OrderDate).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(x => x.OrderDate).ToList();
                    }
                    break;
                case 2: // CompanyName column
                    if (sortDirection == "asc")
                    {
                        orders = orders.OrderBy(x => x.CompanyName).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(x => x.CompanyName).ToList();
                    }
                    break;
                case 3:// ShipperName column
                    if (sortDirection == "asc")
                    {
                        orders = orders.OrderBy(x => x.ShipperName).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(x => x.ShipperName).ToList();
                    }
                    break;

            }
            //objet that whill be sent to client
            JsonDataTableOrderList dataTableData = new JsonDataTableOrderList()
            {
                draw = draw,
                recordsTotal = db.Orders.Count(),
                data = new List<OrderInfo>(),
                recordsFiltered = orders.Count(), //need to be below data(ref recordsFiltered)
                //aLengthMenu = length,
            };
            foreach (var itemOderInfo in orders.Skip(start).Take(length))
            {
                OrderInfo orderInfo = new OrderInfo
                {
                    OrderID = itemOderInfo.OrderID,
                    OrderDate = itemOderInfo.OrderDate,
                    CompanyName = itemOderInfo.CompanyName,
                    ShipperName = itemOderInfo.ShipperName
                };
                dataTableData.data.Add(orderInfo);
            }
            //return Json(dataTableData, JsonRequestBehavior.AllowGet);
            return Json(dataTableData);
        }
        /// <summary>
        /// Return a list of users to complete table
        /// </summary>
        /// <param name="draw">Draw order. Client send a draw id in request to keep track of asyncron response</param>
        /// <param name="start">Start from this item</param>
        /// <param name="length">Take a list with "lenght" (if exists) objects inside.</param>
        /// <returns>Returns json for datatable with users</returns>        
#warning "sortColumn and sortDirection will not work";
        public JsonResult JsonTableCustomerFill(int draw, int start, int length, [FromQuery]string sortColumn = "-1", [FromQuery]string sortDirection = "asc")
        {
            const int TOTAL_ROWS = 999;

            if (length == -1)
            {
                length = TOTAL_ROWS;
            }

            string curentUser = User.Identity.Name;
            List<OrderInfo> orders = Orders(Search, curentUser);

            //order list
            switch (int.Parse(sortColumn))
            {
                case -1: //sort by first column
                    goto OrderID;
                case 0: //OrderID column
                    OrderID:
                    if (sortDirection == "asc")
                    {
                        orders = orders.OrderBy(x => x.OrderID).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(x => x.OrderID).ToList();
                    }
                    break;
                case 1: //OrderDate column
                    if (sortDirection == "asc")
                    {
                        orders = orders.OrderBy(x => x.OrderDate).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(x => x.OrderDate).ToList();
                    }
                    break;
                case 2: // CompanyName column
                    if (sortDirection == "asc")
                    {
                        orders = orders.OrderBy(x => x.CompanyName).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(x => x.CompanyName).ToList();
                    }
                    break;
                case 3:// ShipperName column
                    if (sortDirection == "asc")
                    {
                        orders = orders.OrderBy(x => x.ShipperName).ToList();
                    }
                    else
                    {
                        orders = orders.OrderByDescending(x => x.ShipperName).ToList();
                    }
                    break;

            }
            //objet that whill be sent to client
            JsonDataTableOrderList dataTableData = new JsonDataTableOrderList()
            {
                draw = draw,
                recordsTotal = db.Customers.Where(c => c.ContactName == curentUser).Count(),
                data = new List<OrderInfo>(),
                recordsFiltered = orders.Count(), //need to be below data(ref recordsFiltered)
                //aLengthMenu = length,
            };
            foreach (var itemOderInfo in orders.Skip(start).Take(length))
            {
                OrderInfo orderInfo = new OrderInfo
                {
                    OrderID = itemOderInfo.OrderID,
                    OrderDate = itemOderInfo.OrderDate,
                    CompanyName = itemOderInfo.CompanyName,
                    ShipperName = itemOderInfo.ShipperName
                };
                dataTableData.data.Add(orderInfo);
            }
            //return Json(dataTableData, JsonRequestBehavior.AllowGet);
            return Json(dataTableData);
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
                OrderTen lastTenOrdeElementr = new OrderTen
                {
                    OrderID = itemOrderTen.OrderID,
                    Cost = Math.Round(itemOrderTen.Cost, 2)
                };
                lastTenOrderData.Add(lastTenOrdeElementr);

            }
            return lastTenOrderData;
        }

        //last ten of current user orders  
        private List<OrderTen> LastTenOrder(string user)
        {
            var orderTen = (from o in db.Orders
                            join od in db.Order_Details on o.OrderID equals od.OrderID
                            join c in db.Customers on o.CustomerID equals c.CustomerID
                            where (c.ContactName == user)
                            group od by o.OrderID into x
                            select new { OrderID = x.Key, Cost = x.Sum(o => o.UnitPrice * o.Quantity) })
                              .OrderByDescending(x => x.Cost)
                              .Take(10);
            ;
            List<OrderTen> lastTenOrderData = new List<OrderTen>();
            //IQueryable->list
            foreach (var itemOrderTen in orderTen)
            {
                OrderTen lastTenOrdeElementr = new OrderTen
                {
                    OrderID = itemOrderTen.OrderID,
                    Cost = Math.Round(itemOrderTen.Cost, 2)
                };
                lastTenOrderData.Add(lastTenOrdeElementr);

            }
            return lastTenOrderData;
        }

        //last ten of current Employeers orders  
        private List<OrderTen> LastTenOrder(int userId)
        {
            var orderTen = (from o in db.Orders
                            join od in db.Order_Details on o.OrderID equals od.OrderID
                            join e in db.Employees on o.EmployeeID equals e.EmployeeID
                            where (e.EmployeeID == userId)
                            group od by o.OrderID into x
                            select new { OrderID = x.Key, Cost = x.Sum(o => o.UnitPrice * o.Quantity) })
                              .OrderByDescending(x => x.Cost)
                              .Take(10);
            ;
            List<OrderTen> lastTenOrderData = new List<OrderTen>();
            //IQueryable->list
            foreach (var itemOrderTen in orderTen)
            {
                OrderTen lastTenOrdeElementr = new OrderTen
                {
                    OrderID = itemOrderTen.OrderID,
                    Cost = Math.Round(itemOrderTen.Cost, 2)
                };
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
                        OrderInfo x = new OrderInfo
                        {
                            OrderID = item.OrderID
                        };
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
                    OrderInfo x = new OrderInfo
                    {
                        OrderID = item.OrderID
                    };
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
                         join s in db.Shippers on o.ShipVia equals s.ShipperID
                         join c in db.Customers on o.CustomerID equals c.CustomerID
                         where (c.ContactName == user)
                         select new { o.OrderID, o.OrderDate, c.CompanyName, ShipperName = s.CompanyName })
                       .OrderBy(i => i.OrderID);
            //Filter orders if a text has been entered
            if (!String.IsNullOrEmpty(search))
            {//IQueryable->list
                foreach (var item in order)
                {//filter the results
                    if ((Convert.ToString(item.OrderID).Contains(search)) || (item.ShipperName.ToLower().Contains(search.ToLower())))
                    {
                        OrderInfo x = new OrderInfo
                        {
                            OrderID = item.OrderID
                        };
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
                    OrderInfo x = new OrderInfo
                    {
                        OrderID = item.OrderID
                    };
                    DateTime t = Convert.ToDateTime(item.OrderDate);
                    x.OrderDate = t.Day.ToString() + "." + t.Month + "." + t.Year;
                    x.CompanyName = item.CompanyName;
                    x.ShipperName = item.ShipperName;
                    orders.Add(x);

                }
            }
            return orders;
        }
        //current Employeers orders 
        private List<OrderInfo> Orders(string search, int userId)
        {
            List<OrderInfo> orders = new List<OrderInfo>();
            var order = (from o in db.Orders
                         join c in db.Customers on o.CustomerID equals c.CustomerID
                         join s in db.Shippers on o.ShipVia equals s.ShipperID
                         join e in db.Employees on o.EmployeeID equals e.EmployeeID
                         where (e.EmployeeID == userId)
                         select new { o.OrderID, o.OrderDate, c.CompanyName, ShipperName = s.CompanyName })
                       .OrderBy(i => i.OrderID);
            //Filter orders if a text has been entered
            if (!String.IsNullOrEmpty(search))
            {//IQueryable->list
                foreach (var item in order)
                {//filter the results
                    if ((Convert.ToString(item.OrderID).Contains(search)) || (item.ShipperName.ToLower().Contains(search.ToLower())))
                    {
                        OrderInfo x = new OrderInfo
                        {
                            OrderID = item.OrderID
                        };
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
                    OrderInfo x = new OrderInfo
                    {
                        OrderID = item.OrderID
                    };
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
                bigOrder.NumberOfProduct = itemOrder.max;
            }
            return bigOrder;
        }
        //big order of current user orders  
        private BigOrder BigComand(string user)
        {
            var order = (from o in db.Orders
                         join od in db.Order_Details on o.OrderID equals od.OrderID
                         join c in db.Customers on o.CustomerID equals c.CustomerID
                         where (c.ContactName == user)
                         group od by o.OrderID into x
                         select new { OrderID = x.Key, max = x.Sum(o => o.Quantity) })
                                  .OrderByDescending(x => x.max)
                                  .Take(1);
            BigOrder bigOrder = new BigOrder();
            //IQueryable->BigOrder
            foreach (var itemOrder in order)
            {
                bigOrder.OrderID = itemOrder.OrderID;
                bigOrder.NumberOfProduct = itemOrder.max;
            }
            return bigOrder;
        }
        //big order of current Employeers orders  
        private BigOrder BigComand(int userId)
        {
            var order = (from o in db.Orders
                         join od in db.Order_Details on o.OrderID equals od.OrderID
                         join e in db.Employees on o.EmployeeID equals e.EmployeeID
                         where (e.EmployeeID == userId)
                         group od by o.OrderID into x
                         select new { OrderID = x.Key, max = x.Sum(o => o.Quantity) })
                                  .OrderByDescending(x => x.max)
                                  .Take(1);
            BigOrder bigOrder = new BigOrder();
            //IQueryable->BigOrder
            foreach (var itemOrder in order)
            {
                bigOrder.OrderID = itemOrder.OrderID;
                bigOrder.NumberOfProduct = itemOrder.max;
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
                OrderProduct orderDetails = new OrderProduct
                {
                    ProductID = itemDetails.ProductID,
                    ProductName = itemDetails.ProductName,
                    UnitPrice = itemDetails.UnitPrice,
                    Quantity = itemDetails.Quantity,
                    Discount = itemDetails.Discount
                };
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
                ProductCategory productCategoryElement = new ProductCategory
                {
                    ProductName = itemDetails.ProductName,
                    CategoryName = itemDetails.CategoryName,
                    UnitsInStock = itemDetails.UnitsInStock
                };

                productCategoryData.Add(productCategoryElement);

            }
            return productCategoryData;
        }


    }
}
