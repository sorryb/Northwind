using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Linq;
namespace NorthwindWeb.Controllers
{
    public class ContactController : Controller
    {
        private NorthwindModel db = new NorthwindModel();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstName,Age")] Persons person)
        {
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