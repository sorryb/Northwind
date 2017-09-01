using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using System.Configuration;
using HtmlAgilityPack;
using System.Linq;
using System.Net;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;

namespace NorthwindWeb.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        [Authorize]
        public ActionResult Index()
        {
            string reportServer = ConfigurationManager.AppSettings.Get("ReportServer");
            string reportServerDir = ConfigurationManager.AppSettings.Get("ReportServerDirectory");
            string dirpath = Path.GetFullPath(Path.Combine(Server.MapPath("~"), $@"../{reportServerDir}"));
            //var request = (HttpWebRequest)WebRequest.Create($"{reportServer}/browse/?%2f{reportServerDir}&rs:Command=ListChildren");






            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            
            doc = web.Load($"{reportServer}?%2f{reportServerDir}","localhost",80,"intern","kepler");
            var links = doc.DocumentNode.SelectNodes("//a");
            var links2=links.Skip(1);
            List<string> hrefs = new List<string>();
            foreach (var link in links2)
            {
                hrefs.Add(link.GetAttributeValue("href", ""));
            }


            List<ViewModels.ReportViewModel> reports = new List<ViewModels.ReportViewModel>();
            ViewModels.ReportViewModel temp;

            foreach (var filepath in Directory.GetFiles(dirpath, "*rdl"))
            {
                string filename = Path.GetFileNameWithoutExtension(filepath);
                string link = $"{reportServer}/Pages/ReportViewer.aspx?%2fNorthwindReports%2f{filename.Replace(' ', '+')}&rs:Command=Render&rc:zoom=Page%20Width";
                temp = new ViewModels.ReportViewModel(link, filename);
                reports.Add(temp);
            }

            return View(reports);
        }
    }
}