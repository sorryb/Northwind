using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels
{/// <summary>
/// The class used to store, join between the Orders and Orders_Details
/// </summary>
    public class OrderProduct
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public OrderProduct() { }
    }
}