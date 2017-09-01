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
using NorthwindWeb.ViewModels;
using NorthwindWeb.Models.ExceptionHandler;

namespace NorthwindWeb.Controllers
{
    [Authorize(Roles = "Admins")]
    /// <summary>
    /// Regions Controller. For table Region
    /// </summary>
    public class RegionsController : Controller
    {
        private NorthwindModel db = new NorthwindModel();

        // GET: Regions
        public async Task<ActionResult> Index(string search = "")
        {
            
            return View(await db.Regions.Where(r=>r.RegionDescription.Contains(search)).ToListAsync());
        }

        // GET: Regions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegionIndex viewModel = new RegionIndex();
            Region region = await db.Regions.FindAsync(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            viewModel.region = region;

            var ter = from t in db.Territories
                        where (t.RegionID == id)
                        select new { t.TerritoryID,t.TerritoryDescription };

            List<RegionDetails> list = new List<RegionDetails>();

            
            foreach (var item in ter)
            {
                RegionDetails x = new RegionDetails();

                x.TerritoryID = item.TerritoryID;
                x.TerritoryDescription = item.TerritoryDescription;

                list.Add(x);

            }
            viewModel.details = list;
            ViewBag.regionid = id;
            return View(viewModel);
        }

        // GET: Regions/Create
        [Authorize(Roles = "Employees, Admins")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Regions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Create([Bind(Include = "RegionID,RegionDescription")] Region region)
        {
            if (ModelState.IsValid)
            {
                db.Regions.Add(region);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(region);
        }

        // GET: Regions/Edit/5
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = await db.Regions.FindAsync(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: Regions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit([Bind(Include = "RegionID,RegionDescription")] Region region)
        {
            if (ModelState.IsValid)
            {
                db.Entry(region).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(region);
        }

        // GET: Regions/Delete/5
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = await db.Regions.FindAsync(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: Regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Region region = await db.Regions.FindAsync(id);
            
            try 
            {
                db.Regions.Remove(region);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
            throw new DeleteException("Nu poti sterge regiunea deoarece are constrangeri.");
            }
           
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
        public JsonResult JsonTableFill(string search = "")
        {
            var regions = db.Regions.Include(p => p.RegionDescription).Where(r => r.RegionDescription.Contains(search));
            
            /*Select what wee need in table*/
            return Json(
                regions.Select(x => new {
                    ID = x.RegionID,
                    RegionDescription = x.RegionDescription
                 
                })
                , JsonRequestBehavior.AllowGet);
        }
    }
}
