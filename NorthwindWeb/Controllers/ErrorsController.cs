using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Contains methods that read and show error logs.
    /// </summary>
    public class ErrorsController : Controller
    {
        /// <summary>
        /// Displays a page with a table containing all the errors from the NorthwindLog
        /// </summary>
        /// <returns>Errors index view</returns>
        public ActionResult Index()
        {
            var test = EventLog.GetEventLogs();
            


            return View();
        }
    }
}