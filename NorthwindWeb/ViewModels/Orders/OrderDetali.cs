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
        /// <summary>
        /// Display the order. 
        /// </summary>
        public Models.Orders Order { get; set; }

        /// <summary>
        /// Dispay the details of the order.
        /// </summary>
        public IEnumerable<DetailsOfOrder> Details { get; set; }
    }
}