using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels.Order
{   /// <summary>
    /// The class used to store, Ten most expensive orders
    /// </summary>
    public class OrderTen
    {
        /// <summary>
        /// Order id
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// Order value
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderTen(){}
    }
}