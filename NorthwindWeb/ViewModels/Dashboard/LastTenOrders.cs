using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels.Dashboard
{/// <summary>
/// The data required for the table of the last 10 commands in the dashboard
/// </summary>
    public class LastTenOrders
    {
        public int OrderID { get; set; }
        // How long ago the order was placed
        public string Ago { get; set; }
        public LastTenOrders() { }
    }
}