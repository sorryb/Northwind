using System.Collections.Generic;
using NorthwindWeb.Models;

namespace NorthwindWeb.ViewModels
{
    public class OrderIndexData
    {
        public IEnumerable<Orders> Order { get; set; }
        public IEnumerable<Order_Details> Order_Detail { get; set; }
        public IEnumerable<Products> Product { get; set; }
    }
}