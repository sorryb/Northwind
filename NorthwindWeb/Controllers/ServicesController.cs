using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;
using NorthwindWeb.ViewModels;
using PagedList;

namespace NorthwindWeb.Controllers
{
    public class ServicesController : Controller
    {
        private Models.NorthwindModel db = new Models.NorthwindModel();
        public ActionResult Index()
        {
            var viewModel = new ServicesIndex();
            viewModel.top4nume = (from p in db.Products
                                  orderby p.ProductID
                                  select p.ProductName).Take(4);

            var produse = (from p in db.Products
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

            List<Produse> list = new List<Produse>();

            foreach (var item in produse)
            {
                Produse x = new Produse();

                x.ProductName = item.ProductName;
                x.CategoryName = item.CategoryName;
                x.CompanyName = item.CompanyName;
                x.QuantityPerUnit = item.QuantityPerUnit;
                x.UnitPrice = item.UnitPrice;
                x.UnitsInStock = item.UnitsInStock;
                x.UnitsOnOrder = item.UnitsOnOrder;

                list.Add(x);

            }
            viewModel.top4produse = list;



            var produse2 = (from p in db.Products
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

            List<Produse> list2 = new List<Produse>();

            foreach (var item2 in produse2)
            {
                Produse y = new Produse();

                y.ProductName = item2.ProductName;
                y.CategoryName = item2.CategoryName;
                y.CompanyName = item2.CompanyName;
                y.QuantityPerUnit = item2.QuantityPerUnit;
                y.UnitPrice = item2.UnitPrice;
                y.UnitsInStock = item2.UnitsInStock;
                y.UnitsOnOrder = item2.UnitsOnOrder;

                list2.Add(y);

            }
            viewModel.ultimele3 = list2;




            //                select top 4 p.ProductName,s.CompanyName,c.CategoryName
            //from Products p
            //inner join Categories c on p.CategoryID = c.CategoryID
            //inner join Suppliers s on p.SupplierID = s.SupplierID

            //var x = from p in db.Products
            //        orderby p.ProductID
            //        select p.ProductName;
            //x=x.Take(4);


            return View(viewModel);
        }
	}
}