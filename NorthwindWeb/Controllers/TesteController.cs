using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;

namespace NorthwindWeb.Controllers
{
    public class TesteController : Controller
    {
        private Models.NorthwindModel db = new Models.NorthwindModel();
        // GET: Teste
        public ActionResult Index(int id=0)
        {
            List<string> filenames = new List<string>();

            string dirpath = Path.GetFullPath(Path.Combine(Server.MapPath("~"), @"../NorthwindReports"));
            foreach (var filepath in Directory.GetFiles(dirpath, "*rdl"))
            {
                string filename = Path.GetFileNameWithoutExtension(filepath);
                filenames.Add(filename);
            }
            ViewBag.filenames = filenames;
            string serverurl = "http://localhost/" + ConfigurationManager.AppSettings.Get("ReportServer") + "/";
            ReportViewer rep = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                //Width = Unit.Percentage(100),
                //Height = Unit.Percentage(100),
                ZoomMode = ZoomMode.PageWidth,
                AsyncRendering = true,
                ID = "asdf"
                
            };
            rep.ServerReport.ReportServerUrl = new Uri(serverurl);
            rep.ServerReport.ReportPath = "/NorthwindReports/" + filenames.ElementAt(id);
            rep.ServerReport.DisplayName = filenames.ElementAt(id);
            ViewBag.rep = rep;

            return View();
        }
    }
}
