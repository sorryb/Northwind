using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorthwindWeb.Models;

namespace NorthwindWeb.ViewModels.Orders
{
    /// <summary>
    /// The data model sent by the OrdersController to Home
    /// </summary>
    public class OrderDetali
    {
        public Models.Orders order { get; set; }
        public IEnumerable<DetailsOfOrder> details { get; set; }
    }
}