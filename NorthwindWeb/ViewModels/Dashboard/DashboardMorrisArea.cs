using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;

namespace NorthwindWeb.ViewModels.Dashboard
{    /// <summary>
     /// The data model for the MorrisArea graph
     /// </summary>
     public class DashboardMorrisArea
    {
        /// <summary>
        /// year on which sales are calculated
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// Sales per Year
        /// </summary>
        public decimal Sales { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public DashboardMorrisArea() {}
    }
}