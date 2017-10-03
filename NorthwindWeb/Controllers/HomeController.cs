using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NorthwindWeb.Models;
using NorthwindWeb.Context;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Used from enter page from the site (public site not admin site).
    /// </summary>
    public class HomeController : Controller
    {

        private NorthwindDatabase _northwindDatabase = new NorthwindDatabase();
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));  //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.

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
        //[ChildActionOnly]
        public ActionResult Menu()
        {
            var productsCategories = _northwindDatabase.Categories;
            List<string> listOfCategories = new List<string>();

            try
            {
                foreach (var item in productsCategories)
                {
                    string categoryName = item.CategoryName;
                    listOfCategories.Add(categoryName);
                }

                return View(listOfCategories);

            }
            catch (Exception exception)
            {
                logger.Error(exception.ToString());
                return View();
            }
        }


    }
}