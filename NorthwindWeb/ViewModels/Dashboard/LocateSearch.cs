using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels.Dashboard
{/// <summary>
/// Data model returned by dashboard search
/// </summary>
    public class LocateSearch
    {
        public int Position { get; set; }
        public string ID { get; set; }
        public string WhereFound { get; set; }
        public string Controller { get; set; }
        public LocateSearch() { }
    }
}