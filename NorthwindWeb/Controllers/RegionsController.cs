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
using NorthwindWeb.Context;

namespace NorthwindWeb.Controllers
{

    /// <summary>
    /// Regions Controller. For table Region
    /// </summary>
    [Authorize(Roles = "Admins, Managers")]
    public class RegionsController : Controller
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));  //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.
        private NorthwindDatabase db = new NorthwindDatabase();

        /// <summary>
        /// Displays a page with all the regions existing in the database.
        /// </summary>
        /// <param name="search">The search look to find something asked</param>
        /// <returns>Regions index view</returns>
        public async Task<ActionResult> Index(string search = "")
        {
            return View(await db.Regions.Where(r=>r.RegionDescription.Contains(search)).ToListAsync());
        }


        /// <summary>
        /// Displays a page showing all the information about one region.
        /// </summary>
        /// <param name="id">The id of the region whose information to show</param>
        /// <returns>Regions details view</returns>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["RegionID"] = id;
            RegionIndex viewModel = new RegionIndex();
            //take details of Region
            Region region = await db.Regions.FindAsync(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            viewModel.region = region;

            var teritory = from t in db.Territories
                        where (t.RegionID == id)
                        select new { t.TerritoryID,t.TerritoryDescription };

            List<RegionDetails> list = new List<RegionDetails>();

            //lopp in all territories
            foreach (var item in teritory)
            {
                RegionDetails regionDetail = new RegionDetails();

                regionDetail.TerritoryID = item.TerritoryID;
                regionDetail.TerritoryDescription = item.TerritoryDescription;

                list.Add(regionDetail);

            }
            viewModel.details = list;
            ViewBag.regionid = id;
            return View(viewModel);
        }

        /// <summary>
        /// Returns the view containing the form neccesary for creating a new region.
        /// </summary>
        /// <returns>Create view.</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Inserts a region into the database table. If it fails, goes back to the form.
        /// </summary>
        /// <param name="region">The region entity to be inserted</param>
        /// <returns>If successful returns regions index view, else goes back to form.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RegionDescription")] Region region)
        {
            if (db.Territories.Any())
            {
                region.RegionID = db.Regions.OrderByDescending(x=>x.RegionID).First().RegionID + 1;
            }
            else
            {
                region.RegionID = 1;
            }
            if (ModelState.IsValid)
            {
                db.Regions.Add(region);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(region);
        }

        /// <summary>
        /// Returns the view containing the form necessary for editing an existing eregion.
        /// </summary>
        /// <param name="id">The id of the region that is going to be edited</param>
        /// <returns>Regions edit view</returns>
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //take details of Region
            Region region = await db.Regions.FindAsync(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            region.RegionDescription = region.RegionDescription.Trim();
            return View(region);
        }

        /// <summary>
        /// Updates the database changing the fields of the region whose id is equal to the id of the provided regions parameter to those of the parameter.
        /// </summary>
        /// <param name="region">The changed region.</param>
        /// <returns>Regions index view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        /// <summary>
        /// Displays a confirmation page for the following delete.
        /// </summary>
        /// <param name="id">The employee that is going to be deleted.</param>
        /// <returns>Delete view</returns>
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //take details of Region
            Region region = await db.Regions.FindAsync(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        /// <summary>
        /// Deletes a region from the database. The region must not contains territories.
        /// </summary>
        /// <param name="id">The id of the employee that is going to be deleted</param>
        /// <returns>Regions index view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //take details of Region
            Region region = await db.Regions.FindAsync(id);

            try
            {
                db.Regions.Remove(region);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
               }
            catch(Exception exception)
            {
                logger.Error(exception.ToString());
                string list = "";
            var territoryId = db.Territories.Include(x => x.Region).Where(x => x.RegionID == id).Select(x => new { x.TerritoryDescription });
                //lopp in all territories
                foreach (var i in territoryId)
            {
                list = list + i.TerritoryDescription + "\n ";
            }
            throw new DeleteException("Nu poti sterge regiunea deoarece contine urmatoarele teritorii:\n"+ list + "\nPentru a putea sterge acesta regiune trebuie sterse teritoriile.");

            }

        }

        /// <summary>
        /// Function used to control the dashboard datatables local
        /// </summary>
        /// <param name="search"></param>
        /// <returns>A JSON filtered regions list.</returns>
        public JsonResult JsonTableFill(string search = "")
        {
            var regions = db.Regions.Include(p => p.RegionDescription).Where(r => r.RegionDescription.Contains(search));
            
            //Select what wee need in table
            return Json(
                regions.Select(x => new NorthwindWeb.Models.ServerClientCommunication.RegionData {
                    ID = x.RegionID,
                    RegionDescription = x.RegionDescription
                 
                })
                , JsonRequestBehavior.AllowGet);
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
