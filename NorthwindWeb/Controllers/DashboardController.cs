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
using PagedList;


namespace NorthwindWeb.Controllers
{   /// <summary>
/// 
/// </summary>
    [Authorize(Roles = "Admins")]
    public class DashboardController : Controller
    {
        private NorthwindModel db = new NorthwindModel();


        /// <summary>
        /// The action that creates the data for the dashboard page
        /// </summary>
        /// <returns></returns>
        public ActionResult Home()
        {
            DashboardIndexData viewModel = new DashboardIndexData();

            viewModel.TotalSalesValue = TotalSalesValue();
            viewModel.NumberProductsSold = NumberProductsSold();
            viewModel.NumberEmployees = NumberEmployees();
            viewModel.NumberCustomers = NumberCustomers();
            viewModel.SalesPerQuarter = SalesByQuarter();
            viewModel.LastTenOrders = LastTen();

            return View(viewModel);
        }
        /// <summary>
        /// Creates a list of data from the database containing the search parameter
        /// </summary>
        /// <param name="search">The parameter to be searched for</param>
        /// <param name="page"></param>
        /// <param name="currentFilter"></param>
        /// <returns></returns>
        
        public ActionResult Search(string search,int? page, string currentFilter)
        {
            var viewModel = new DashboardSearchData();
            // test null if in search control
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            ViewBag.CurrentFilter = search;
            List<LocateSearch> list = new List<LocateSearch>();
            //The test returns false if no search is entered
            if (!String.IsNullOrEmpty(search))
            {//Test each table for match 
                var category = db.Categories;
                foreach (var item in category)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.CategoryID).ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.CategoryID);
                        x.WhereFound = "CategoryID: " +Convert.ToString(item.CategoryID);
                        x.Controller = "Categories";
                        list.Add(x);
                    }
                    else if(item.CategoryName.ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.CategoryID);
                        x.WhereFound = "CategoryName: " + item.CategoryName;
                        x.Controller = "Categories";
                        list.Add(x);
                    }
                    else if(item.Description.ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.CategoryID);
                        x.WhereFound = "Description: " + item.Description;
                        x.Controller = "Categories";
                        list.Add(x);
                    }

                }
                var suppliers = db.Suppliers;
                foreach (var item in suppliers)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.SupplierID).ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.SupplierID);
                        x.WhereFound = "CategoryID: " + Convert.ToString(item.SupplierID);
                        x.Controller = "Suppliers";
                        list.Add(x);
                    }
                    else if (item.CompanyName.ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.SupplierID);
                        x.WhereFound = "CompanyName: " + item.CompanyName;
                        x.Controller = "Suppliers";
                        list.Add(x);
                    }
                    else if (item.ContactName.ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.SupplierID);
                        x.WhereFound = "ContactName: " + item.ContactName;
                        x.Controller = "Suppliers";
                        list.Add(x);
                    }

                }
                var product = db.Products;
                foreach (var item in product)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.ProductID).ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.ProductID);
                        x.WhereFound = "ProductID: " + Convert.ToString(item.ProductID);
                        x.Controller = "Product";
                        list.Add(x);
                    }
                    else if (item.ProductName.ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.ProductID);
                        x.WhereFound = "ProductName: " + item.ProductName;
                        x.Controller = "Product";
                        list.Add(x);
                    }
                 

                }
                var order = db.Orders;
                foreach (var item in order)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.OrderID).ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.OrderID);
                        x.WhereFound = "OrderID: " + Convert.ToString(item.OrderID);
                        x.Controller = "Orders";
                        list.Add(x);
                    }
                    else if (Convert.ToString(item.OrderDate).ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.OrderID);
                        x.WhereFound = "OrderDate: " + Convert.ToString(item.OrderDate);
                        x.Controller = "Orders";
                        list.Add(x);
                    }
                    else if (item.ShipName.ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.OrderID);
                        x.WhereFound = "ShipName: " + item.ShipName;
                        x.Controller = "Orders";
                        list.Add(x);
                    }

                }
                var customers = db.Customers;
                foreach (var item in customers)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.CustomerID).ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.CustomerID);
                        x.WhereFound = "CustomerID: " + Convert.ToString(item.CustomerID);
                        x.Controller = "Customers";
                        list.Add(x);
                    }
                    else if (item.CompanyName.ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.CustomerID);
                        x.WhereFound = "CompanyName: " + item.CompanyName;
                        x.Controller = "Customers";
                        list.Add(x);
                    }
                    else if (item.ContactName.ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.CustomerID);
                        x.WhereFound = "ContactName: " + item.ContactName;
                        x.Controller = "Customers";
                        list.Add(x);
                    }

                }
                var region = db.Regions;
                foreach (var item in region)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.RegionID).ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.RegionID);
                        x.WhereFound = "RegionID: " + Convert.ToString(item.RegionID);
                        x.Controller = "Regions";
                        list.Add(x);
                    }
                    else if (item.RegionDescription.ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.RegionID);
                        x.WhereFound = "RegionDescription: " + item.RegionDescription;
                        x.Controller = "Regions";
                        list.Add(x);
                    }
                    

                }
                var employees = db.Employees;
                foreach (var item in employees)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.EmployeeID).ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.EmployeeID);
                        x.WhereFound = "EmployeeID: " + Convert.ToString(item.EmployeeID);
                        x.Controller = "Employees";
                        list.Add(x);
                    }
                    else if (item.LastName.ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.EmployeeID);
                        x.WhereFound = "LastName: " + item.LastName;
                        x.Controller = "Employees";
                        list.Add(x);
                    }
                    else if (item.FirstName.ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.EmployeeID);
                        x.WhereFound = "FirstName: " + item.FirstName;
                        x.Controller = "Employees";
                        list.Add(x);
                    }

                }
                var shippers = db.Shippers;
                foreach (var item in shippers)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.ShipperID).ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.ShipperID);
                        x.WhereFound = "ShipperID: " + Convert.ToString(item.ShipperID);
                        x.Controller = "Shippers";
                        list.Add(x);
                    }
                    else if (item.CompanyName.ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.ShipperID);
                        x.WhereFound = "CompanyName: " + item.CompanyName;
                        x.Controller = "Shippers";
                        list.Add(x);
                    }


                }
                var territories = db.Territories;
                foreach (var item in territories)
                {
                    LocateSearch x = new LocateSearch();
                    if (Convert.ToString(item.TerritoryID).ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.TerritoryID);
                        x.WhereFound = "TerritoryID: " + Convert.ToString(item.TerritoryID);
                        x.Controller = "Territories";
                        list.Add(x);
                    }
                    else if (item.TerritoryDescription.ToLower().Contains(search.ToLower()))
                    {
                        x.Position = list.Count() + 1;
                        x.ID = Convert.ToString(item.TerritoryID);
                        x.WhereFound = "TerritoryDescription: " + item.TerritoryDescription;
                        x.Controller = "Territories";
                        list.Add(x);
                    }


                }

            }
            //In case he did not find returns Not Found
            if (list.Count < 1)
            {
                LocateSearch x = new LocateSearch();
                x.WhereFound = "Not Found";
                x.Controller = "Dashboard";
                list.Add(x);
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            viewModel.MatchesCount = list.Count();
            viewModel.MatchesFound=list.ToPagedList(pageNumber, pageSize);
            viewModel.MatchesFoundPaged= list.ToPagedList(pageNumber, pageSize);
            return View(viewModel);


           
        }
        /// <summary>
        /// Returns a json with the data required for the MorrisArea table on the dashboard page
        /// </summary>
        /// <returns></returns>
        public ActionResult MorrisArea()
        {
            List<DashboardMorrisArea> list = new List<DashboardMorrisArea>();
            var salesbyyear = from o in db.Orders
                              join od in db.Order_Details on o.OrderID equals od.OrderID
                              select new { od.UnitPrice, od.Quantity, od.Discount, o.OrderDate };
            //Calculates sales each year
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
                //test return true if item->year is not in list
                if (ok == 0)
                {
                    DashboardMorrisArea x = new DashboardMorrisArea();
                    x.Year = Convert.ToString(Convert.ToDateTime(item.OrderDate).Year);
                    x.Sales = item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount));
                    list.Add(x);
                }
            }
            //Rounds the value to two decimal places
            foreach (var i in list)
            {
                i.Sales = decimal.Round(i.Sales, 2);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Returns a json with the data required for the MorrisBar table on the dashboard page
        /// </summary>
        /// <returns></returns>
        public ActionResult MorrisBar()
        {


            return Json(SalesByQuarter(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Returns a json with the data required for the MorrisDonut table on the dashboard page
        /// </summary>
        /// <returns></returns>
        public ActionResult MorrisDonut()
        {
            List<DashboardMorrisDonut> list = new List<DashboardMorrisDonut>();
            var salesbyyear = from od in db.Order_Details
                              join p in db.Products on od.ProductID equals p.ProductID
                              join c in db.Categories on p.CategoryID equals c.CategoryID
                              select new { od.UnitPrice, od.Quantity, od.Discount, c.CategoryName };
            var category = from c in db.Categories
                           select new { c.CategoryName };
            //select all category
            foreach (var item in category)
            {
                DashboardMorrisDonut x = new DashboardMorrisDonut();
                x.label = item.CategoryName;
                x.value = 0;
                list.Add(x);
            }
            //Calculate sales by year for all category
            foreach (var item in salesbyyear)
            {

                foreach (var i in list)
                {
                    if (i.label == item.CategoryName)
                    {
                        i.value += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount));

                        break;
                    }
                }

            }
            //Rounds the value to two decimal places
            foreach (var i in list)
            {
                i.value = decimal.Round(i.value, 2);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        private List<DashboardMorrisBar> SalesByQuarter()
        {
            List<DashboardMorrisBar> list = new List<DashboardMorrisBar>();
            var salesbyyear = from o in db.Orders
                              join od in db.Order_Details on o.OrderID equals od.OrderID
                              select new { od.UnitPrice, od.Quantity, od.Discount, o.OrderDate };
            //Divides year and quarter and calculates sales
            foreach (var item in salesbyyear)
            {
                int quarter;
                //Determine quarter
                if (Convert.ToDateTime(item.OrderDate).Month <= 4) { quarter = 1; }
                else if (Convert.ToDateTime(item.OrderDate).Month <= 8) { quarter = 2; }
                else { quarter = 3; }
                int ok = 0;
                foreach (var i in list)
                {
                    if (int.Parse(i.Year) == Convert.ToDateTime(item.OrderDate).Year)
                    {
                        if (quarter == 1) { i.a += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }
                        else if (quarter == 2) { i.b += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }
                        else { i.c += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }

                        ok = 1;
                        break;
                    }
                }
                //test return true if item->quarter not exist yet in item->year 
                if (ok == 0)
                {
                    DashboardMorrisBar x = new DashboardMorrisBar();
                    x.Year = Convert.ToString(Convert.ToDateTime(item.OrderDate).Year);
                    if (quarter == 1) { x.a = item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }
                    else if (quarter == 2) { x.b = item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }
                    else { x.c = item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount)); }
                    list.Add(x);
                }
            }
            //Rounds the value to two decimal places
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
            decimal d = 0;
            var sales = db.Order_Details;
            foreach (var item in sales)
            {
                d += item.Quantity * item.UnitPrice * (1 - Convert.ToDecimal(item.Discount));

            }
            d = decimal.Round(d, 2);
            return d;
        }

        private int NumberProductsSold()
        {
            int p = 0;
            var product = db.Order_Details;
            foreach (var item in product)
            {
                p += item.Quantity;

            }
            return p;
        }

        private int NumberEmployees()
        {
            int e = db.Employees.Count();
            return e;
        }

        private int NumberCustomers()
        {
            int c = db.Customers.Count();
            return c;
        }

        private List<LastTenOrders> LastTen()
        {
            List<LastTenOrders> list = new List<LastTenOrders>();
            var order = (from o in db.Orders
                         select new { o.OrderID, o.OrderDate }).OrderByDescending(o => o.OrderDate).Take(10);
            foreach (var item in order)
            {
                LastTenOrders x = new LastTenOrders();
                x.OrderID = item.OrderID;
                var y = DateTime.Now - Convert.ToDateTime(item.OrderDate);
                if (y.TotalMinutes <= 60) { x.Ago = "Acum " + Convert.ToString(y.TotalMinutes) + " Minute"; }
                else if (y.TotalHours <= 24) { x.Ago = "Acum " + Convert.ToString(y.TotalHours) + " Ore"; }
                else if (y.TotalDays <= 30) { x.Ago = "Acum " + Convert.ToString(y.TotalHours) + " Zile"; }
                else { x.Ago = "Cu mai mult de o luna in urma"; }
                list.Add(x);
            }
            return list;

        }


    }

}