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
        //[ChildActionOnly]
        //public ActionResult Menu()
        //{
        //    var categori = db.Categories;
        //    List<string> list = new List<string>();
        //    foreach(var item in categori)
        //    {
        //        string x = item.CategoryName;
        //        list.Add(x);
        //    }
        //    return View(list);
        //}
    }
}