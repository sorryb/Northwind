using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels
{/// <summary>
/// Order with most products
/// </summary>
    public class BigOrder
    {
        public int OrderID { get; set; }
        public int Produse { get; set; }
        public BigOrder() { }
    }
}