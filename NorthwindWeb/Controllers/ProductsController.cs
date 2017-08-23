using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Products controller. From table Products.
    /// </summary>
    public class ProductsController : Controller
    {
        private Models.NorthwindModel db = new Models.NorthwindModel();

        /// <summary>
        /// Returns a paged list filtered by category and by name
        /// </summary>
        /// <param name="category">The category of the product, if it's not a valid value returns all products</param>
        /// <param name="search">Filters by products' name</param>
        /// <param name="page">Required for paged list to work</param>
        /// 
        /// <returns>PagedList</returns>
        public ActionResult Products(string category, string search = "", int? page = 1)
        {
            var products = db.Products as IQueryable<ViewModels.ViewProductCategoryS>;
            ViewBag.title = "Produse";
            ViewBag.search = search;
            int categID = 0;

            //test categories of products.
            switch (category)
            {
                case "Classic":
                    ViewBag.title = "Classic";
                    categID = 1;
                    break;
                case "Smartphone":
                    ViewBag.title = "Smartphone";
                    categID = 2;
                    break;
                case "Accesorii":
                    ViewBag.title = "Accesories";
                    categID = 3;
                    break;
                case "Gadgeturi":
                    ViewBag.title = "Gadgets";
                    categID = 4;
                    break;
                case "eBookReaders":
                    ViewBag.title = "eBookReaders";
                    categID = 5;
                    break;
            }

            //Gets all products filtered by category and by name from the database.
            products = from prod in db.Products
                       join cat in db.Categories on prod.CategoryID equals cat.CategoryID
                       join supp in db.Suppliers on prod.SupplierID equals supp.SupplierID
                       where categID>0 ? prod.CategoryID.Value == categID : true && prod.ProductName.Contains(search)
                       orderby prod.ProductName ascending
                       select new ViewModels.ViewProductCategoryS
                       {
                           CategoryName = cat.CategoryName,
                           ProductName = prod.ProductName,
                           ProductID = prod.ProductID.ToString(),
                           ProductPrice = prod.UnitPrice ?? 0,
                           OnOrder = prod.UnitsOnOrder.ToString(),
                           Stock = prod.UnitsInStock.ToString(),
                           SuppliersName = supp.CompanyName
                       };

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }
    }
}
