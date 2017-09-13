using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels.Orders
{
    /// <summary>
    /// Keeps all order-details 
    /// </summary>
    public class DetailsOfOrder
    {
        /// <summary>
        /// The id of the order.
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// The id of the product.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// The unit price of product.
        /// </summary>
        public decimal? UnitPrice { get; set; }

        /// <summary>
        /// The quantity of the product.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The discount of the product.
        /// </summary>
        public float Discount { get; set; }

        /// <summary>
        /// Default constructor. Initialises new empty instance.
        /// </summary>
        public DetailsOfOrder() { }
    }
}