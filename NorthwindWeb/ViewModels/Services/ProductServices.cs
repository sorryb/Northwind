using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels
{
    /// <summary>
    /// Keeps all products from Services.
    /// </summary>
    public class ProductServices
    {
        public string ProductName { get; set; }
        public int ProductID { get; set; }
        public string CategoryName { get; set; }
        public string CompanyName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public int? UnitsOnOrder { get; set; }
        public ProductServices(){}

    }
}