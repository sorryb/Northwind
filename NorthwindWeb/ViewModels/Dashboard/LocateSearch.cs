using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels.Dashboard
{
    public class LocateSearch
    {
        public string ID { get; set; }
        public string WhereFound { get; set; }
        public string Controller { get; set; }
        public LocateSearch() { }
    }
}