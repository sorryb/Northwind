using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels.Orders
{
    public class DetailsOfOrder
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal? UnitPrice { get; set; }
        public int Quantity { get; set; }
        public DetailsOfOrder() { }
    }
}