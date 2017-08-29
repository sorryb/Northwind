using System.Collections.Generic;
using NorthwindWeb.Models;
using PagedList;


namespace NorthwindWeb.ViewModels
{/// <summary>
/// The data model sent by the OrderController to Home1
/// </summary>
    public class OrderIndexData
    {   public IPagedList page { get; set; }
        public IEnumerable<OrderInfo> Order { get; set; }
        public IEnumerable<OrderProduct> Order_Detail { get; set; }
        public IEnumerable<ProductCategory> Product { get; set; }
        public IEnumerable<OrderTen> Order10 { get; set; }
        public BigOrder Comand { get; set; }
    }
}