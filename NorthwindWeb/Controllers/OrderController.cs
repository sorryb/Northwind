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


namespace NorthwindWeb.Controllers
{
    
    
    public class OrderController : Controller
    {
        private NorthwindModel db = new NorthwindModel();

        public ActionResult Home1(int? did, int? pid)
        {
            var viewModel = new OrderIndexData();
            viewModel.Order = from s in db.Orders
                              select s;


                //db.Orders
                //.OrderBy(i => i.OrderID);
            if (did != null)
            {
                ViewBag.OrderID = did.Value;
                viewModel.Order_Detail = from s in db.Order_Details where(s.OrderID==did)
                                         select s;
            }
            if (pid != null)
            {
                ViewBag.CourseID = pid.Value;
               
                viewModel.Product = from s in db.Products
                                    where (s.ProductID == pid)
                                    select s;

                //var selectedDetails = viewModel.Order_Detail.Where(x => x.ProductID == pid).Single();
                //db.Entry(selectedDetails).Collection(x => x.Products).Load();
                //foreach (Products produs in selectedDetails.Products)
                //{
                //    db.Entry(produs).Reference(x => x.Order_Details).Load();
                //}

                //viewModel.Product = selectedDetails.Products;
            }

            return View(viewModel);
        }
    }
}
