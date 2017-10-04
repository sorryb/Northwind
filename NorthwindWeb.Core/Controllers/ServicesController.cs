using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using NorthwindWeb.Models;
using NorthwindWeb.ViewModels;
using NorthwindWeb.Context;
using Microsoft.AspNetCore.Mvc;
using NorthwindWeb.Core.Context;
using NorthwindWeb.Core.ViewModels;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Services Controller. For table Products
    /// </summary>
    public class ServicesController : Controller
    {
        private NorthwindDatabase db = new NorthwindDatabase(new Microsoft.EntityFrameworkCore.DbContextOptions<NorthwindDatabase>());

        public ServicesController(NorthwindDatabase context)
        {
            db = context;
        }
        /// <summary>
        /// take first 4 products and last 3 products with their details
        /// </summary>
        /// <returns>Products index view</returns>
        public ActionResult Index()
        {
            var viewModel = new ServicesIndex
            {

                //take the names of first 4 products
                TopFourName = (from p in db.Products
                               where (p.CategoryID == 6)
                               orderby p.ProductID
                               select p.ProductName).Take(4)
            };

            //take first 4 products
            var products = (from p in db.Products
                            where (p.CategoryID == 6)
                            join c in db.Categories on p.CategoryID equals c.CategoryID
                            join s in db.Suppliers on p.SupplierID equals s.SupplierID
                            select new
                           {
                               p.ProductName,
                               s.CompanyName,
                               c.CategoryName,
                               p.QuantityPerUnit,
                               p.UnitPrice,
                               p.UnitsInStock,
                               p.UnitsOnOrder,
                           }).Take(4);

            List<ProductServices> listOfProducts = new List<ProductServices>();

            //lopp in first 4 products 
            foreach (var itemInProducts in products)
            {
                ProductServices product = new ProductServices
                {
                    ProductName = itemInProducts.ProductName,
                    CategoryName = itemInProducts.CategoryName,
                    CompanyName = itemInProducts.CompanyName,
                    QuantityPerUnit = itemInProducts.QuantityPerUnit,
                    UnitPrice = itemInProducts.UnitPrice,
                    UnitsInStock = itemInProducts.UnitsInStock,
                    UnitsOnOrder = itemInProducts.UnitsOnOrder
                };

                listOfProducts.Add(product);

            }
            viewModel.TopFourProducts = listOfProducts;


            //take last 3 products
            var productsOrderByDesc = (from p in db.Products
                           where (p.CategoryID == 6)
                           join c in db.Categories on p.CategoryID equals c.CategoryID
                           join s in db.Suppliers on p.SupplierID equals s.SupplierID
                           orderby p.ProductID descending
                           select new
                           {
                               p.ProductName,
                               p.ProductID,
                               s.CompanyName,
                               c.CategoryName,
                               p.QuantityPerUnit,
                               p.UnitPrice,
                               p.UnitsInStock,
                               p.UnitsOnOrder,
                           }).Take(3);

            List<ProductServices> listOfProductsOrderByDesc = new List<ProductServices>();

            //loop in last 3 products
            foreach (var itemInProductsDesc in productsOrderByDesc)
            {
                ProductServices product = new ProductServices
                {
                    ProductName = itemInProductsDesc.ProductName,
                    ProductID = itemInProductsDesc.ProductID,
                    CategoryName = itemInProductsDesc.CategoryName,
                    CompanyName = itemInProductsDesc.CompanyName,
                    QuantityPerUnit = itemInProductsDesc.QuantityPerUnit,
                    UnitPrice = itemInProductsDesc.UnitPrice,
                    UnitsInStock = itemInProductsDesc.UnitsInStock,
                    UnitsOnOrder = itemInProductsDesc.UnitsOnOrder
                };

                listOfProductsOrderByDesc.Add(product);

            }
            viewModel.LastThreeProducts = listOfProductsOrderByDesc;


            return View(viewModel);
        }
	}
}