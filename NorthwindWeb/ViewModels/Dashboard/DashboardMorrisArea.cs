using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;

namespace NorthwindWeb.ViewModels.Dashboard
{/// <summary>
 /// The data model for the MorrisArea graph
 /// </summary>

    public class DashboardMorrisArea
    {
       
        public string Year { get; set; }
        public decimal Sales { get; set; }
        public DashboardMorrisArea() {}
    }
}