using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;
using NorthwindWeb.ViewModels.Dashboard;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace NorthwindWeb.Controllers
{
    public class DashboardController: Controller
    {
        private NorthwindModel db = new NorthwindModel();
        


        public ActionResult Index1()
        {
            DashboardIndexData viewModel = new DashboardIndexData();

            viewModel.TotalSalesValue = TotalSalesValue();
            viewModel.NumberProductsSold = NumberProductsSold();
            viewModel.NumberEmployees = NumberEmployees();
            viewModel.NumberCustomers = NumberCustomers();
            viewModel.Tabel = Table();
            viewModel.LastTen = LastTen();

            return View(viewModel);
        }

        public ActionResult Graph1()
        {
            List<DashboardGraph1> list = new List<DashboardGraph1>();
            var salesbyyear = from o in db.Orders
                              join od in db.Order_Details on o.OrderID equals od.OrderID
                              select new { od.UnitPrice, od.Quantity, od.Discount, o.OrderDate };
            foreach (var item in salesbyyear)
            {
                int ok = 0;
                foreach (var i in list)
                {
                    if (int.Parse(i.Year) == Convert.ToDateTime(item.OrderDate).Year)
                    {
                        i.Sales += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount));
                        ok = 1;
                        break;
                    }
                }
                if (ok == 0)
                {
                    DashboardGraph1 x = new DashboardGraph1();
                    x.Year = Convert.ToString(Convert.ToDateTime(item.OrderDate).Year);
                    x.Sales = item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount));
                    list.Add(x);
                }
            }
            foreach (var i in list)
            {
                i.Sales = decimal.Round(i.Sales, 2);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Graph2()
        {
            
            
            return Json(Table(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Graph3()
        {
            List<DashboardGraph3> list = new List<DashboardGraph3>();
            var salesbyyear = from od in db.Order_Details
                              join p in db.Products on od.ProductID equals p.ProductID
                              join c in db.Categories on p.CategoryID equals c.CategoryID
                              select new { od.UnitPrice, od.Quantity, od.Discount,c.CategoryName };
            foreach (var item in salesbyyear)
            {
                int ok = 0;
                foreach (var i in list)
                {
                    if (i.label == item.CategoryName)
                    {
                        i.value += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount));
                        ok = 1;
                        break;
                    }
                }
                if (ok == 0)
                {
                    DashboardGraph3 x = new DashboardGraph3();
                    x.label = item.CategoryName;
                    x.value = item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount));
                    list.Add(x);
                }
            }
            foreach (var i in list)
            {
                i.value = decimal.Round(i.value, 2);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        private List<DashboardGraph2> Table()
        {
            List<DashboardGraph2> list = new List<DashboardGraph2>();
            var salesbyyear = from o in db.Orders
                              join od in db.Order_Details on o.OrderID equals od.OrderID
                              select new { od.UnitPrice, od.Quantity, od.Discount, o.OrderDate };
            foreach (var item in salesbyyear)
            {
                int t;
                if (Convert.ToDateTime(item.OrderDate).Month <= 4) { t = 1; }
                else if (Convert.ToDateTime(item.OrderDate).Month <= 8) { t = 2; }
                else { t = 3; }
                int ok = 0;
                foreach (var i in list)
                {
                    if (int.Parse(i.Year) == Convert.ToDateTime(item.OrderDate).Year)
                    {
                        if (t == 1) { i.a += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }
                        else if (t == 2) { i.b += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }
                        else { i.c += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }

                        ok = 1;
                        break;
                    }
                }
                if (ok == 0)
                {
                    DashboardGraph2 x = new DashboardGraph2();
                    x.Year = Convert.ToString(Convert.ToDateTime(item.OrderDate).Year);
                    if (t == 1) { x.a = item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }
                    else if (t == 2) { x.b = item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }
                    else { x.c = item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }
                    list.Add(x);
                }
            }
            foreach (var i in list)
            {
                {
                    i.a = decimal.Round(i.a, 2);
                    i.b = decimal.Round(i.b, 2);
                    i.c = decimal.Round(i.c, 2);
                }
            }
            return list;
        }

        private decimal TotalSalesValue()
        {
            decimal d=0;
            var sales = db.Order_Details;
            foreach( var item in sales)
            {
                d += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount));

            }
            d=decimal.Round(d, 2);
            return d;
        }

        private int NumberProductsSold()
        {
            int p=0;
            var product = db.Order_Details;
            foreach (var item in product)
            {
                p += item.Quantity;

            }
            return p;
        }

        private int NumberEmployees()
        {
            int e =db.Employees.Count();
            return e;
        }
        
        private int NumberCustomers()
        {
            int c = db.Customers.Count();
            return c;
        }

        private List<Last10Orders> LastTen()
        {
            List<Last10Orders> list = new List<Last10Orders>();
            var order = (from o in db.Orders
                         select new { o.OrderID,o.OrderDate}).OrderByDescending(o => o.OrderDate).Take(10);
            foreach(var item in order)
            {
                Last10Orders x = new Last10Orders();
                x.OrderID = item.OrderID;
                var y = DateTime.Now - Convert.ToDateTime(item.OrderDate);
                if (y.TotalMinutes <= 60) { x.Ago ="Acum "+ Convert.ToString(y.TotalMinutes) + " Minute"; }
                else if(y.TotalHours <= 24) { x.Ago = "Acum " + Convert.ToString(y.TotalHours) + " Ore"; }
                else if (y.TotalDays <= 30) { x.Ago = "Acum " + Convert.ToString(y.TotalHours) + " Zile"; }
                else { x.Ago = "Cu mai mult de o luna in urma"; }
                list.Add(x);
            }
            return list;

        }

      
    }

}