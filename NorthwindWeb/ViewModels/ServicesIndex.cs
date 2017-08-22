using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NorthwindWeb.ViewModels
{
    public class ServicesIndex
    {
        public IQueryable<string> top4name {get; set;}
        public IEnumerable<ProductServices> top4products { get; set; }
        public IEnumerable<ProductServices> last3 { get; set; }

    }
}