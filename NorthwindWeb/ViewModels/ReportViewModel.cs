using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.ViewModels
{
    /// <summary>
    /// Raports View Model
    /// </summary>
    public class ReportViewModel
    {
        /// <summary>
        /// Constructor of ReportViewModel
        /// </summary>
        /// <param name="link">set link</param>
        /// <param name="filename">set filename</param>
        public ReportViewModel(string link,string filename)
        {
            this.Link = link;
            this.Filename = filename;
        }

        /// <summary>
        /// Link of RaportView
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// File name of RaportView
        /// </summary>
        public string Filename { get; set; } 

    }
}