using System.Collections.Generic;
using NorthwindWeb.Core.Models;

namespace NorthwindWeb.ViewModels
{
    /// <summary>
    /// The data model sent by the RegionController to Home
    /// </summary>
    public class RegionIndex
    {
        /// <summary>
        /// The name of region.
        /// </summary>
        public Region region { get; set; }

        /// <summary>
        /// The details of region.
        /// </summary>
        public IEnumerable<RegionDetails> details { get; set; }
    }
}