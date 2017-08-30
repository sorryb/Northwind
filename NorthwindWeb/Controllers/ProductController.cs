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







        
        // here we simulate SQL search, sorting and paging operations
        // !!!! DO NOT DO THIS IN REAL APPLICATION !!!!
        private IQueryable FilterData(ref int recordFiltered, int start, int length, string search, int sortColumn, string sortDirection)
        {
            IQueryable list = new List<DataItem>();
            if (search == null)
            {
                list = _data;
            }
            else
            {
                // simulate search
                foreach (DataItem dataItem in _data)
                {
                    if (dataItem.Name.ToUpper().Contains(search.ToUpper()) ||
                        dataItem.Age.ToString().Contains(search.ToUpper()) ||
                        dataItem.DoB.ToString().Contains(search.ToUpper()))
                    {
                        list.Add(dataItem);
                    }
                }
            }

            // simulate sort
            if (sortColumn == 0)
            {// sort Name
                list.Sort((x, y) => SortString(x.Name, y.Name, sortDirection));
            }
            else if (sortColumn == 1)
            {// sort Age
                list.Sort((x, y) => SortInteger(x.Age, y.Age, sortDirection));
            }
            else if (sortColumn == 2)
            {   // sort DoB
                list.Sort((x, y) => SortDateTime(x.DoB, y.DoB, sortDirection));
            }

            recordFiltered = list.Count;

            // get just one page of data
            list = list.GetRange(start, Math.Min(length, list.Count - start));

            return list;
        }

        // GET: Product by Json
        public JsonResult JsonTestServerSide(int draw, int start, int length)
        {
            const int TOTAL_ROWS = 999;


            string search = Request.QueryString["search[value]"];
            int sortColumn = -1;
            string sortDirection = "asc";
            if (length == -1)
            {
                length = TOTAL_ROWS;
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

            var list = db.Products.Include(p => p.Category).Include(p => p.Supplier).Where(p => p.ProductName.Contains(search));

            //order list
            switch (sortColumn)
            {
                case 0: //first column
                    if(sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.ProductName);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.ProductName);
                    }
                    break;
                case 1: //second column
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.UnitPrice);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.UnitPrice);
                    }
                    break;
                case 2: // and so on
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.UnitsInStock);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.UnitsInStock);
                    }
                    break;
                case 3:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.UnitsOnOrder);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.UnitsOnOrder);
                    }
                    break;
                case 4:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.ReorderLevel);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.ReorderLevel);
                    }
                    break;
                case 5:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.Discontinued);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.Discontinued);
                    }
                    break;
            }
            
            //objet that whill be sent to client
            JsonDataTableObject dataTableData = new JsonDataTableObject()
            {
                draw = draw,
                recordsTotal = TOTAL_ROWS,
                data = list,
                recordsFiltered = list.Count(), //need to be below data(ref recordsFiltered)

            };

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
    }




}
