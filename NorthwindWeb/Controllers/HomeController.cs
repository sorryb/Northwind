using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;

namespace NorthwindWeb.Controllers
{
    public class HomeController : Controller
    { NorthwindModel db = new NorthwindModel();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menu()
        {
            var categori = from c in db.Categories
                           select new { c.CategoryName };
            return Json(categori);
        }
    }
}