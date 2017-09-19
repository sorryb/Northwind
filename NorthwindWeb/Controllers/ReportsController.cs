using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using HtmlAgilityPack;
using System.Linq;
using System.Net;
using System;
using NorthwindWeb.ViewModels;



namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Contains the methods neccessary to deal with reports.
    /// </summary>
    [Authorize(Roles = "Admins, Managers")]
    public class ReportsController : Controller
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ReportsController));
        /// <summary>
        /// Displays a page containing a form to login on the report server.
        /// </summary>
        /// <returns></returns>
        public ActionResult LogIn()
        {
            return View();
        }

        /// <summary>
        /// Shows the reports on the report server.
        /// </summary>
        /// <param name="login">Holds the username and password for the report server</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index([Bind(Include = "Username,Password")] ReportLoginViewModel login)
        {
            try
            {
                string reportServer = ConfigurationManager.AppSettings.Get("ReportServer");
                string reportServerDir = ConfigurationManager.AppSettings.Get("ReportServerDirectory");

                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = new HtmlDocument();

                doc = web.Load($"{reportServer}?%2f{reportServerDir}", "GET", new WebProxy() { UseDefaultCredentials = true }, new NetworkCredential(login.Username, login.Password));

                if (doc == null)
                {
                    throw new ArgumentException("The username or password were not entered correctly. If not the problem might be with the report server.");
                }

                var links = doc.DocumentNode.SelectNodes("//a");
                var links2 = links.Skip(1);



                List<ReportViewModel> reports = new List<ReportViewModel>();
                ReportViewModel temp;

                //loops through each <a> in links2 and records the reports' href and filename
                foreach (var linkloop in links2)
                {
                    string filename = linkloop.InnerHtml;
                    string link = $"{reportServer}/Pages/ReportViewer.aspx{linkloop.Attributes.FirstOrDefault().DeEntitizeValue}&rc:zoom=Page%20Width";
                    temp = new ReportViewModel(link, filename);
                    reports.Add(temp);
                }
                return View(reports);
            }
            catch (WebException e)
            {
                logger.Error(e.ToString());
                throw new WebException("Nu am primit nici un raspuns de la server. Verificati ca adresa serverului este scrisa corect sau ca acesta este pornit.");
            }
            catch (ArgumentException e)
            {
                logger.Error(e.ToString());
                throw new ArgumentException("Numele sau parola nu au fost introduse corect.");
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                throw new Exception("A aparut o eroare in timpul afisarii rapoartelor, va rugam incercati din nou. Verificati ca setarile pentru report server sunt corecte.");
            }

        }
    }
}