using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;

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
        public DataContractJsonSerializer Graph1 { get; set; }


    }
}