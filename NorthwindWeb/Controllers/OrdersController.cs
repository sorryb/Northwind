using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using NorthwindWeb.Models;
using NorthwindWeb.ViewModels.Orders;
using NorthwindWeb.Models.Interfaces;
using NorthwindWeb.Models.ServerClientCommunication;
using NorthwindWeb.Context;
using NorthwindWeb.Models.ExceptionHandler;
using System;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Orders Controller. For table Orders
    /// </summary>
    [Authorize(Roles = "Admins, Managers")]
    public class OrdersController : Controller, IJsonTableFillServerSide
    {
        private NorthwindDatabase db = new NorthwindDatabase();
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(OrdersController));  //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.


        /// <summary>
        /// Displays a page with all the orders existing in the database.
        /// </summary>
        /// <param name="search">The search look to find something asked</param>
        /// <returns>Orders index view</returns>
        public ActionResult Index(string search = "")
        {
            return View(db.Orders.Include(o => o.Customer).Include(o => o.Employee).Include(o => o.Shipper).Where(o => o.OrderID.ToString().Contains(search)).OrderBy(o => o.OrderID));
        }

        /// <summary>
        /// Displays a page showing all the information about one order and its order-details.
        /// </summary>
        /// <param name="id">The id of the order whose information to show</param>
        /// <returns>Orders details view</returns>
        public async Task<ActionResult> Details(int id)
        {
            OrderDetali viewModel = new OrderDetali();
            //take details of orders
            Orders orders = await db.Orders.FindAsync(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            viewModel.Order = orders;

            //take order-details of orders
            var orderDetail = from od in db.Order_Details
                              where (od.OrderID == id)
                              select new { od.OrderID, od.ProductID, od.Quantity, od.UnitPrice, od.Discount };


            List<DetailsOfOrder> listOfDetails = new List<DetailsOfOrder>();

            //lopp in all order-details
            foreach (var itemInOrderDetail in orderDetail)
            {
                DetailsOfOrder order = new DetailsOfOrder();

                order.OrderID = itemInOrderDetail.OrderID;
                order.ProductID = itemInOrderDetail.ProductID;
                order.Quantity = itemInOrderDetail.Quantity;
                order.UnitPrice = itemInOrderDetail.UnitPrice;
                order.Discount = itemInOrderDetail.Discount;


                listOfDetails.Add(order);

            }
            viewModel.Details = listOfDetails;
            ViewBag.orderid = id;
            return View(viewModel);
        }

        /// <summary>
        /// Returns the view containing the form neccesary for creating a new order.
        /// </summary>
        /// <returns>Create view.</returns>
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CompanyName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName");
            ViewBag.ShipVia = new SelectList(db.Shippers, "ShipperID", "CompanyName");
            return View();
        }

        /// <summary>
        /// Inserts an order into the database table. If it fails, goes back to the form.
        /// </summary>
        /// <param name="orders">The order entity to be inserted</param>
        /// <returns>If successful returns orders index view, else goes back to form.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderID,CustomerID,EmployeeID,OrderDate,RequiredDate,ShippedDate,ShipVia,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CompanyName", orders.CustomerID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", orders.EmployeeID);
            ViewBag.ShipVia = new SelectList(db.Shippers, "ShipperID", "CompanyName", orders.ShipVia);
            return View(orders);
        }



        /// <summary>
        /// Returns the view containing the form necessary for editing an existing order.
        /// </summary>
        /// <param name="id">The id of the order that is going to be edited</param>
        /// <returns>Orders edit view</returns>
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = await db.Orders.FindAsync(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CompanyName", orders.CustomerID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", orders.EmployeeID);
            ViewBag.ShipVia = new SelectList(db.Shippers, "ShipperID", "CompanyName", orders.ShipVia);
            return View(orders);
        }

        /// <summary>
        /// Updates the database 
        /// changing the fields of the order whose id is equal to the id of the provided orders parameter
        /// to those of the parameter.
        /// </summary>
        /// <param name="orders">The changed order.</param>
        /// <returns>Orders index view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderID,CustomerID,EmployeeID,OrderDate,RequiredDate,ShippedDate,ShipVia,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CompanyName", orders.CustomerID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", orders.EmployeeID);
            ViewBag.ShipVia = new SelectList(db.Shippers, "ShipperID", "CompanyName", orders.ShipVia);
            return View(orders);
        }

        /// <summary>
        /// Displays a confirmation page for the following delete.
        /// </summary>
        /// <param name="id">The order that is going to be deleted.</param>
        /// <returns>Delete view</returns>
        public async Task<ActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }
            OrderDetali viewModel = new OrderDetali();
            //take details of orders
            Orders orders = await db.Orders.FindAsync(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            viewModel.Order = orders;

            //take order-details of orders
            var orderDetail = from od in db.Order_Details
                              where (od.OrderID == id)
                              select new { od.OrderID, od.ProductID, od.Quantity, od.UnitPrice, od.Discount };


            List<DetailsOfOrder> listOfDetails = new List<DetailsOfOrder>();

            //lopp in all order-details
            foreach (var itemInOrderDetail in orderDetail)
            {
                DetailsOfOrder order = new DetailsOfOrder();

                order.OrderID = itemInOrderDetail.OrderID;
                order.ProductID = itemInOrderDetail.ProductID;
                order.Quantity = itemInOrderDetail.Quantity;
                order.UnitPrice = itemInOrderDetail.UnitPrice;
                order.Discount = itemInOrderDetail.Discount;


                listOfDetails.Add(order);

            }
            viewModel.Details = listOfDetails;
            ViewBag.orderid = id;

            return View(viewModel);
        }

        /// <summary>
        /// Deletes an order from the database. The order will be deleted along with its details
        /// </summary>
        /// <param name="id">The id of the order that is going to be deleted</param>
        /// <returns>Orders index view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {


            var details = db.Order_Details.Where(x => x.OrderID == id);
            foreach (var orderDet in details)
                db.Order_Details.Remove(orderDet);

            Orders orders = await db.Orders.FindAsync(id);
            db.Orders.Remove(orders);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        /// <summary>
        /// Function used to control the dashboard datatables from the server
        /// </summary>
        /// <param name="draw"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns>A JSON filtered orders list.</returns>
        public JsonResult JsonTableFill(int draw, int start, int length)
        {
            try
            {
                const int totalRows = 999;

                string search = "";
                search = Request.QueryString["search[value]"] ?? "";


                int sortColumn = -1;
                string sortDirection = "asc";
                if (length == -1)
                {
                    length = totalRows;
                }

                // note: we only sort one column at a time
                if (Request.QueryString["order[0][column]"] != null)
                {
                    sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
                }

                if (Request.QueryString["order[0][dir]"] != null)
                {

                    sortDirection = Request.QueryString["order[0][dir]"];
                }

                //list of orders that contain "search"
                var list = db.Orders.Include(o => o.Customer).Include(o => o.Employee).Include(o => o.Shipper).Where(o => o.OrderID.ToString().Contains(search) ||
                                            o.Employee.LastName.Contains(search) ||
                                            o.Shipper.CompanyName.Contains(search) ||
                                            o.ShippedDate.ToString().Contains(search) ||
                                            o.ShipName.Contains(search) ||
                                            o.ShipAddress.Contains(search));

                //order list
                switch (sortColumn)
                {
                    case -1: //sort by first column
                        goto FirstColumn;
                    case 0: //first column
                        FirstColumn:
                        if (sortDirection == "asc")
                        {
                            list = list.OrderBy(x => x.OrderID);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.OrderID);
                        }
                        break;
                    case 1: //second column
                        if (sortDirection == "asc")
                        {
                            list = list.OrderBy(x => x.Employee.LastName);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.Employee.LastName);
                        }
                        break;
                    case 2: // and so on
                        if (sortDirection == "asc")
                        {
                            list = list.OrderBy(x => x.Shipper.CompanyName);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.Shipper.CompanyName);
                        }
                        break;
                    case 3:
                        if (sortDirection == "asc")
                        {
                            list = list.OrderBy(x => x.ShippedDate.ToString());
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.ShippedDate.ToString());
                        }
                        break;
                    case 4:
                        if (sortDirection == "asc")
                        {
                            list = list.OrderBy(x => x.ShipName);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.ShipName);
                        }
                        break;
                    case 5:
                        if (sortDirection == "asc")
                        {
                            list = list.OrderBy(x => x.ShipAddress);
                        }
                        else
                        {
                            list = list.OrderByDescending(x => x.ShipAddress);
                        }
                        break;
                }

                //objet that whill be sent to client
                JsonDataTable dataTableData = new JsonDataTable()
                {
                    draw = draw,
                    recordsTotal = db.Orders.Count(),
                    data = list.Skip(start).Take(length).Select(x => new
                    {
                        ID = x.OrderID,
                        LastName = x.Employee.LastName,
                        CompanyName = x.Shipper.CompanyName,
                        ShippedDate = x.ShippedDate.ToString(),
                        ShipName = x.ShipName,
                        ShipAddress = x.ShipAddress,

                    }),
                    recordsFiltered = list.Count(), //need to be below data(ref recordsFiltered)

                };
                return Json(dataTableData, JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            {
                logger.Error(e.ToString());
                return Json(new JsonDataTable()
                {
                    draw = draw,
                    recordsTotal = db.Orders.Count(),
                    error = "Ceva nu a mers bine",
                    recordsFiltered = 0
                }, JsonRequestBehavior.AllowGet);
            }

           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
