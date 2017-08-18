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
            viewModel.Order = db.Orders
                .OrderBy(i => i.OrderID);
            if (did != null)
            {
                ViewBag.OrderID = did.Value;
                viewModel.Order_Detail = viewModel.Order.Where(i => i.OrderID == did.Value).Single().Order_Details;
            }
            if (pid != null)
            {
                ViewBag.CourseID = pid.Value;
                // Lazy loading
                //viewModel.Enrollments = viewModel.Courses.Where(
                //    x => x.CourseID == courseID).Single().Enrollments;
                // Explicit loading
                var selectedDetails = viewModel.Order_Detail.Where(x => x.ProductID == pid).Single();
                //db.Entry(selectedDetails).Collection(x => x.Products).Load();
                //foreach (Products produs in selectedDetails.Products)
                //{
                //    db.Entry(produs).Reference(x => x.Student).Load();
                //}

                //viewModel.Product = selectedDetails.Products;
            }

            return View(viewModel);
        }
    }
}
