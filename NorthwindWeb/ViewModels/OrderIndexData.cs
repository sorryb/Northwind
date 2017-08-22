﻿using System.Collections.Generic;
using NorthwindWeb.Models;
using PagedList;


namespace NorthwindWeb.ViewModels
{
    public class OrderIndexData
    {   public IPagedList page { get; set; }
        public IEnumerable<Comanda> Order { get; set; }
        public IEnumerable<OrderProduct> Order_Detail { get; set; }
        public IEnumerable<Products> Product { get; set; }
        public IEnumerable<Order10> Order10 { get; set; }
        public BigOrder Comanda { get; set; }
    }
}