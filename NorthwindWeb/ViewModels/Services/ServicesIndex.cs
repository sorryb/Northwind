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

        public IQueryable<string> top4name {get; set;}
        public IEnumerable<ProductServices> top4products { get; set; }
        public IEnumerable<ProductServices> last3 { get; set; }

    }
}