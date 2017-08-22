using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels
{/// <summary>
/// The class used to store, Ten most expensive orders
/// </summary>
    public class Order10
    {
        public int OrderID { get; set; }
        public decimal Cost { get; set; }
        public Order10(){}
    }
}