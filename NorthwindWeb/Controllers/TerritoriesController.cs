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
    [Authorize(Roles = "Admins")]
    /// <summary>
    /// Territories Controller. For table Territories
    /// </summary>
    public class TerritoriesController : Controller
    {
        private NorthwindModel db = new NorthwindModel();

        // GET: Territories
        public async Task<ActionResult> Index()
        {
            var territories = db.Territories.Include(t => t.Region);
            return View(await territories.ToListAsync());
        }

        // GET: Territories/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Territories territories = await db.Territories.FindAsync(id);
            if (territories == null)
            {
                return HttpNotFound();
            }
            return View(territories);
        }

        // GET: Territories/Create
        [Authorize(Roles = "Employees, Admins")]
        public ActionResult Create(int? id)
        {
            ViewBag.regionid = id;
            //ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "RegionDescription");
            return View();
        }

        // POST: Territories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Create([Bind(Include = "TerritoryID,TerritoryDescription")] Territories territories,int id)
        {
            territories.RegionID = id;
            if (ModelState.IsValid)
            {
                db.Territories.Add(territories);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "RegionDescription", territories.RegionID);
            return View(territories);
        }

        // GET: Territories/Edit/5
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Territories territories = await db.Territories.FindAsync(id);
            if (territories == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "RegionDescription", territories.RegionID);
            return View(territories);
        }

        // POST: Territories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit([Bind(Include = "TerritoryID,TerritoryDescription,RegionID")] Territories territories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(territories).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "RegionDescription", territories.RegionID);
            return View(territories);
        }

        // GET: Territories/Delete/5
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Territories territories = await db.Territories.FindAsync(id);
            if (territories == null)
            {
                return HttpNotFound();
            }
            return View(territories);
        }

        // POST: Territories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Territories territories = await db.Territories.FindAsync(id);
        
            try { 
            db.Territories.Remove(territories);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
            }
            catch
            {
                throw new DeleteException("Nu poti sterge teritoriul deoarece are constrangeri.");
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
    }
}
