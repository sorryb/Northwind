using System.Collections.Generic;

namespace NorthwindWeb.Core.ViewModels.Dashboard
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
        public List<LocateSearch> MatchesFoundPaged { get; set; }
    }
}