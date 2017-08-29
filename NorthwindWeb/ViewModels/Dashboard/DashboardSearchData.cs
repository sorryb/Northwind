using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace NorthwindWeb.ViewModels.Dashboard
{
    public class DashboardSearchData
    {
        public int MatchesCount { get; set; }
        public IEnumerable<LocateSearch> MatchesFound { get; set; }
        public IPagedList MatchesFoundPaged { get; set; }
    }
}