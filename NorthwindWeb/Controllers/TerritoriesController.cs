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

        /// <summary>
        /// Displays a page with all the territories existing in the database.
        /// </summary>
        /// <param name="search">The search look to find something asked</param>
        /// <returns>Territories index view</returns>
        public async Task<ActionResult> Index()
        {
            var territories = db.Territories.Include(t => t.Region);
            return View(await territories.ToListAsync());
        }

        /// <summary>
        /// Displays a page showing all the information about one territory.
        /// </summary>
        /// <param name="id">The id of the territory whose information to show</param>
        /// <returns>Territory details view</returns>
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ///take details of Territory
            Territories territories = await db.Territories.FindAsync(id);
            if (territories == null)
            {
                return HttpNotFound();
            }
            return View(territories);
        }

        /// <summary>
        /// Returns the view containing the form neccesary for creating a new territory.
        /// </summary>
        /// <returns>Create view.</returns>
        [Authorize(Roles = "Employees, Admins")]
        ///Enter in the page Create via RegionID
        public ActionResult Create(int? id)
        {
            ViewBag.regionid = id;
            return View();
        }

        /// <summary>
        /// Inserts a territory into the database table. If it fails, goes back to the form.
        /// </summary>
        /// <param name="territories">The territory entity to be inserted</param>
        /// <returns>If successful returns territories index view, else goes back to form.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Create([Bind(Include = "TerritoryID,TerritoryDescription")] Territories territories, int id)
        {
            territories.RegionID = id;
            if (ModelState.IsValid)
            {
                db.Territories.Add(territories);
                await db.SaveChangesAsync();
                return RedirectToAction("Details","Regions",new { id=id });
            }
            
            return View(territories);
        }

        /// <summary>
        /// Returns the view containing the form necessary for editing an existing territory.
        /// </summary>
        /// <param name="id">The id of the territory that is going to be edited</param>
        /// <returns>Territories edit view</returns>
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //take details of Territory
            Territories territories = await db.Territories.FindAsync(id);
            if (territories == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "RegionDescription", territories.RegionID);
            return View(territories);
        }

        /// <summary>
        /// Updates the database changing the fields of the territory whose id is equal to the id of the provided territories parameter to those of the parameter.
        /// </summary>
        /// <param name="territories">The changed territory.</param>
        /// <returns>Territories index view</returns>
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

        /// <summary>
        /// Displays a confirmation page for the following delete.
        /// </summary>
        /// <param name="id">The territory that is going to be deleted.</param>
        /// <returns>Delete view</returns>
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //take details of Territory
            Territories territories = await db.Territories.FindAsync(id);
            if (territories == null)
            {
                return HttpNotFound();
            }
            return View(territories);
        }

        /// <summary>
        /// Deletes a territory from the database. The territory must not have employees.
        /// </summary>
        /// <param name="id">The id of the territory that is going to be deleted</param>
        /// <returns>Territories index view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ///take details of Territory
            Territories territories = await db.Territories.FindAsync(id);
            int idRegion=territories.RegionID;
            try
            {
                territories.Employees.Clear();
                db.Territories.Remove(territories);
                await db.SaveChangesAsync();
                return RedirectToAction("Details","Regions",new { id=idRegion });
            }
            catch
            {
                throw new DeleteException("Nu poti sterge teritoriul deoarece contine angajati. \nPentru a putea sterge acest teritoriu trebuie sa stergi angajatii.");
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
