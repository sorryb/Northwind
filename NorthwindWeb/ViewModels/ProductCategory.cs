using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels
{
    public class ProductCategory
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public short? UnitsInStock { get; set; }
    }
}