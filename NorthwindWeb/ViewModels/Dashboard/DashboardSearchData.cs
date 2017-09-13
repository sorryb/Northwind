using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace NorthwindWeb.ViewModels.Dashboard
{   /// <summary>
    ///  The data model sent by the DashboardController to Search
    /// </summary>
    public class DashboardSearchData
    {   /// <summary>
        /// number of matches
        /// </summary>
        public int MatchesCount { get; set; }
        /// <summary>
        /// Location of matches
        /// </summary>
        public IEnumerable<LocateSearch> MatchesFound { get; set; }
        /// <summary>
        /// page locations of matches
        /// </summary>
        public IPagedList MatchesFoundPaged { get; set; }
    }
}