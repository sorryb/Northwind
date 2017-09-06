using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;

namespace NorthwindWeb.Controllers
{
    public class ShopCartController : Controller
    {
        NorthwindModel db = new NorthwindModel();

        /// <summary>
        /// See the curent shop list
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}