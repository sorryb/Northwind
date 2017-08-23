using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;
using NorthwindWeb.ViewModels.Dashboard;

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