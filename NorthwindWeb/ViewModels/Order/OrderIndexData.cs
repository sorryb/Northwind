using System.Collections.Generic;
using NorthwindWeb.Models;
using PagedList;


namespace NorthwindWeb.ViewModels.Order
{/// <summary>
/// The data model sent by the OrderController to Home
/// </summary>
    public class OrderIndexData
    {   public IPagedList Page { get; set; }
        public IEnumerable<OrderInfo> Order { get; set; }
        public IEnumerable<OrderProduct> Order_Detail { get; set; }
        public IEnumerable<ProductCategory> Product { get; set; }
        public IEnumerable<OrderTen> OrderTen { get; set; }
        public BigOrder Command { get; set; }
    }
}