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
            var order = from o in db.Orders
                        join c in db.Customers on o.CustomerID equals c.CustomerID
                        join s in db.Shippers on o.ShipVia equals s.ShipperID
                        select new { o.OrderID, o.OrderDate, c.CompanyName, ShipperName = s.CompanyName };
            List<Comanda> comenzi = new List<Comanda>();

            foreach (var item in order)
            {
                Comanda x = new Comanda();

                x.OrderID = item.OrderID;
                DateTime t =Convert.ToDateTime(item.OrderDate);
                x.OrderDate = t.Day.ToString() + "." + t.Month + "." + t.Year;
                x.CompanyName = item.CompanyName;
                x.ShipperName = item.ShipperName;
                comenzi.Add(x);

            }

            viewModel.Order = comenzi;

            //if (search!=null)
            //{
            //    viewModel.Order = viewModel.Order.Where(s => s.OrderID==search).OrderBy(x => x.OrderID);
            //}

            //db.Orders
            //.OrderBy(i => i.OrderID);

            var order10 = (from o in db.Orders
                                            join od in db.Order_Details on o.OrderID equals od.OrderID
                                            group od by o.OrderID into x
                                            select new { OrderID = x.Key, Cost = x.Sum(o => o.UnitPrice * o.Quantity) })
                           .OrderByDescending(x => x.Cost)
                           .Take(10);
                           ;
            
            
            List<Order10> list= new List<Order10>();
            
            foreach (var item in order10)
            {
                Order10 x=new Order10();
                
                x.OrderID = item.OrderID;
                x.Cost =decimal.Round(item.Cost,2);
                list.Add(x);
              
            }
         
            viewModel.Order10 = list;




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
