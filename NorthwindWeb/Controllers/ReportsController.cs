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

            string reportServer = "reportserver_SSRS";
            string dirpath = Path.GetFullPath(Path.Combine(Server.MapPath("~"), @"../NorthwindReports"));

            List<ViewModels.ReportViewModel> reports = new List<ViewModels.ReportViewModel>();
            ViewModels.ReportViewModel temp;
            foreach (var filepath in Directory.GetFiles(dirpath, "*rdl"))
            {
                string filename = Path.GetFileNameWithoutExtension(filepath);
                string link = "http://localhost/" + reportServer + "/Pages/ReportViewer.aspx?%2fNorthwindReports%2f" + filename + "&rs:Command=Render";
                temp = new ViewModels.ReportViewModel(link, filename);
                reports.Add(temp);
            }

            return View(reports);
        }
    }
}