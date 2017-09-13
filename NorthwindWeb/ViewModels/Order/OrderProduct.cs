using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels.Order
{   /// <summary>
    /// The class used to store, join between the Orders and Orders_Details
    /// </summary>
    public class OrderProduct
    {
        /// <summary>
        /// Product id
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// Name Product
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Product Price
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public short Quantity { get; set; }
        /// <summary>
        /// Discount
        /// </summary>
        public float Discount { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderProduct() { }
    }
}