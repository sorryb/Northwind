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
        public ReportViewModel(string link, string filename)
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

    /// <summary>
    /// Holds username and password for the report server.
    /// </summary>
    public class ReportServerViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// The address of the report server.
        /// </summary>
        public string ServerAddress { get; set; }
        /// <summary>
        /// The directory that holds the reports.
        /// </summary>
        public string ReportsDirectory { get; set; }


    }
}