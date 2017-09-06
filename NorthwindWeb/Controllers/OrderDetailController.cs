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

namespace NorthwindWeb.Controllers
{
    [Authorize(Roles = "Admins")]
    /// <summary>
    /// OrderDetail Controller. For table Order_Details
    /// </summary>
    public class OrderDetailController : Controller
    {
        private NorthwindModel db = new NorthwindModel();


        /// <summary>
        /// Returns a paged list with all order-details
        /// </summary>
        /// <param name="page">Required for paged list to work</param>
        /// 
        /// <returns>PagedList</returns>
        // GET: OrderDetail
        public ActionResult Index(int page = 1)
        {
            var order_Details = db.Order_Details.Include(o => o.Order).Include(o => o.Product).OrderBy(o=>o.OrderID);
          
            int pageSize = 15;
            int pageNumber = page;
            return View(order_Details.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Displays a page showing all the information about one order-detail.
        /// </summary>
        /// <param name="id">The id of the order-detail whose information to show</param>
        /// <returns>Orders-details details view</returns>
        // GET: OrderDetail/Details/5
        public async Task<ActionResult> Details(int? OrderID, int? ProductID)
        {
            Order_Details orderdetail = await db.Order_Details.FindAsync(OrderID, ProductID);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID", orderdetail.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", orderdetail.ProductID);
            return View(orderdetail);
        }

        /// <summary>
        /// Returns the view containing the form neccesary for creating a new order-detail.
        /// </summary>
        /// <returns>Create view.</returns>
        // GET: OrderDetail/Create
        public ActionResult Create(int? id)
        {
            ViewBag.orderid = id;
            //ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            return View();
        }

        /// <summary>
        /// Inserts an order-detail into the database table. If it fails, goes back to the form.
        /// </summary>
        /// <param name="order_Details">The order-detail entity to be inserted</param>
        /// <param name="id">The order id for which order-details are made</param>
        /// <returns>If successful returns orders-details index view, else goes back to form.</returns>
        // POST: OrderDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductID,UnitPrice,Quantity,Discount")] Order_Details order_Details,int id)
        {
            order_Details.OrderID = id;
            if (ModelState.IsValid)
            {
                db.Order_Details.Add(order_Details);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID", order_Details.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order_Details.ProductID);
            return View(order_Details);
        }

        /// <summary>
        /// Returns the view containing the form necessary for editing an existing order-detail.
        /// </summary>
        /// <param name="OrderID">The OrderID of the order-detail that is going to be edited</param>
        /// <param name="ProductID">The ProductID of the order-detail that is going to be edited</param>
        /// <returns>Orders-details edit view</returns>
        // GET: OrderDetail/Edit/5
        public async Task<ActionResult> Edit(int? OrderID,int? ProductID)
        {
            Order_Details orderdetail = await db.Order_Details.FindAsync(OrderID,ProductID);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID", orderdetail.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", orderdetail.ProductID);
            return View(orderdetail);

        }

        /// <summary>
        /// Updates the database 
        /// changing the fields of the order-detail whose id is equal to the id of the provided orders-details parameter
        /// to those of the parameter.
        /// </summary>
        /// <param name="order_Details">The changed order-detail.</param>
        /// <returns>Orders-details index view</returns>
        // POST: OrderDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderID,ProductID,UnitPrice,Quantity,Discount")] Order_Details order_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Details).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID", order_Details.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order_Details.ProductID);
            return View(order_Details);
        }

        /// <summary>
        /// Displays a confirmation page for the following delete.
        /// </summary>
        /// <param name="OrderID">The OrderID of the order-detail that is going to be deleted.</param>
        /// <param name="ProductID">The ProductID of the order-detail that is going to be deleted.</param>
        /// <returns>Delete view</returns>
        // GET: OrderDetail/Delete/5
        public async Task<ActionResult> Delete(int? OrderID, int? ProductID)
        {
            Order_Details orderdetail = await db.Order_Details.FindAsync(OrderID, ProductID);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID", orderdetail.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", orderdetail.ProductID);
            return View(orderdetail);
        }

        /// <summary>
        /// Deletes an order-detail from the database. 
        /// </summary>
        /// <param name="OrderID">The OrderID of the order-detail that is going to be deleted.</param>
        /// <param name="ProductID">The ProductID of the order-detail that is going to be deleted.</param>
        /// <returns>Orders index view</returns>
        // POST: OrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? OrderID, int? ProductID)
        {
            var details = db.Order_Details.Where(x => x.OrderID == OrderID && x.ProductID==ProductID);
            foreach (var orderdet in details)
                db.Order_Details.Remove(orderdet);

            await db.SaveChangesAsync();
            return RedirectToAction("Index","Orders");

        }

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
