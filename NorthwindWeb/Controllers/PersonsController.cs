using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;

namespace NorthwindWeb.Controllers
{
    [Authorize]
    /// <summary>
    /// Persons Controller. For table Persons
    /// </summary>
    public class PersonsController : Controller
    {
        private NorthwindModel db = new NorthwindModel();

        /// <summary>
        /// Displays a page with all the persons in the database.
        /// </summary>
        /// <returns>Persons index view</returns>
        public ActionResult Index()
        {
            return View(db.Persons.ToList());
        }

        /// <summary>
        /// Displays a page showing all the information about one person.
        /// </summary>
        /// <param name="id">The id of the person whose information to show</param>
        /// <returns>Persons details view</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //take details of Persons
            Persons persons = db.Persons.Find(id);
            if (persons == null)
            {
                return HttpNotFound();
            }
            return View(persons);
        }

        /// <summary>
        /// Returns the view containing the form neccesary for creating a new person.
        /// </summary>
        /// <returns>Create view.</returns>
        [Authorize(Roles = "Employees, Admins")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Inserts a person into the database table. If it fails, goes back to the form.
        /// </summary>
        /// <param name="persons">The person entity to be inserted</param>
        /// <returns>If successful returns persons index view, else goes back to form.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstName,Age")] Persons persons)
        {
            if (ModelState.IsValid)
            {
                db.Persons.Add(persons);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(persons);
        }

        /// <summary>
        /// Returns the view containing the form necessary for editing an existing person.
        /// </summary>
        /// <param name="id">The id of the person that is going to be edited</param>
        /// <returns>Persons edit view</returns>
        [Authorize(Roles = "Employees, Admins")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persons persons = db.Persons.Find(id);
            if (persons == null)
            {
                return HttpNotFound();
            }
            return View(persons);
        }

        /// <summary>
        /// Updates the database changing the fields of the personr whose id is equal to the id of the provided persons parameter to those of the parameter.
        /// </summary>
        /// <param name="shippers">The changed personr.</param>
        /// <returns>Persons index view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstName,Age")] Persons persons)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persons).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(persons);
        }

        /// <summary>
        /// Displays a confirmation page for the following delete.
        /// </summary>
        /// <param name="id">The person that is going to be deleted.</param>
        /// <returns>Delete view</returns>
        [Authorize(Roles = "Admins")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persons persons = db.Persons.Find(id);
            if (persons == null)
            {
                return HttpNotFound();
            }
            return View(persons);
        }

        /// <summary>
        /// Deletes a person from the database.
        /// </summary>
        /// <param name="id">The id of the person that is going to be deleted</param>
        /// <returns>Persons index view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public ActionResult DeleteConfirmed(int id)
        {
            Persons persons = db.Persons.Find(id);
            db.Persons.Remove(persons);
            db.SaveChanges();
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
    }
}
