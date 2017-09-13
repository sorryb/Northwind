using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Mvc;

namespace NorthwindWeb.ViewModels.Dashboard
{   /// <summary>
    /// The data model sent by the DashboardController to Home
    /// </summary>
    public class DashboardIndexData
    {   /// <summary>
        /// sales value
        /// </summary>
        public decimal TotalSalesValue { get; set; }
        /// <summary>
        /// Number Products Sold
        /// </summary>
        public int NumberProductsSold{get;set;}
        /// <summary>
        /// Number of Employees
        /// </summary>
        public int NumberEmployees { get; set; }
        /// <summary>
        /// Number of Customers
        /// </summary>
        public int NumberCustomers { get; set; }
        /// <summary>
        /// Sales Per Quarter
        /// </summary>
        public IEnumerable<DashboardMorrisBar> SalesPerQuarter { get; set; }
        /// <summary>
        /// Last Ten Orders
        /// </summary>
        public IEnumerable<LastTenOrders> LastTenOrders { get; set; }

    }
}