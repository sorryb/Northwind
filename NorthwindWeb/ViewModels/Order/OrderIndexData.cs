using System.Collections.Generic;
using NorthwindWeb.Models;
using PagedList;


namespace NorthwindWeb.ViewModels.Order
{   /// <summary>
    /// The data model sent by the OrderController to Home
    /// </summary>
    public class OrderIndexData
    {
        /// <summary>
        /// Paging the list of OrderInfo
        /// </summary>
        public IPagedList Page { get; set; }
        /// <summary>
        /// List of OrderInfo
        /// </summary>
        public IEnumerable<OrderInfo> Order { get; set; }
        /// <summary>
        /// List of OrderProduct
        /// </summary>
        public IEnumerable<OrderProduct> Order_Detail { get; set; }
        /// <summary>
        /// List of ProductCategory
        /// </summary>
        public IEnumerable<ProductCategory> Product { get; set; }
        /// <summary>
        /// List of OrderTen
        /// </summary>
        public IEnumerable<OrderTen> OrderTen { get; set; }
        /// <summary>
        /// order with the highest number of products
        /// </summary>
        public BigOrder Command { get; set; }
    }
}