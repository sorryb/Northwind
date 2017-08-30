﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;
using NorthwindWeb.Models.Interfaces;
using PagedList;
using System.Web.Helpers;
using NorthwindWeb.Models.ServerClientCommunication;

namespace NorthwindWeb.Controllers
{

    [Authorize]
    public class ProductController : Controller, IJsonTableFill
    {
        private NorthwindModel db = new NorthwindModel();

        // GET: Product
        public ActionResult Index(string category = "", string search = "", int page = 1)
        {
            IOrderedQueryable<Products> products;
            ViewBag.category = category;

            if (category.Equals(""))
            {
                products = db.Products.Include(p => p.Category).Include(p => p.Supplier).Where(p => p.ProductName.Contains(search)).OrderBy(x => x.ProductID);
            }
            else
            {
                products = db.Products.Include(p => p.Category).Include(p => p.Supplier).Where(p => p.Category.CategoryName.Equals(category) && p.ProductName.Contains(search)).OrderBy(x => x.ProductID);
            }
            //int pageSize = 15;
            //int pageNumber = page;
            return View(products.ToList());
        }

        // GET: Product/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Product/Create
        [Authorize(Roles = "Employees, Admins")]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Create([Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(products);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", products.SupplierID);
            return View(products);
        }

        // GET: Product/Edit/5
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", products.SupplierID);
            return View(products);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit([Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", products.SupplierID);
            return View(products);
        }

        // GET: Product/Delete/5
        //TODO Delete from related tables
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Products products = await db.Products.FindAsync(id);
            db.Products.Remove(products);
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

        // GET: Product by Json
        public JsonResult JsonTableFill(string search = "")
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Supplier).Where(p => p.ProductName.Contains(search)).OrderBy(x => x.ProductID);

            /*Select what wee need in table*/
            return Json(
                products.Select(x => new
                {
                    ID = x.ProductID,
                    ProductName = x.ProductName,
                    Price = x.UnitPrice,
                    InStock = x.UnitsInStock,
                    OnOrders = x.UnitsOnOrder,
                    ReorderLevel = x.ReorderLevel,
                    Discontinued = x.Discontinued
                })
                , JsonRequestBehavior.AllowGet);
        }

        // GET: Product by Json
        public JsonResult JsonTestServerSide(
            string search = ""
            )
        {
            JsonDataTableObject dataSendedToClient = new JsonDataTableObject()
            {
                data = db.Products.Include(p => p.Category).Include(p => p.Supplier)
                .Where(p => p.ProductName.Contains(search)).OrderBy(x => x.ProductID)
                .Select(x => new
                {
                    ID = x.ProductID,
                    ProductName = x.ProductName,
                    Price = x.UnitPrice,
                    InStock = x.UnitsInStock,
                    OnOrders = x.UnitsOnOrder,
                    ReorderLevel = x.ReorderLevel,
                    Discontinued = x.Discontinued
                }),
                recordsTotal = 5,
                recordsFiltered = 4,
                draw = 1
            };

            /*Select what wee need in table*/
            return Json(dataSendedToClient, JsonRequestBehavior.AllowGet);
        }
    }
}
