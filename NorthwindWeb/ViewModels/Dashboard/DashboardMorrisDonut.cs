using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels.Dashboard
{/// <summary>
///  The data model for the MorrisDonut graph
/// </summary>
    public class DashboardMorrisDonut
    {   /// <summary>
        /// category for which sales are calculated
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// value of sales in this category
        /// </summary>
        public decimal value { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public DashboardMorrisDonut() { }
    }
}