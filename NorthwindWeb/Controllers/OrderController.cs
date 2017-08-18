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
    
    
    public class OrderController : Controller
    {
        private NorthwindModel db = new NorthwindModel();

        public ActionResult Home1(int? id, int? pid, int? page)
        {
            var viewModel = new OrderIndexData();
            viewModel.Order = db.Orders.OrderBy(x=>x.OrderID);

            //if (search!=null)
            //{
            //    viewModel.Order = viewModel.Order.Where(s => s.OrderID==search).OrderBy(x => x.OrderID);
            //}

            //db.Orders
            //.OrderBy(i => i.OrderID);
            if (id == 0) { id = null; }
            if (id != null)
            {
                ViewBag.OrderID = id.Value;
                viewModel.Order_Detail = db.Order_Details.Where(x => x.OrderID == id);
                                          
                                        
            }
            if (pid == 0) { pid = null; }
            if (pid != null)
            {
                ViewBag.ProductID = pid.Value;

                viewModel.Product = db.Products.Where(x => x.ProductID == pid);
                                 

                //var selectedDetails = viewModel.Order_Detail.Where(x => x.ProductID == pid).Single();
                //db.Entry(selectedDetails).Collection(x => x.Products).Load();
                //foreach (Products produs in selectedDetails.Products)
                //{
                //    db.Entry(produs).Reference(x => x.Order_Details).Load();
                //}

                //viewModel.Product = selectedDetails.Products;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            viewModel.page = viewModel.Order.ToPagedList(pageNumber, pageSize);
            viewModel.Order = viewModel.Order.ToPagedList(pageNumber, pageSize);
            return View(viewModel);
            
        }
    }
}
