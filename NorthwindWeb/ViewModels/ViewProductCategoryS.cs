using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels
{
    /// <summary>
    /// This class is to parse data from products and category table in view
    /// </summary>
    public class ViewProductCategoryS
    {
        //take ProductID, ProductNmae and CategoryName  all as string
        public string ProductID { get; set; }   
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal ProductPrice { get; set; }
        public string SuppliersName { get; set; }
        public string Stock { get; set; }
        public string OnOrder { get; set; }
    }
}