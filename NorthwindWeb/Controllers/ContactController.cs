using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Linq;
using System;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Contact Controller. For table Persons which is used in Contact index view
    /// </summary>
    public class ContactController : Controller
    {
        private NorthwindModel db = new NorthwindModel();

        /// <summary>
        /// Displays a Contact index page.
        /// </summary>
        /// <returns>Contact index view</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns the view containing the form neccesary for creating a new person.
        /// </summary>
        /// <returns>Contact index view.</returns>
        [HttpGet]
        public ActionResult Create()
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
        public ActionResult Create([Bind(Include = "ID,LastName,FirstName,Email,Comment")] Persons person)
        {//create person from Form

            if (String.IsNullOrEmpty(person.FirstName))
                ModelState.AddModelError("FirstName", "Introduceti-va numele");

            if (String.IsNullOrEmpty(person.Email))
                ModelState.AddModelError("Email", "Email-ul nu a fost introdus corect");

            if (String.IsNullOrEmpty(person.Comment))
                ModelState.AddModelError("Comment", "Va rugam sa va spuneti parerea");

            if (ModelState.IsValid)
            {
                person.ID = db.Persons.Count() + 1;
                db.Persons.Add(person);
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");

        }
    }
}