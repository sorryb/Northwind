using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels.Dashboard
{
    public class Last10Orders
    {
        public int OrderID { get; set; }
        public string Ago { get; set; }
        public Last10Orders() { }
    }
}