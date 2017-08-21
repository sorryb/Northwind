using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;
using NorthwindWeb.ViewModels;
using PagedList;

namespace NorthwindWeb.Controllers
{
    public class ServicesController : Controller
    {
        private Models.NorthwindModel db = new Models.NorthwindModel();
        public ActionResult Index()
        {
            var viewModel = new ServicesIndex();
            viewModel.top4nume = (from p in db.Products
                                  orderby p.ProductID
                                  select p.ProductName).Take(4);
            //var x = from p in db.Products
            //        orderby p.ProductID
            //        select p.ProductName;
            //x=x.Take(4);
                   

            return View(viewModel);
        }
	}
}