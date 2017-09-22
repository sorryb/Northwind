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
using NorthwindWeb.Context;

namespace NorthwindWeb.Controllers
{

    /// <summary>
    /// Territories Controller. For table Territories
    /// </summary>
    [Authorize(Roles = "Admins, Managers")]
    public class TerritoriesController : Controller
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));  //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.
        private NorthwindDatabase db = new NorthwindDatabase();

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

            //take details of Territory
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
        public ActionResult Create(int? id)
        {
            ViewBag.regionid = id;
            return View();
        }

        /// <summary>
        /// Inserts a territory into the database table. If it fails, goes back to the form.
        /// </summary>
        /// <param name="territories">The territory entity to be inserted</param>
        /// <param name="id">The territory find by region's id</param>
        /// <returns>If successful returns territories index view, else goes back to form.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TerritoryDescription")] Territories territories, int id)
        {
            territories.RegionID = id;
            if (db.Territories.Any())
            {
                var lastItem = db.Territories.Select(x => new { nr = x.TerritoryID }).ToList().OrderByDescending(x => int.Parse(x.nr)).First();
                territories.TerritoryID = (int.Parse(lastItem.nr) + 1).ToString();
            }
            else
            {
                territories.TerritoryID = "1";
            }
            if (ModelState.IsValid)
            {
                db.Territories.Add(territories);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Regions", new { id = id });
            }
            ViewBag.regionid = id;
            return View(territories);
        }

        /// <summary>
        /// Returns the view containing the form necessary for editing an existing territory.
        /// </summary>
        /// <param name="id">The id of the territory that is going to be edited</param>
        /// <returns>Territories edit view</returns>
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //take details of Territory
            ViewBag.RegionsID = Convert.ToInt32(TempData["RegionID"]);
            TempData["RegionID"] = ViewBag.RegionsID;
            Territories territories = await db.Territories.FindAsync(id);
            if (territories == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "RegionDescription", territories.RegionID);
            territories.TerritoryDescription = territories.TerritoryDescription.Trim();
            return View(territories);
        }

        /// <summary>
        /// Updates the database changing the fields of the territory whose id is equal to the id of the provided territories parameter to those of the parameter.
        /// </summary>
        /// <param name="territories">The changed territory.</param>
        /// <returns>Territories index view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TerritoryID,TerritoryDescription,RegionID")] Territories territories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(territories).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Details", "Regions", new { id = Convert.ToInt32(TempData["RegionID"]) });
            }
            
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "RegionDescription", territories.RegionID);
            return View(territories);
        }

        /// <summary>
        /// Displays a confirmation page for the following delete.
        /// </summary>
        /// <param name="id">The territory that is going to be deleted.</param>
        /// <returns>Delete view</returns>
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
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            //take details of Territory
            Territories territories = await db.Territories.FindAsync(id);
            int idRegion = territories.RegionID;
            try
            {
                territories.Employees.Clear();
                db.Territories.Remove(territories);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Regions", new { id = idRegion });
            }
            catch (Exception exception)
            {
                logger.Error(exception.ToString());
                throw new DeleteException("Nu puteti sterge teritoriul deoarece are angajati asignati la el. \nPentru a putea sterge acest teritoriu trebuie sa stergi angajatii asignati mai intai!.");
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
