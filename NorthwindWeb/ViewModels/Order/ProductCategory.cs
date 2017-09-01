using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels.Order
{/// <summary>
/// The class used to store, join between the Products and Categories
/// </summary>
    public class ProductCategory
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public short? UnitsInStock { get; set; }
    }
}