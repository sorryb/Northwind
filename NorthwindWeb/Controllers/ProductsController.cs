using System.Collections;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using NorthwindWeb.Models;

namespace NorthwindWeb.Controllers
{
    public class ProductsController : Controller
    {
        private Models.NorthwindModel db = new Models.NorthwindModel();
        public ActionResult Products(string category, int? page = 1)
        {
            var products = db.Products as IQueryable<Products>;
            switch (category)
            {
                case "Classic":
                    ViewBag.title = "Classic";
                    products = db.Products.Where(x => x.CategoryID == 1);
                    break;
                case "Smartphone":
                    ViewBag.title = "Smartphone";
                    products = db.Products.Where(x => x.CategoryID == 2);
                    break;
                case "Accesorii":
                    ViewBag.title = "Accesorii";
                    products = db.Products.Where(x => x.CategoryID == 3);
                    break;
                case "Gadgeturi":
                    ViewBag.title = "Gadgeturi";
                    products = db.Products.Where(x => x.CategoryID == 4);
                    break;
                case "eBookReaders":
                    ViewBag.title = "eBookReaders";
                    products = db.Products.Where(x => x.CategoryID == 5);
                    break;
            }
            products = products.OrderBy(x => x.ProductName);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Search(string search, int? page = 1)
        {
            var products = db.Products as IQueryable<Products>;
            
            ViewBag.title = "Result for: " + search;
            ViewBag.search = search;
            ViewBag.page = page;
            products = db.Products.Where(x => x.ProductName.Contains(search));
            
            products = products.OrderBy(x => x.ProductName);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }
        public void AddToCart()
        {

        }
    }
    
}
