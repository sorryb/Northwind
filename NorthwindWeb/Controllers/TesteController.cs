using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.IO;
namespace NorthwindWeb.Controllers
{
    public class TesteController : Controller
    {
        private Models.NorthwindModel db = new Models.NorthwindModel();
        // GET: Teste
        public ActionResult Index()
        {
            string serverurl = "http://localhost/" + ConfigurationManager.AppSettings.Get("ReportServer") + "/";
            List<ReportViewer> reports = new List<ReportViewer>();

            string dirpath = Path.GetFullPath(Path.Combine(Server.MapPath("~"), @"../NorthwindReports"));
            ReportViewer rep;
            foreach (var filepath in Directory.GetFiles(dirpath, "*rdl"))
            {
                rep = new ReportViewer()
                {
                    ProcessingMode = ProcessingMode.Remote
                };

                string filename = Path.GetFileNameWithoutExtension(filepath);

                rep.ServerReport.ReportServerUrl = new Uri(serverurl);
                rep.ServerReport.ReportPath = "/NorthwindReports/" + filename;
                rep.ServerReport.DisplayName = filename;
                reports.Add(rep);
            }
            ViewBag.reports = reports;

            return View();
        }


    }
}
