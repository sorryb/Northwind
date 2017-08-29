using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels.Dashboard
{/// <summary>
/// The data model for the MorrisBar graph
/// </summary>
    public class DashboardMorrisBar
    {
        public string Year { get; set; }
        public decimal a { get; set; }
        public decimal b { get; set; }
        public decimal c { get; set; }
        public DashboardMorrisBar() {}
    }
}