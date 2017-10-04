using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindWeb.Core.Context;

namespace NorthwindWeb.Core.Controllers
{
    public class HomeController : Controller
    {
        private NorthwindDatabase db;
        //private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));  //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.


        public HomeController(NorthwindDatabase context)
        {
            db = context;
        }

        /// <summary>
        /// First page in the site.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            ViewBag.SiteName = "Northwind Phone Shop";

            return View("Index");
        }

        //Menu() was here
        //Moved to ViewComponents
        //Used in _LayoutDashboard
        //https://davepaquette.com/archive/2016/01/02/goodbye-child-actions-hello-view-components.aspx



    }
}