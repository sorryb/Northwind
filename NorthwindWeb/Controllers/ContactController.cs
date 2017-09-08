using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Linq;
namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Contact Controller. For table Persons which is used in Contact
    /// </summary>
    public class ContactController : Controller
    {
        private NorthwindModel db = new NorthwindModel();
        ///Enter in the page Index from Contact
        public ActionResult Index()
        {
            return View();
        }

        ///Prepare to create in form a person in the page Index
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        ///Add a new prson which contain the next fields: ID,LastName,FirstName,Ageand it will be saved in database
        public ActionResult Create([Bind(Include = "ID,LastName,FirstName,Age")] Persons person)
        {///create person from Form
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