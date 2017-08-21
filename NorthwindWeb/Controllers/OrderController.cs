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
            var order10 = (from o in db.Orders
                           join od in db.Order_Details on o.OrderID equals od.OrderID
                           group od by o.OrderID into x
                           select new { OrderID = x.Key, total = x.Sum(o => o.UnitPrice * o.Quantity * Convert.ToDecimal(1 - o.Discount)) })
                           .OrderByDescending(x=>x.total)
                           .Take(10);
                        
                        

            
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
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            viewModel.page = viewModel.Order.ToPagedList(pageNumber, pageSize);
            viewModel.Order = viewModel.Order.ToPagedList(pageNumber, pageSize);
            return View(viewModel);
            
        }
    }
}
