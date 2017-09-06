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
        private NorthwindModel dbContext = null;

        /// <summary>
        /// ProductShopCartDerailed
        /// </summary>
        /// <param name="northwindLocalContext">Curent context of northwind database</param>
        public ProductShopCartDetailed(NorthwindModel northwindLocalContext)
        {
            this.dbContext = northwindLocalContext;
        }
        /// <summary>
        /// Get category of this product
        /// </summary>
        string Category
        {
            get
            {
                return dbContext.Products.Include(Category).Where(x => x.ProductID == ID).Select(x => x.Category.CategoryName).FirstOrDefault();
            }
        }

        /// <summary>
        /// get unit price of this product
        /// </summary>
        decimal UnitPrice
        {
            get
            {
                return dbContext.Products.Where(x => x.ProductID == ID).Select(x => x.UnitPrice).FirstOrDefault() ?? 9999999;
            }
        }

    }
}