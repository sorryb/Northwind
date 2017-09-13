using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels.Order
{   /// <summary>
    /// Order with most products
    /// </summary>
    public class BigOrder
    {
        /// <summary>
        /// Order id
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// Number Of Product
        /// </summary>
        public int NumberOfProduct { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public BigOrder() { }
    }
}