using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels
{
    /// <summary>
    ///  Keeps all territories from regions.
    /// </summary>
    public class RegionDetails
    {
        /// <summary>
        /// The Id of territory.
        /// </summary>
        public string TerritoryID { get; set; }

        /// <summary>
        /// The description of territory.
        /// </summary>
        public string TerritoryDescription { get; set; }

        /// <summary>
        /// The region of territory.
        /// </summary>
        public RegionDetails() { }

    }
}