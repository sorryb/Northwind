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



        public ActionResult Index()
        {
            var viewModel = new DashboardIndexData();
            viewModel.TotalSalesValue = TotalSalesValue();
            viewModel.NumberProductsSold = NumberProductsSold();
            viewModel.NumberEmployees = NumberEmployees();
            viewModel.NumberCustomers = NumberCustomers();
           
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

        
    }

}