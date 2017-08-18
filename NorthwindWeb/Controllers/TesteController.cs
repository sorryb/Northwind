using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthwindWeb.Controllers
{
    public class TesteController : Controller
    {
        private Models.NorthwindModel db = new Models.NorthwindModel();
        // GET: Teste
        public string Index()
        {
            string test = "";
            IQueryable asd = db.Products.Select(x => x.ProductName);
                foreach(var item in asd)
            {
                test = test + item;

            }
            return test;
        }
    }
}