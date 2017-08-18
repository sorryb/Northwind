using System.Linq;
using System.Web.Mvc;

namespace NorthwindWeb.Controllers
{
    public class ProductsController : Controller
    {
        private Models.NorthwindModel db = new Models.NorthwindModel();
        public ActionResult Products(string category)
        {
            var products=db.Products as IQueryable;
            switch(category)
            {
                case "Classic":
                products = db.Products.Select(x => x.CategoryID == 1);
                    break;
                case "Smartphone":
                products = db.Products.Select(x => x.CategoryID == 2);
                    break;
                case "Accesorii":
                products = db.Products.Select(x => x.CategoryID == 3);
                    break;
                case "Gadgeturi":
                products = db.Products.Select(x => x.CategoryID == 4);
                    break;
                case "eBookReaders":
                products = db.Products.Select(x => x.CategoryID == 5);
                    break;

            }
            return View(products);
        }
    }
}
