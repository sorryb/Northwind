using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;
using System.Web.Script.Serialization;
using System.Configuration;
using log4net;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Used from enter page from the site (public site not admin site).
    /// </summary>
    public class HomeController : Controller
    {

        NorthwindModel _northwindDatabase = new NorthwindModel();
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));  //Declaring Log4Net

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
            try
            {

                var productsCategories = _northwindDatabase.Categories;
                List<string> listOfCategories = new List<string>();
                foreach (var item in productsCategories)
                {
                    string categoryName = item.CategoryName;
                    listOfCategories.Add(categoryName);
                }

                int x, y, z;
                x = 5; y = 0;
                z = x / y;

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