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
        public decimal dashboardMorrisBarColumA { get; set; }
        public decimal dashboardMorrisBarColumB { get; set; }
        public decimal dashboardMorrisBarColumC { get; set; }
        public DashboardMorrisBar() {}
    }
}