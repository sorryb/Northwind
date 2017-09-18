using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Linq;
using System;
using NorthwindWeb.Context;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Contact Controller. For table Persons which is used in Contact index view
    /// </summary>
    public class ContactController : Controller
    {
        private NorthwindDatabase db = new NorthwindDatabase();

        /// <summary>
        /// Displays a Contact index page.
        /// </summary>
        /// <returns>Contact index view</returns>
        public ActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// Inserts a person into the database table. If it fails, goes back to the form.
        /// </summary>
        /// <param name="person">The person entity to be inserted</param>
        /// <returns>If successful returns contact index view, else goes back to form.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ID,LastName,FirstName,Email,Comment")] Persons person)
        {//create person from Form

            if (ModelState.IsValid)
            {
                person.ID = db.Persons.Count() + 1;
                db.Persons.Add(person);
                db.SaveChanges();
            }

            if (String.IsNullOrEmpty(person.FirstName))
                ModelState.AddModelError("","Introduceti-va numele");

            if (String.IsNullOrEmpty(person.Email))
                ModelState.AddModelError("","Email-ul nu a fost introdus corect");

            if (String.IsNullOrEmpty(person.Comment))
                ModelState.AddModelError("","Va rugam sa va spuneti parerea");
            
            return View();

        }
    }
}