using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;
using PagedList;
using NorthwindWeb.ViewModels.Orders;
using NorthwindWeb.Models.Interfaces;




namespace NorthwindWeb.Controllers
{
    [Authorize]
    /// <summary>
    /// Orders Controller. For table Orders
    /// </summary>
    public class OrdersController : Controller, IJsonTableFill
    {
        private NorthwindModel db = new NorthwindModel();

        // GET: Orders
        public ActionResult Index(string search = "")
        {
            return View(db.Orders.Include(o => o.Customer).Include(o => o.Employee).Include(o => o.Shipper).Where(o=>o.OrderID.ToString().Contains(search)).OrderBy(o=>o.OrderID));
            
          
        }
            
        // GET: Orders/Details
        public async Task<ActionResult> Details(int id)
        {
            OrderDetali viewModel = new OrderDetali();
            //take details of orders
            Orders orders = await db.Orders.FindAsync(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            viewModel.order = orders;

            //take order-details of orders
            var ordet = from od in db.Order_Details
                        where (od.OrderID == id)
                        select new { od.OrderID,od.ProductID,od.Quantity,od.UnitPrice, od.Discount };


            List<DetailsOfOrder> list = new List<DetailsOfOrder>();

            //lopp in all order-details
            foreach (var item in ordet)
            {
                DetailsOfOrder x = new DetailsOfOrder();

                x.OrderID = item.OrderID;
                x.ProductID = item.ProductID;
                x.Quantity = item.Quantity;
                x.UnitPrice = item.UnitPrice;
                x.Discount = item.Discount;


                list.Add(x);

            }
            viewModel.details = list;
            ViewBag.orderid = id;
            return View(viewModel);
        }

        // GET: Orders/Create
        [Authorize(Roles = "Employees, Admins")]
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CompanyName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName");
            ViewBag.ShipVia = new SelectList(db.Shippers, "ShipperID", "CompanyName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
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



        // GET: Orders/Edit/5
        [Authorize(Roles = "Employees, Admins")]
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

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
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

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Delete(int? id)
        {

            if(id==null)
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
            viewModel.order = orders;

            //take order-details of orders
            var ordet = from od in db.Order_Details
                        where (od.OrderID == id)
                        select new { od.OrderID, od.ProductID, od.Quantity, od.UnitPrice, od.Discount };


            List<DetailsOfOrder> list = new List<DetailsOfOrder>();

            //lopp in all order-details
            foreach (var item in ordet)
            {
                DetailsOfOrder x = new DetailsOfOrder();

                x.OrderID = item.OrderID;
                x.ProductID = item.ProductID;
                x.Quantity = item.Quantity;
                x.UnitPrice = item.UnitPrice;
                x.Discount = item.Discount;


                list.Add(x);

            }
            viewModel.details = list;
            ViewBag.orderid = id;

            return View(viewModel);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {


            var details = db.Order_Details.Where(x=>x.OrderID==id);
            foreach(var orderdet in details)
                db.Order_Details.Remove(orderdet);

            Orders orders = await db.Orders.FindAsync(id);
            db.Orders.Remove(orders);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        // GET: Orders by Json
        public JsonResult JsonTableFill()
        {
            var orders = db.Orders.Include(o => o.Customer).Include(o => o.Employee).Include(o => o.Shipper).OrderBy(o => o.OrderID);

            /*Select what wee need in table*/
            return Json(
                orders.Select(x => new {
                    ID = x.OrderID,
                    LastName = x.Employee.LastName,
                    CompanyName = x.Shipper.CompanyName,
                    ShippedDate = x.ShippedDate.ToString(),
                    ShipName = x.ShipName,
                    ShipAddress = x.ShipAddress
                    
                })
                , JsonRequestBehavior.AllowGet);
        }
    }
}
