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
using NorthwindWeb.Models.ExceptionHandler;

namespace NorthwindWeb.Controllers
{
    [Authorize]
    /// <summary>
    /// Categories Controller. For table Categories
    /// </summary>
    public class CategoriesController : Controller
    {
        private NorthwindModel db = new NorthwindModel();

        // GET: Categories
        public async Task<ActionResult> Index(string search = "")
        {
            return View(await db.Categories.Where(x=>x.CategoryName.Contains(search)).ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //take details of Categories
            Categories categories = await db.Categories.FindAsync(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // GET: Categories/Create
        [Authorize(Roles = "Employees, Admins")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Create([Bind(Include = "CategoryID,CategoryName,Description")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(categories);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(categories);
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //take details of Categories
            Categories categories = await db.Categories.FindAsync(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit([Bind(Include = "CategoryID,CategoryName,Description")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categories).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //take details of Categories
            Categories categories = await db.Categories.FindAsync(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //take details of Categories
            Categories categories = await db.Categories.FindAsync(id);

            try
            {
                db.Categories.Remove(categories);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                string list = "";
                var productId = db.Products.Include(x => x.Category).Where(x => x.CategoryID == id).Select(x => new { x.ProductName });
                foreach (var i in productId)
                {
                    //lopp in ProductName
                    list = list + i.ProductName.ToString() + ",\n ";
                }
                throw new DeleteException("Nu poti sterge categoria deoarece are produse cu numele:\n" + list+ "\nPentru a putea sterge aceasta categorie trebuie sterse produsele.");
            }
        }

        

        // GET: Categories by Json
        public JsonResult JsonTableFill(string search = "")
        {
            var categories = db.Categories.Include(p=>p.Description).Where(x => x.CategoryName.Contains(search)).OrderBy(x => x.CategoryID);

            /*Select what wee need in table*/
            return Json(
               categories.Select(x => new NorthwindWeb.Models.ServerClientCommunication.CategoriesData
               {
                    ID = x.CategoryID,
                    CategoryName = x.CategoryName,
                    Description = x.Description

               })
                , JsonRequestBehavior.AllowGet);
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
