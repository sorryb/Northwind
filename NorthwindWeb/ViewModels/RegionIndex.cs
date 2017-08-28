using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorthwindWeb.Models;

namespace NorthwindWeb.ViewModels
{
    public class RegionIndex
    {
        public Region region { get; set; }
        public IEnumerable<RegionDetails> details { get; set; }
    }
}