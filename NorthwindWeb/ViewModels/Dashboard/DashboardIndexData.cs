using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Mvc;

namespace NorthwindWeb.ViewModels.Dashboard
{/// <summary>
/// The data model sent by the DashboardController to Index
/// </summary>
    public class DashboardIndexData
    {
        public decimal TotalSalesValue { get; set; }
        public int NumberProductsSold{get;set;}
        public int NumberEmployees { get; set; }
        public int NumberCustomers { get; set; }
        public IEnumerable<DashboardMorrisBar> Tabel { get; set; }
        public IEnumerable<Last10Orders> LastTen { get; set; }

    }
}