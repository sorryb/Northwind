using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace NorthwindWeb.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            //var test = Directory.EnumerateFiles("C:\Users\intern\Source\GitHub\Northwind\NorthwindReports", ".rdl");

            return View();
        }
    }
}