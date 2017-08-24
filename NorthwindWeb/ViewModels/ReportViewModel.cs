using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels
{
    public class ReportViewModel
    {
        public ReportViewModel(string link,string filename)
        {
            this.Link = link;
            this.Filename = filename;
        }
        public string Link { get; set; }
        public string Filename { get; set; } 

    }
}