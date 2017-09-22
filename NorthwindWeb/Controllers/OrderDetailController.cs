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
using NorthwindWeb.Context;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// OrderDetail Controller. For table Order_Details
    /// </summary>
    [Authorize(Roles = "Admins, Managers")]
    public class OrderDetailController : Controller
    {
        private NorthwindDatabase db = new NorthwindDatabase();

        /// <summary>
        /// Displays a page showing all the information about one order-detail.
        /// </summary>
        /// <param name="orderID">The orderID of the order-detail that is going to be showed</param>
        /// <param name="productID">The productID of the order-detail that is going to be showed</param>
        /// <returns>Orders-details details view</returns>
        public async Task<ActionResult> Details(int? orderID, int? productID)
        {
            Order_Details orderdetail = await db.Order_Details.FindAsync(orderID, productID);
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
        public ActionResult Create(int? id)
        {
            ViewBag.orderid = id;
            ////ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID");
            var productsOnOrder = db.Orders.Find(id).Order_Details.Select(od => od.Product).AsEnumerable();
            var productsNotOnOrder = db.Products.ToList();
            foreach (Products product in productsOnOrder)
                productsNotOnOrder.Remove(product);
            ViewBag.ProductID = new SelectList(productsNotOnOrder, "ProductID", "ProductName");
            return View();
        }

        /// <summary>
        /// Inserts an order-detail into the database table. If it fails, goes back to the form.
        /// </summary>
        /// <param name="order_Details">The order-detail entity to be inserted</param>
        /// <param name="id">The order id for which order-details are made</param>
        /// <returns>If successful returns orders-details index view, else goes back to form.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductID,Quantity,Discount")] Order_Details order_Details, int id)
        {
            order_Details.OrderID = id;
            order_Details.UnitPrice = db.Products.Find(order_Details.ProductID).UnitPrice ?? 0;
            if (ModelState.IsValid)
            {
                db.Order_Details.Add(order_Details);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Orders", new { id = id });
            }

            //ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID", order_Details.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order_Details.ProductID);
            return View(order_Details);
        }

        /// <summary>
        /// Returns the view containing the form necessary for editing an existing order-detail.
        /// </summary>
        /// <param name="orderID">The orderID of the order-detail that is going to be edited</param>
        /// <param name="productID">The productID of the order-detail that is going to be edited</param>
        /// <returns>Orders-details edit view</returns>
        public async Task<ActionResult> Edit(int? orderID, int? productID)
        {
            Order_Details orderdetail = await db.Order_Details.FindAsync(orderID, productID);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderID,ProductID,Quantity,Discount")] Order_Details order_Details)
        {
            order_Details.UnitPrice = db.Products.Find(order_Details.ProductID).UnitPrice ?? 0;
            if (ModelState.IsValid)
            {
                db.Entry(order_Details).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Orders", new { id = order_Details.OrderID });
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID", order_Details.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order_Details.ProductID);
            return View(order_Details);
        }

        /// <summary>
        /// Displays a confirmation page for the following delete.
        /// </summary>
        /// <param name="orderID">The orderID of the order-detail that is going to be deleted.</param>
        /// <param name="productID">The productID of the order-detail that is going to be deleted.</param>
        /// <returns>Delete view</returns>
        public async Task<ActionResult> Delete(int? orderID, int? productID)
        {
            Order_Details orderdetail = await db.Order_Details.FindAsync(orderID, productID);
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
        /// <param name="orderID">The orderID of the order-detail that is going to be deleted.</param>
        /// <param name="productID">The productID of the order-detail that is going to be deleted.</param>
        /// <returns>Orders index view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? orderID, int? productID)
        {
            var details = db.Order_Details.Where(x => x.OrderID == orderID && x.ProductID == productID);
            foreach (var orderdet in details)
                db.Order_Details.Remove(orderdet);

            await db.SaveChangesAsync();
            return RedirectToAction("Details", "Orders", new { id = orderID });

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
