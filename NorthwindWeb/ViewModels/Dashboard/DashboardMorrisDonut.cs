using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels.Dashboard
{/// <summary>
///  The data model for the MorrisDonut graph
/// </summary>
    public class DashboardMorrisDonut
    {
        public string label { get; set; }
        public decimal value { get; set; }
        public DashboardMorrisDonut() { }
    }
}