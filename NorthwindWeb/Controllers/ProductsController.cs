using System.Linq;
using System.Web.Mvc;
using PagedList;
using NorthwindWeb.Models.ExceptionHandler;

using System.Web.Script.Serialization;
using System.Collections.Generic;

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
            ViewBag.title = ViewBag.category= "Produse";
            ViewBag.search = search;
            int categID = 0;

            int count = 0;
            foreach (var a in db.Categories)
            {
                count++;
                if(category == a.CategoryName)
                {
                    ViewBag.title = ViewBag.category = a.CategoryName;
                    categID = count;
                }
            }

            //Gets all products filtered by category and by name from the database.
            products = from prod in db.Products
                       join cat in db.Categories on prod.CategoryID equals cat.CategoryID
                       join supp in db.Suppliers on prod.SupplierID equals supp.SupplierID
                       where (categID>0 ? prod.CategoryID == categID : true) && prod.ProductName.Contains(search)
                       orderby prod.ProductName ascending
                       select new ViewModels.ViewProductCategoryS
                       {
                           CategoryName = cat.CategoryName,
                           ProductName = prod.ProductName,
                           ProductID = prod.ProductID.ToString(),
                           ProductPrice = prod.UnitPrice ?? 0,
                           OnOrder = prod.UnitsOnOrder.ToString(),
                           Stock = prod.UnitsInStock.ToString(),
                           SuppliersName = supp.CompanyName,
                           Discontinued = prod.Discontinued
                       };
            products = products.OrderBy(x => x.Discontinued).ThenBy(y => y.ProductName);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }
    }
}
