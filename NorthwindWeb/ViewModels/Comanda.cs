using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels
{
    public class Comanda
    {
        public int OrderID { get; set; }
        public string OrderDate { get; set; }
        public string CompanyName { get; set; }
        public string ShipperName { get; set; }
    }
}