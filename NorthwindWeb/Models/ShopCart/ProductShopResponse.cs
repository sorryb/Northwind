using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NorthwindWeb.Models.ShopCart
{
    /// <summary>
    /// Contains the response for shopcart from server
    /// </summary>
    public class ProductShopResponse
    {
        /// <summary>
        /// contains all errors
        /// </summary>
        public enum  ErrorType
        {
            NoError,
            UnknownError,
            InvalidProduct
        }

        /// <summary>
        /// true if server cannot send back a proper response
        /// </summary>
        ErrorType Error { get; set; } = ErrorType.NoError;

        /// <summary>
        /// message
        /// </summary>
        string MessageTitle { get; set; }
        string MessageText { get; set; }
        IQueryable<ProductShopCart> ProductList { get; set; }
    }
}