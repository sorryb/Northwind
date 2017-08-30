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
            viewModel.LastTenOrders = LastTenOrder();

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
            List<LocateSearch> machesFount = new List<LocateSearch>();
            //The test returns false if no search is entered
            if (!String.IsNullOrEmpty(search))
            {//Test each table for match 
                var category = db.Categories;
                foreach (var item in category)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(item.CategoryID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.CategoryID);
                        locationSearch.WhereFound = "CategoryID: " +Convert.ToString(item.CategoryID);
                        locationSearch.Controller = "Categories";
                        machesFount.Add(locationSearch);
                    }
                    else if(item.CategoryName.ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.CategoryID);
                        locationSearch.WhereFound = "CategoryName: " + item.CategoryName;
                        locationSearch.Controller = "Categories";
                        machesFount.Add(locationSearch);
                    }
                    else if(item.Description.ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.CategoryID);
                        locationSearch.WhereFound = "Description: " + item.Description;
                        locationSearch.Controller = "Categories";
                        machesFount.Add(locationSearch);
                    }

                }
                var suppliers = db.Suppliers;
                foreach (var item in suppliers)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(item.SupplierID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.SupplierID);
                        locationSearch.WhereFound = "CategoryID: " + Convert.ToString(item.SupplierID);
                        locationSearch.Controller = "Suppliers";
                        machesFount.Add(locationSearch);
                    }
                    else if (item.CompanyName.ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.SupplierID);
                        locationSearch.WhereFound = "CompanyName: " + item.CompanyName;
                        locationSearch.Controller = "Suppliers";
                        machesFount.Add(locationSearch);
                    }
                    else if (item.ContactName.ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.SupplierID);
                        locationSearch.WhereFound = "ContactName: " + item.ContactName;
                        locationSearch.Controller = "Suppliers";
                        machesFount.Add(locationSearch);
                    }

                }
                var product = db.Products;
                foreach (var item in product)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(item.ProductID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.ProductID);
                        locationSearch.WhereFound = "ProductID: " + Convert.ToString(item.ProductID);
                        locationSearch.Controller = "Product";
                        machesFount.Add(locationSearch);
                    }
                    else if (item.ProductName.ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.ProductID);
                        locationSearch.WhereFound = "ProductName: " + item.ProductName;
                        locationSearch.Controller = "Product";
                        machesFount.Add(locationSearch);
                    }
                 

                }
                var order = db.Orders;
                foreach (var item in order)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(item.OrderID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.OrderID);
                        locationSearch.WhereFound = "OrderID: " + Convert.ToString(item.OrderID);
                        locationSearch.Controller = "Orders";
                        machesFount.Add(locationSearch);
                    }
                    else if (Convert.ToString(item.OrderDate).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.OrderID);
                        locationSearch.WhereFound = "OrderDate: " + Convert.ToString(item.OrderDate);
                        locationSearch.Controller = "Orders";
                        machesFount.Add(locationSearch);
                    }
                    else if (item.ShipName.ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.OrderID);
                        locationSearch.WhereFound = "ShipName: " + item.ShipName;
                        locationSearch.Controller = "Orders";
                        machesFount.Add(locationSearch);
                    }

                }
                var customers = db.Customers;
                foreach (var item in customers)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(item.CustomerID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.CustomerID);
                        locationSearch.WhereFound = "CustomerID: " + Convert.ToString(item.CustomerID);
                        locationSearch.Controller = "Customers";
                        machesFount.Add(locationSearch);
                    }
                    else if (item.CompanyName.ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.CustomerID);
                        locationSearch.WhereFound = "CompanyName: " + item.CompanyName;
                        locationSearch.Controller = "Customers";
                        machesFount.Add(locationSearch);
                    }
                    else if (item.ContactName.ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.CustomerID);
                        locationSearch.WhereFound = "ContactName: " + item.ContactName;
                        locationSearch.Controller = "Customers";
                        machesFount.Add(locationSearch);
                    }

                }
                var region = db.Regions;
                foreach (var item in region)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(item.RegionID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.RegionID);
                        locationSearch.WhereFound = "RegionID: " + Convert.ToString(item.RegionID);
                        locationSearch.Controller = "Regions";
                        machesFount.Add(locationSearch);
                    }
                    else if (item.RegionDescription.ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.RegionID);
                        locationSearch.WhereFound = "RegionDescription: " + item.RegionDescription;
                        locationSearch.Controller = "Regions";
                        machesFount.Add(locationSearch);
                    }
                    

                }
                var employees = db.Employees;
                foreach (var item in employees)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(item.EmployeeID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.EmployeeID);
                        locationSearch.WhereFound = "EmployeeID: " + Convert.ToString(item.EmployeeID);
                        locationSearch.Controller = "Employees";
                        machesFount.Add(locationSearch);
                    }
                    else if (item.LastName.ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.EmployeeID);
                        locationSearch.WhereFound = "LastName: " + item.LastName;
                        locationSearch.Controller = "Employees";
                        machesFount.Add(locationSearch);
                    }
                    else if (item.FirstName.ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.EmployeeID);
                        locationSearch.WhereFound = "FirstName: " + item.FirstName;
                        locationSearch.Controller = "Employees";
                        machesFount.Add(locationSearch);
                    }

                }
                var shippers = db.Shippers;
                foreach (var item in shippers)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(item.ShipperID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.ShipperID);
                        locationSearch.WhereFound = "ShipperID: " + Convert.ToString(item.ShipperID);
                        locationSearch.Controller = "Shippers";
                        machesFount.Add(locationSearch);
                    }
                    else if (item.CompanyName.ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.ShipperID);
                        locationSearch.WhereFound = "CompanyName: " + item.CompanyName;
                        locationSearch.Controller = "Shippers";
                        machesFount.Add(locationSearch);
                    }


                }
                var territories = db.Territories;
                foreach (var item in territories)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(item.TerritoryID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.TerritoryID);
                        locationSearch.WhereFound = "TerritoryID: " + Convert.ToString(item.TerritoryID);
                        locationSearch.Controller = "Territories";
                        machesFount.Add(locationSearch);
                    }
                    else if (item.TerritoryDescription.ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(item.TerritoryID);
                        locationSearch.WhereFound = "TerritoryDescription: " + item.TerritoryDescription;
                        locationSearch.Controller = "Territories";
                        machesFount.Add(locationSearch);
                    }


                }

            }
            //In case he did not find returns Not Found
            if (machesFount.Count < 1)
            {
                LocateSearch notMachesFount = new LocateSearch();
                notMachesFount.WhereFound = "Not Found";
                notMachesFount.Controller = "Dashboard";
                machesFount.Add(notMachesFount);
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            viewModel.MatchesCount = machesFount.Count();
            viewModel.MatchesFound=machesFount.ToPagedList(pageNumber, pageSize);
            viewModel.MatchesFoundPaged= machesFount.ToPagedList(pageNumber, pageSize);
            return View(viewModel);


           
        }
        /// <summary>
        /// Returns a json with the data required for the MorrisArea table on the dashboard page
        /// </summary>
        /// <returns></returns>
        public ActionResult MorrisArea()
        {
            List<DashboardMorrisArea> dashboardMorrisAreaData = new List<DashboardMorrisArea>();
            var salesByYear = from o in db.Orders
                              join od in db.Order_Details on o.OrderID equals od.OrderID
                              select new { od.UnitPrice, od.Quantity, od.Discount, o.OrderDate };
            //Calculates sales each year
            foreach (var itemSalesByYear in salesByYear)
            {
                int ok = 0;
                foreach (var i in dashboardMorrisAreaData)
                {
                    if (int.Parse(i.Year) == Convert.ToDateTime(itemSalesByYear.OrderDate).Year)
                    {
                        i.Sales += itemSalesByYear.Quantity * itemSalesByYear.UnitPrice * (1 - Convert.ToDecimal(itemSalesByYear.Discount));
                        ok = 1;
                        break;
                    }
                }
                //test return true if item->year is not in list
                if (ok == 0)
                {
                    DashboardMorrisArea dashboardMorrisAreaElement = new DashboardMorrisArea();
                    dashboardMorrisAreaElement.Year = Convert.ToString(Convert.ToDateTime(itemSalesByYear.OrderDate).Year);
                    dashboardMorrisAreaElement.Sales = itemSalesByYear.Quantity * itemSalesByYear.UnitPrice * (1 - Convert.ToDecimal(itemSalesByYear.Discount));
                    dashboardMorrisAreaData.Add(dashboardMorrisAreaElement);
                }
            }
            //Rounds the value to two decimal places
            foreach (var itemDashboardMorrisAreaData in dashboardMorrisAreaData)
            {
                itemDashboardMorrisAreaData.Sales = decimal.Round(itemDashboardMorrisAreaData.Sales, 2);
            }

            return Json(dashboardMorrisAreaData, JsonRequestBehavior.AllowGet);
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
            List<DashboardMorrisDonut> dashboardMorrisDonutData = new List<DashboardMorrisDonut>();
            var salesByYear = from od in db.Order_Details
                              join p in db.Products on od.ProductID equals p.ProductID
                              join c in db.Categories on p.CategoryID equals c.CategoryID
                              select new { od.UnitPrice, od.Quantity, od.Discount, c.CategoryName };
            var category = from c in db.Categories
                           select new { c.CategoryName };
            //select all category
            foreach (var itemCategory in category)
            {
                DashboardMorrisDonut dashboardMorrisDonutElement = new DashboardMorrisDonut();
                dashboardMorrisDonutElement.label = itemCategory.CategoryName;
                dashboardMorrisDonutElement.value = 0;
                dashboardMorrisDonutData.Add(dashboardMorrisDonutElement);
            }
            //Calculate sales by year for all category
            foreach (var itemSalesByYear in salesByYear)
            {

                foreach (var itemDashboardMorrisDonutData in dashboardMorrisDonutData)
                {
                    if (itemDashboardMorrisDonutData.label == itemSalesByYear.CategoryName)
                    {
                        itemDashboardMorrisDonutData.value += itemSalesByYear.Quantity * itemSalesByYear.UnitPrice * (1 - Convert.ToDecimal(itemSalesByYear.Discount));

                        break;
                    }
                }

            }
            //Rounds the value to two decimal places
            foreach (var itemDashboardMorrisDonutData in dashboardMorrisDonutData)
            {
                itemDashboardMorrisDonutData.value = decimal.Round(itemDashboardMorrisDonutData.value, 2);
            }

            return Json(dashboardMorrisDonutData, JsonRequestBehavior.AllowGet);
        }

        private List<DashboardMorrisBar> SalesByQuarter()
        {
            List<DashboardMorrisBar> dashboardMorrisBarData = new List<DashboardMorrisBar>();
            var salesByYear = from o in db.Orders
                              join od in db.Order_Details on o.OrderID equals od.OrderID
                              select new { od.UnitPrice, od.Quantity, od.Discount, o.OrderDate };
            //Divides year and quarter and calculates sales
            foreach (var itemSalesByYear in salesByYear)
            {
                int quarter;
                //Determine quarter
                if (Convert.ToDateTime(itemSalesByYear.OrderDate).Month <= 4) { quarter = 1; }
                else if (Convert.ToDateTime(itemSalesByYear.OrderDate).Month <= 8) { quarter = 2; }
                else { quarter = 3; }
                int alreadyExistQuarter = 0;
                foreach (var itemDashboardMorrisBarData in dashboardMorrisBarData)
                {
                    if (int.Parse(itemDashboardMorrisBarData.Year) == Convert.ToDateTime(itemSalesByYear.OrderDate).Year)
                    {
                        if (quarter == 1) { itemDashboardMorrisBarData.a += itemSalesByYear.Quantity * itemSalesByYear.UnitPrice * (1 - Convert.ToDecimal(itemSalesByYear.Discount)); }
                        else if (quarter == 2) { itemDashboardMorrisBarData.b += itemSalesByYear.Quantity * itemSalesByYear.UnitPrice * (1 - Convert.ToDecimal(itemSalesByYear.Discount)); }
                        else { itemDashboardMorrisBarData.c += itemSalesByYear.Quantity * itemSalesByYear.UnitPrice * (1 - Convert.ToDecimal(itemSalesByYear.Discount)); }

                        alreadyExistQuarter = 1;
                        break;
                    }
                }
                //test return true if itemSalesByYear->quarter not exist yet in itemDashboardMorrisBarData->year 
                if (alreadyExistQuarter == 0)
                {
                    DashboardMorrisBar dashboardMorrisBarElement = new DashboardMorrisBar();
                    dashboardMorrisBarElement.Year = Convert.ToString(Convert.ToDateTime(itemSalesByYear.OrderDate).Year);
                    if (quarter == 1) { dashboardMorrisBarElement.a = itemSalesByYear.Quantity * itemSalesByYear.UnitPrice * (1 - Convert.ToDecimal(itemSalesByYear.Discount)); }
                    else if (quarter == 2) { dashboardMorrisBarElement.b = itemSalesByYear.Quantity * itemSalesByYear.UnitPrice * (1 - Convert.ToDecimal(itemSalesByYear.Discount)); }
                    else { dashboardMorrisBarElement.c = itemSalesByYear.Quantity * itemSalesByYear.UnitPrice * (1 - Convert.ToDecimal(itemSalesByYear.Discount)); }
                    dashboardMorrisBarData.Add(dashboardMorrisBarElement);
                }
            }
            //Rounds the value to two decimal places
            foreach (var itemDashboardMorrisBarData in dashboardMorrisBarData)
            {
                {
                    itemDashboardMorrisBarData.a = decimal.Round(itemDashboardMorrisBarData.a, 2);
                    itemDashboardMorrisBarData.b = decimal.Round(itemDashboardMorrisBarData.b, 2);
                    itemDashboardMorrisBarData.c = decimal.Round(itemDashboardMorrisBarData.c, 2);
                }
            }
            return dashboardMorrisBarData;
        }

        private decimal TotalSalesValue()
        {
            decimal totalSalesValue = 0;
            var sales = db.Order_Details;
            foreach (var itemSales in sales)
            {
                totalSalesValue += itemSales.Quantity * itemSales.UnitPrice * (1 - Convert.ToDecimal(itemSales.Discount));

            }
            totalSalesValue = decimal.Round(totalSalesValue, 2);
            return totalSalesValue;
        }

        private int NumberProductsSold()
        {
            int numberProductsSold = 0;
            var product = db.Order_Details;
            foreach (var itemProduct in product)
            {
                numberProductsSold += itemProduct.Quantity;

            }
            return numberProductsSold;
        }

        private int NumberEmployees()
        {
            return db.Employees.Count();
        }

        private int NumberCustomers()
        {
           return db.Customers.Count();
        }

        private List<LastTenOrders> LastTenOrder()
        {
            List<LastTenOrders> lastTenOrdersData = new List<LastTenOrders>();
            var order = (from o in db.Orders
                         select new { o.OrderID, o.OrderDate }).OrderByDescending(o => o.OrderDate).Take(10);
            foreach (var itemOrder in order)
            {
                LastTenOrders lastTenOrdersElement = new LastTenOrders();
                lastTenOrdersElement.OrderID = itemOrder.OrderID;
                var pastTime = DateTime.Now - Convert.ToDateTime(itemOrder.OrderDate);
                if (pastTime.TotalMinutes <= 60) { lastTenOrdersElement.Ago = "Acum " + Convert.ToString(pastTime.TotalMinutes) + " Minute"; }
                else if (pastTime.TotalHours <= 24) { lastTenOrdersElement.Ago = "Acum " + Convert.ToString(pastTime.TotalHours) + " Ore"; }
                else if (pastTime.TotalDays <= 30) { lastTenOrdersElement.Ago = "Acum " + Convert.ToString(pastTime.TotalHours) + " Zile"; }
                else { lastTenOrdersElement.Ago = "Cu mai mult de o luna in urma"; }
                lastTenOrdersData.Add(lastTenOrdersElement);
            }
            return lastTenOrdersData;

        }


    }

}