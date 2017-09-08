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
        /// If you see this please delete it (public string CreateJsonTableObject())
        /// </summary>
        /// <returns></returns>
        public string CreateJsonTableObject()
        {
            System.IO.File.WriteAllText("d:\\Table\\products.json", new JavaScriptSerializer().Serialize(db.Products.Select(x => new {
                ProductName = x.ProductName,
                SupplierID = x.SupplierID,
                CategoryID = x.CategoryID,
                QuantityPerUnit = x.QuantityPerUnit,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                UnitsOnOrder = x.UnitsOnOrder,
                ReorderLevel = x.ReorderLevel,
                Discontinued = x.Discontinued
            })));
            System.IO.File.WriteAllText("d:\\Table\\categoies.json", new JavaScriptSerializer().Serialize(db.Categories.Select(x => new {
                CategoryName = x.CategoryName,
                Description = x.Description,
                Products = x.Products
            })));
            System.IO.File.WriteAllText("d:\\Table\\products.json", new JavaScriptSerializer().Serialize(db.Customers.Select(x => new {
                CompanyName = x.CompanyName,
                ContactName = x.ContactName,
                ContactTitle = x.ContactTitle,
                Address = x.Address,
                City = x.City,
                Region = x.Region,
                PostalCode = x.PostalCode,
                Country = x.Country,
                Phone = x.Phone,
                Fax = x.Fax
            })));
            System.IO.File.WriteAllText("d:\\Table\\products.json", new JavaScriptSerializer().Serialize(db.Employees.Select(x => new {

            })));
            System.IO.File.WriteAllText("d:\\Table\\products.json", new JavaScriptSerializer().Serialize(db.Products.Select(x => new {

            })));
            System.IO.File.WriteAllText("d:\\Table\\products.json", new JavaScriptSerializer().Serialize(db.Products.Select(x => new {

            })));
            System.IO.File.WriteAllText("d:\\Table\\products.json", new JavaScriptSerializer().Serialize(db.Products.Select(x => new {

            })));
            System.IO.File.WriteAllText("d:\\Table\\products.json", new JavaScriptSerializer().Serialize(db.Products.Select(x => new {

            })));
            System.IO.File.WriteAllText("d:\\Table\\products.json", new JavaScriptSerializer().Serialize(db.Products.Select(x => new {

            })));

            return "Succes!";
        }



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
                           SuppliersName = supp.CompanyName
                       };

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }
    }
}
