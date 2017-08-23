using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;

namespace NorthwindWeb.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            var paths = Directory.EnumerateFiles("C:\\Users\\intern\\Source\\GitHub\\Northwind\\NorthwindReports", "*rdl");
            List<string> files = new List<string>();
            foreach (var x in paths)
            {
                files.Add(Path.GetFileNameWithoutExtension(x));
            }

            return View(files);
        }
    }
}