using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NorthwindWeb.Models;
using NorthwindWeb.ViewModels.Dashboard;
using PagedList;
using NorthwindWeb.Context;


namespace NorthwindWeb.Controllers
{


    /// <summary>
    /// DashBoard Controller. Charts with actual state of the site
    /// </summary>
    [Authorize(Roles = "Admins, Managers")]
    public class DashboardController : Controller
    {
        private NorthwindDatabase db = new NorthwindDatabase();
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(DashboardController));  //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.

        /// <summary>
        /// The action that creates the data for the dashboard page
        /// </summary>
        /// <returns>returns a view with a DashboardIndexData model</returns>
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
        /// <returns>Returns a view vith a list of match</returns>
        public ActionResult Search(string search, int? page, string currentFilter)
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
            //The test returns false if no search is entered if the column is not null
            if (!String.IsNullOrEmpty(search))
            {//Test each table for match 
                var category = db.Categories;
                foreach (var itemCategory in category)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(itemCategory.CategoryID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(itemCategory.CategoryID);
                        locationSearch.WhereFound = "CategoryID: " + Convert.ToString(itemCategory.CategoryID);
                        locationSearch.Controller = "Categories";
                        machesFount.Add(locationSearch);
                    }
                    else if (itemCategory.CategoryName != null)
                    {
                        if (itemCategory.CategoryName.ToLower().Contains(search.ToLower()))
                        {
                            locationSearch.Position = machesFount.Count() + 1;
                            locationSearch.ID = Convert.ToString(itemCategory.CategoryID);
                            locationSearch.WhereFound = "CategoryName: " + itemCategory.CategoryName;
                            locationSearch.Controller = "Categories";
                            machesFount.Add(locationSearch);
                        }
                    }
                    else if (itemCategory.Description != null)
                    {
                        if (itemCategory.Description.ToLower().Contains(search.ToLower()))
                        {
                            locationSearch.Position = machesFount.Count() + 1;
                            locationSearch.ID = Convert.ToString(itemCategory.CategoryID);
                            locationSearch.WhereFound = "Description: " + itemCategory.Description;
                            locationSearch.Controller = "Categories";
                            machesFount.Add(locationSearch);
                        }
                    }

                }
                var suppliers = db.Suppliers;
                foreach (var itemSuppliers in suppliers)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(itemSuppliers.SupplierID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(itemSuppliers.SupplierID);
                        locationSearch.WhereFound = "SupplierID: " + Convert.ToString(itemSuppliers.SupplierID);
                        locationSearch.Controller = "Suppliers";
                        machesFount.Add(locationSearch);
                    }
                    else if (itemSuppliers.CompanyName != null)
                    {
                        if (itemSuppliers.CompanyName.ToLower().Contains(search.ToLower()))
                        {
                            locationSearch.Position = machesFount.Count() + 1;
                            locationSearch.ID = Convert.ToString(itemSuppliers.SupplierID);
                            locationSearch.WhereFound = "CompanyName: " + itemSuppliers.CompanyName;
                            locationSearch.Controller = "Suppliers";
                            machesFount.Add(locationSearch);
                        }
                    }
                    else if (itemSuppliers.ContactName != null)
                    {
                        if (itemSuppliers.ContactName.ToLower().Contains(search.ToLower()))
                        {
                            locationSearch.Position = machesFount.Count() + 1;
                            locationSearch.ID = Convert.ToString(itemSuppliers.SupplierID);
                            locationSearch.WhereFound = "ContactName: " + itemSuppliers.ContactName;
                            locationSearch.Controller = "Suppliers";
                            machesFount.Add(locationSearch);
                        }
                    }

                }
                var product = db.Products;
                foreach (var itemProduct in product)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(itemProduct.ProductID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(itemProduct.ProductID);
                        locationSearch.WhereFound = "ProductID: " + Convert.ToString(itemProduct.ProductID);
                        locationSearch.Controller = "Product";
                        machesFount.Add(locationSearch);
                    }
                    else if (itemProduct.ProductName != null)
                    {
                        if (itemProduct.ProductName.ToLower().Contains(search.ToLower()))
                        {
                            locationSearch.Position = machesFount.Count() + 1;
                            locationSearch.ID = Convert.ToString(itemProduct.ProductID);
                            locationSearch.WhereFound = "ProductName: " + itemProduct.ProductName;
                            locationSearch.Controller = "Product";
                            machesFount.Add(locationSearch);
                        }
                    }


                }
                var order = db.Orders;
                foreach (var itemOrder in order)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(itemOrder.OrderID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(itemOrder.OrderID);
                        locationSearch.WhereFound = "OrderID: " + Convert.ToString(itemOrder.OrderID);
                        locationSearch.Controller = "Orders";
                        machesFount.Add(locationSearch);
                    }
                    else if (itemOrder.OrderDate != null)
                    {
                        if (Convert.ToString(itemOrder.OrderDate).ToLower().Contains(search.ToLower()))
                        {
                            locationSearch.Position = machesFount.Count() + 1;
                            locationSearch.ID = Convert.ToString(itemOrder.OrderID);
                            locationSearch.WhereFound = "OrderDate: " + Convert.ToString(itemOrder.OrderDate);
                            locationSearch.Controller = "Orders";
                            machesFount.Add(locationSearch);
                        }
                    }
                    else if (itemOrder.ShipName != null)
                    {
                        if (itemOrder.ShipName.ToLower().Contains(search.ToLower()))
                        {
                            locationSearch.Position = machesFount.Count() + 1;
                            locationSearch.ID = Convert.ToString(itemOrder.OrderID);
                            locationSearch.WhereFound = "ShipName: " + itemOrder.ShipName;
                            locationSearch.Controller = "Orders";
                            machesFount.Add(locationSearch);
                        }
                    }
                }
                var customers = db.Customers;
                foreach (var itemCustomers in customers)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(itemCustomers.CustomerID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(itemCustomers.CustomerID);
                        locationSearch.WhereFound = "CustomerID: " + Convert.ToString(itemCustomers.CustomerID);
                        locationSearch.Controller = "Customers";
                        machesFount.Add(locationSearch);
                    }
                    else if (itemCustomers.CompanyName != null)
                    {
                        if (itemCustomers.CompanyName.ToLower().Contains(search.ToLower()))
                        {
                            locationSearch.Position = machesFount.Count() + 1;
                            locationSearch.ID = Convert.ToString(itemCustomers.CustomerID);
                            locationSearch.WhereFound = "CompanyName: " + itemCustomers.CompanyName;
                            locationSearch.Controller = "Customers";
                            machesFount.Add(locationSearch);
                        }
                    }
                    else if (itemCustomers.ContactName != null)
                    {
                        if (itemCustomers.ContactName.ToLower().Contains(search.ToLower()))
                        {
                            locationSearch.Position = machesFount.Count() + 1;
                            locationSearch.ID = Convert.ToString(itemCustomers.CustomerID);
                            locationSearch.WhereFound = "ContactName: " + itemCustomers.ContactName;
                            locationSearch.Controller = "Customers";
                            machesFount.Add(locationSearch);
                        }
                    }

                }
                var region = db.Regions;
                foreach (var itemRegion in region)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(itemRegion.RegionID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(itemRegion.RegionID);
                        locationSearch.WhereFound = "RegionID: " + Convert.ToString(itemRegion.RegionID);
                        locationSearch.Controller = "Regions";
                        machesFount.Add(locationSearch);
                    }
                    else if (itemRegion.RegionDescription != null)
                    {
                        if (itemRegion.RegionDescription.ToLower().Contains(search.ToLower()))
                        {
                            locationSearch.Position = machesFount.Count() + 1;
                            locationSearch.ID = Convert.ToString(itemRegion.RegionID);
                            locationSearch.WhereFound = "RegionDescription: " + itemRegion.RegionDescription;
                            locationSearch.Controller = "Regions";
                            machesFount.Add(locationSearch);
                        }
                    }


                }
                var employees = db.Employees;
                foreach (var itemEmployees in employees)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(itemEmployees.EmployeeID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(itemEmployees.EmployeeID);
                        locationSearch.WhereFound = "EmployeeID: " + Convert.ToString(itemEmployees.EmployeeID);
                        locationSearch.Controller = "Employees";
                        machesFount.Add(locationSearch);
                    }
                    else if (itemEmployees.LastName != null)
                    {
                        if (itemEmployees.LastName.ToLower().Contains(search.ToLower()))
                        {
                            locationSearch.Position = machesFount.Count() + 1;
                            locationSearch.ID = Convert.ToString(itemEmployees.EmployeeID);
                            locationSearch.WhereFound = "LastName: " + itemEmployees.LastName;
                            locationSearch.Controller = "Employees";
                            machesFount.Add(locationSearch);
                        }
                    }
                    else if (itemEmployees.FirstName != null)
                    {
                        if (itemEmployees.FirstName.ToLower().Contains(search.ToLower()))
                        {
                            locationSearch.Position = machesFount.Count() + 1;
                            locationSearch.ID = Convert.ToString(itemEmployees.EmployeeID);
                            locationSearch.WhereFound = "FirstName: " + itemEmployees.FirstName;
                            locationSearch.Controller = "Employees";
                            machesFount.Add(locationSearch);
                        }
                    }

                }
                var shippers = db.Shippers;
                foreach (var itemShippers in shippers)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(itemShippers.ShipperID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(itemShippers.ShipperID);
                        locationSearch.WhereFound = "ShipperID: " + Convert.ToString(itemShippers.ShipperID);
                        locationSearch.Controller = "Shippers";
                        machesFount.Add(locationSearch);
                    }
                    else if (itemShippers.CompanyName != null)
                    {
                        if (itemShippers.CompanyName.ToLower().Contains(search.ToLower()))
                        {
                            locationSearch.Position = machesFount.Count() + 1;
                            locationSearch.ID = Convert.ToString(itemShippers.ShipperID);
                            locationSearch.WhereFound = "CompanyName: " + itemShippers.CompanyName;
                            locationSearch.Controller = "Shippers";
                            machesFount.Add(locationSearch);
                        }
                    }


                }
                var territories = db.Territories;
                foreach (var itemTerritories in territories)
                {
                    LocateSearch locationSearch = new LocateSearch();
                    if (Convert.ToString(itemTerritories.TerritoryID).ToLower().Contains(search.ToLower()))
                    {
                        locationSearch.Position = machesFount.Count() + 1;
                        locationSearch.ID = Convert.ToString(itemTerritories.TerritoryID);
                        locationSearch.WhereFound = "TerritoryID: " + Convert.ToString(itemTerritories.TerritoryID);
                        locationSearch.Controller = "Territories";
                        machesFount.Add(locationSearch);
                    }
                    else if (itemTerritories.TerritoryDescription != null)
                    {
                        if (itemTerritories.TerritoryDescription.ToLower().Contains(search.ToLower()))
                        {
                            locationSearch.Position = machesFount.Count() + 1;
                            locationSearch.ID = Convert.ToString(itemTerritories.TerritoryID);
                            locationSearch.WhereFound = "TerritoryDescription: " + itemTerritories.TerritoryDescription;
                            locationSearch.Controller = "Territories";
                            machesFount.Add(locationSearch);
                        }
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
            int pageSize;
            try
            {
                pageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["pageSize"]);
            }
            catch
            {
                logger.Error("Exista o eroare in configurare, key pageSize trebuie sa fie un numar");
                pageSize = 10;
            }
            int pageNumber = (page ?? 1);
            viewModel.MatchesCount = machesFount.Count();
            viewModel.MatchesFound = machesFount.ToPagedList(pageNumber, pageSize);
            viewModel.MatchesFoundPaged = machesFount.ToPagedList(pageNumber, pageSize);
            return View(viewModel);



        }
        /// <summary>
        /// Returns a json with the data required for the MorrisArea table on the dashboard page
        /// </summary>
        /// <returns>returns a json with the data for the MorrisArea table</returns>
        public JsonResult MorrisArea()
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
            dashboardMorrisAreaData = dashboardMorrisAreaData.OrderBy(area => Convert.ToInt32(area.Year)).ToList();
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
        /// <returns>returns a json with the data for the MorrisBar table</returns>
        public JsonResult MorrisBar()
        {


            return Json(SalesByQuarter(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Returns a json with the data required for the MorrisDonut table on the dashboard page
        /// </summary>
        /// <returns>returns a json with the data for the MorrisDonut table</returns>
        public JsonResult MorrisDonut()
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
                        if (quarter == 1) { itemDashboardMorrisBarData.dashboardMorrisBarColumA += itemSalesByYear.Quantity * itemSalesByYear.UnitPrice * (1 - Convert.ToDecimal(itemSalesByYear.Discount)); }
                        else if (quarter == 2) { itemDashboardMorrisBarData.dashboardMorrisBarColumB += itemSalesByYear.Quantity * itemSalesByYear.UnitPrice * (1 - Convert.ToDecimal(itemSalesByYear.Discount)); }
                        else { itemDashboardMorrisBarData.dashboardMorrisBarColumC += itemSalesByYear.Quantity * itemSalesByYear.UnitPrice * (1 - Convert.ToDecimal(itemSalesByYear.Discount)); }

                        alreadyExistQuarter = 1;
                        break;
                    }

                }
                //test return true if itemSalesByYear->quarter not exist yet in itemDashboardMorrisBarData->year 
                if (alreadyExistQuarter == 0)
                {
                    DashboardMorrisBar dashboardMorrisBarElement = new DashboardMorrisBar();
                    dashboardMorrisBarElement.Year = Convert.ToString(Convert.ToDateTime(itemSalesByYear.OrderDate).Year);
                    if (quarter == 1) { dashboardMorrisBarElement.dashboardMorrisBarColumA = itemSalesByYear.Quantity * itemSalesByYear.UnitPrice * (1 - Convert.ToDecimal(itemSalesByYear.Discount)); }
                    else if (quarter == 2) { dashboardMorrisBarElement.dashboardMorrisBarColumB = itemSalesByYear.Quantity * itemSalesByYear.UnitPrice * (1 - Convert.ToDecimal(itemSalesByYear.Discount)); }
                    else { dashboardMorrisBarElement.dashboardMorrisBarColumC = itemSalesByYear.Quantity * itemSalesByYear.UnitPrice * (1 - Convert.ToDecimal(itemSalesByYear.Discount)); }
                    dashboardMorrisBarData.Add(dashboardMorrisBarElement);
                }
            }
            dashboardMorrisBarData = dashboardMorrisBarData.OrderBy(bar => Convert.ToInt32(bar.Year)).ToList();
            //Rounds the value to two decimal places
            foreach (var itemDashboardMorrisBarData in dashboardMorrisBarData)
            {
                {
                    itemDashboardMorrisBarData.dashboardMorrisBarColumA = decimal.Round(itemDashboardMorrisBarData.dashboardMorrisBarColumA, 2);
                    itemDashboardMorrisBarData.dashboardMorrisBarColumB = decimal.Round(itemDashboardMorrisBarData.dashboardMorrisBarColumB, 2);
                    itemDashboardMorrisBarData.dashboardMorrisBarColumC = decimal.Round(itemDashboardMorrisBarData.dashboardMorrisBarColumC, 2);
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
            //format the message according to how long it has passed from placing orders
            foreach (var itemOrder in order)
            {
                LastTenOrders lastTenOrdersElement = new LastTenOrders();
                lastTenOrdersElement.OrderID = itemOrder.OrderID;
                var pastTime = DateTime.Now - Convert.ToDateTime(itemOrder.OrderDate);
                if (pastTime.TotalMinutes <= 60) { lastTenOrdersElement.Ago = "Acum " + Convert.ToString(decimal.Round(Convert.ToDecimal(pastTime.TotalMinutes), 0)) + " de Minute"; }
                else if (pastTime.TotalHours <= 24) { lastTenOrdersElement.Ago = "Acum " + Convert.ToString(decimal.Round(Convert.ToDecimal(pastTime.TotalHours), 0)) + " Ore"; }
                else if (pastTime.TotalDays <= 30) { lastTenOrdersElement.Ago = "Acum " + Convert.ToString(decimal.Round(Convert.ToDecimal(pastTime.TotalDays), 0)) + " Zile"; }
                else { lastTenOrdersElement.Ago = "Cu mai mult de o luna in urma"; }
                lastTenOrdersData.Add(lastTenOrdersElement);
            }
            return lastTenOrdersData;

        }


    }

}