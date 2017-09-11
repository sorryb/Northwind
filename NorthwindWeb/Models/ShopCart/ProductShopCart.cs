using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NorthwindWeb.Models.ShopCart
{
    /// <summary>
    /// contain basic info about shopcart
    /// </summary>
    public class ProductShopCart
    {
        /// <summary>
        /// product id from shopcart
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// quantity of product
        /// </summary>
        public int Quantity { get; set; } = 1;
    }

    /// <summary>
    /// ProductShopCartDetailed have aditional information about the product as against ProductShopCart
    /// </summary>
    public class ProductShopCartDetailed : ProductShopCart
    {

        /// <summary>
        /// Get category of this product
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// get unit price of this product
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// get the category of this product
        /// </summary>
        public string Category { get; set; }

    }
}