using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Used from enter page from the site (public site not admin site).
    /// </summary>
    public class HomeController : Controller
    {

        NorthwindModel _northwindDatabase = new NorthwindModel();

        /// <summary>
        /// First page in the site.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.SiteName = "Northwind Phone Shop";

            return View("Index");
        }

        /// <summary>
        /// Used to construct the menu.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Menu()
        {
            var productsCategories = _northwindDatabase.Categories;
            List<string> listOfCategories = new List<string>();
            foreach (var item in productsCategories)
            {
                string categoryName = item.CategoryName;
                listOfCategories.Add(categoryName);
            }

            return View(listOfCategories);
        }
    }
}