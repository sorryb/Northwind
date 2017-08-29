using System.Web.Mvc;
using NorthwindWeb.Models;
using System.Linq;
namespace NorthwindWeb.Controllers
{
    public class AboutController : Controller
    {
        NorthwindModel db = new NorthwindModel();
        //
        /// <summary>
        /// Select the first 6 employees
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            var aboutus = from y in db.Employees
                          select y;
            aboutus = aboutus.Take(6);



            return View(aboutus);
        }
	}
}