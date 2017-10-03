using NorthwindWeb.Models;
using System.Linq;
using NorthwindWeb.Context;
using Microsoft.AspNetCore.Mvc;
using NorthwindWeb.Core.Context;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// AboutController. For the page Index from AboutUs 
    /// </summary>
    public class AboutController : Controller
    {
        NorthwindDatabase db = new NorthwindDatabase(new Microsoft.EntityFrameworkCore.DbContextOptions<NorthwindDatabase>());
       
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