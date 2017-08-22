using System.Collections;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using NorthwindWeb.Models;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Products controller. From table Products.
    /// </summary>
    public class ProductsController : Controller
    {
        private Models.NorthwindModel db = new Models.NorthwindModel();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Products(string category, int? page = 1)
        {
            var products = db.Products as IQueryable<Products>;

            //test categories of products.
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
                    ViewBag.title = "Accesories";
                    products = db.Products.Where(x => x.CategoryID == 3);
                    break;
                case "Gadgeturi":
                    ViewBag.title = "Gadgets";
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Search(string search, int? page = 1)
        {
                    
            ViewBag.title = "Rezultate pentru: " + search;
            ViewBag.search = search;
            ViewBag.page = page;
     


            var products = from prod in db.Products
                            join cat in db.Categories on prod.CategoryID equals cat.CategoryID
                           where prod.ProductName.Contains(search)
                           select new ViewModels.ViewProductCategoryS
                           {
                               CategoryName = cat.CategoryName,
                               ProductName = prod.ProductName,
                               ProductID = prod.ProductID.ToString(),
                               ProductPrice = decimal.Round(prod.UnitPrice ?? 0, 2).ToString()
                           };

            products = products.OrderBy(x => x.ProductName);


            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }
    }
    
}
