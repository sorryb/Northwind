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

        public ActionResult Home1(int? id, int? pid)
        {
            var viewModel = new OrderIndexData();
            viewModel.Order = db.Orders;
                              


                //db.Orders
                //.OrderBy(i => i.OrderID);
            if (id != null)
            {
                ViewBag.OrderID = id.Value;
                viewModel.Order_Detail = db.Order_Details.Where(x => x.OrderID == id);
                                          
                                        
            }
            if (pid != null)
            {
                ViewBag.CourseID = pid.Value;

                viewModel.Product = db.Products.Where(x => x.ProductID == pid);
                                 

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
