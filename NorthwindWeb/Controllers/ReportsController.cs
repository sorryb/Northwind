using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using System.Configuration;

namespace NorthwindWeb.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        [Authorize]
        public ActionResult Index()
        {
            string reportServer = ConfigurationManager.AppSettings.Get("ReportServer");
            string dirpath = Path.GetFullPath(Path.Combine(Server.MapPath("~"), @"../NorthwindReports"));

            List<ViewModels.ReportViewModel> reports = new List<ViewModels.ReportViewModel>();
            ViewModels.ReportViewModel temp;
            foreach (var filepath in Directory.GetFiles(dirpath, "*rdl"))
            {
                string filename = Path.GetFileNameWithoutExtension(filepath);
                string link = "http://localhost/" + reportServer + "/Pages/ReportViewer.aspx?%2fNorthwindReports%2f" + filename.Replace(' ','+') + "&rs:Command=Render&rc:zoom=Page%20Width";
                temp = new ViewModels.ReportViewModel(link, filename);
                reports.Add(temp);
            }

            return View(reports);
        }
    }
}