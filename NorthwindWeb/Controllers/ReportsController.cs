using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using System.Configuration;
using HtmlAgilityPack;
using System.Linq;
using System.Net;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using System;

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

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();

            string userId = ConfigurationManager.AppSettings.Get("userId");
            string password = ConfigurationManager.AppSettings.Get("password");

            doc = web.Load($"{reportServer}?%2f{reportServerDir}", "GET", new WebProxy() {UseDefaultCredentials=true }, new NetworkCredential(userId, password));

            var links = doc.DocumentNode.SelectNodes("//a");
            var links2 = links.Skip(1);



            List<ViewModels.ReportViewModel> reports = new List<ViewModels.ReportViewModel>();
            ViewModels.ReportViewModel temp;

            foreach (var linkloop in links2)
            {
                string filename = linkloop.InnerHtml;
                string link = $"{reportServer}/Pages/ReportViewer.aspx{linkloop.Attributes.FirstOrDefault().DeEntitizeValue}&rc:zoom=Page%20Width";
                temp = new ViewModels.ReportViewModel(link, filename);
                reports.Add(temp);
            }

            return View(reports);
        }
    }
}