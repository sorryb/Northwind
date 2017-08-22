using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels
{
    public class ViewProductCategoryS
    {
        //take ProductID, ProductNmae and CategoryName  all as string
        public string ProductID { get; set; }   
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ProductPrice { get; set; }
    }
}