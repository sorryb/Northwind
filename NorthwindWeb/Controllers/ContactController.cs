using System.Web.Mvc;

namespace NorthwindWeb.Controllers
{
    public class ContactController : Controller
    {
        
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
        public ActionResult Create([Bind(Include = "ID,LastName,FirstName,Age")] Models.Persons persons)
        {
            if (ModelState.IsValid)
            {
                //db.Persons.Add(persons);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}