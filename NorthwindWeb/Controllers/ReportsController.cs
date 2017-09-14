using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using HtmlAgilityPack;
using System.Linq;
using System.Net;
using System;



namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Contains the methods neccessary to deal with reports.
    /// </summary>
    public class ReportsController : Controller
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ReportsController));
        /// <summary>
        /// Displays a page containing a navbar with a list of reports from a report server provided in web.config
        /// </summary>
        /// <returns>Reports index view</returns>
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                string reportServer = ConfigurationManager.AppSettings.Get("ReportServer");
                string reportServerDir = ConfigurationManager.AppSettings.Get("ReportServerDirectory");

                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = new HtmlDocument();

                string userId = ConfigurationManager.AppSettings.Get("userId");
                string password = ConfigurationManager.AppSettings.Get("password");

                doc = web.Load($"{reportServer}?%2f{reportServerDir}", "GET", new WebProxy() { UseDefaultCredentials = true }, new NetworkCredential(userId, password));

                var links = doc.DocumentNode.SelectNodes("//a");
                var links2 = links.Skip(1);



                List<ViewModels.ReportViewModel> reports = new List<ViewModels.ReportViewModel>();
                ViewModels.ReportViewModel temp;

                //loops through each <a> in links2 and records the reports' href and filename
                foreach (var linkloop in links2)
                {
                    string filename = linkloop.InnerHtml;
                    string link = $"{reportServer}/Pages/ReportViewer.aspx{linkloop.Attributes.FirstOrDefault().DeEntitizeValue}&rc:zoom=Page%20Width";
                    temp = new ViewModels.ReportViewModel(link, filename);
                    reports.Add(temp);
                }
                return View(reports);
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                throw new Exception("A aparut o eroare in timpul afisarii rapoartelor, va rugam incercati din nou. Verificati ca setarile pentru report server sunt corecte.");
            }

        }
    }
}