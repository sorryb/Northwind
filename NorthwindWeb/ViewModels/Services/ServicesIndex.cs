using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NorthwindWeb.ViewModels
{
    /// <summary>
    /// The data model sent by the ServicesController to Home
    /// </summary>
    public class ServicesIndex
    {

        /// <summary>
        /// The names of the four first products. 
        /// </summary>
        public IQueryable<string> TopFourName {get; set;}

        /// <summary>
        /// Display the first four products with their details.
        /// </summary>
        public IEnumerable<ProductServices> TopFourProducts { get; set; }

        /// <summary>
        /// Display the last three products with their details.
        /// </summary>
        public IEnumerable<ProductServices> LastThreeProducts { get; set; }

    }
}