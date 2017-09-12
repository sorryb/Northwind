using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;
using NorthwindWeb.Models.ServerClientCommunication;
using Newtonsoft.Json.Linq;
using NorthwindWeb.Models.ShopCart;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace NorthwindWeb.Controllers
{
    public class ShopCartController : Controller, NorthwindWeb.Models.Interfaces.IJsonTableFillServerSide
    {
        NorthwindModel db = new NorthwindModel();

        /// <summary>
        /// See the curent shop list
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Inserts or updates a product in the ShopCart table.
        /// </summary>
        /// <param name="id">The id of the product</param>
        /// <param name="quantity">The quantity of the product</param>
        [Authorize]
        public void Create(int id, int quantity)
        {
            if (!(db.ShopCart.Where(x => x.ProductID == id).Any()))
            {
                ShopCarts cart = new ShopCarts() { UserName = User.Identity.Name, ProductID = id, Quantity = quantity };
                db.ShopCart.Add(cart);
                db.SaveChanges();
            }
            else
            {
                Update(id, quantity);
            }
        }

        private void Update(int id, int quantity)
        {
            var cart = db.ShopCart.Where(x => x.ProductID == id).FirstOrDefault();
            cart.Quantity = quantity;
            db.SaveChanges();
        }










        /// <summary>
        /// Import data from local storage when the user will log on
        /// </summary>
        /// <param name="json">data will be parsed as a json string</param>
        /// <returns>"{}" on success, "Error" on fail</returns>
        public string ImportFromLocal(string json = "")
        {
            var shopCartProducts = JsonConvert.DeserializeObject<List<ProductShopCart>>(json).AsQueryable();

            if (User.Identity.IsAuthenticated && shopCartProducts.Count() != 0)
            {
                try
                {
                    foreach (var shopCartProduct in shopCartProducts)
                    {
                        if (db.ShopCart.Any(x => x.UserName == User.Identity.Name && x.ProductID == shopCartProduct.ID))
                        {
                            db.ShopCart.Where(x => x.UserName == User.Identity.Name && x.ProductID == shopCartProduct.ID).First().Quantity = db.ShopCart.Where(x => x.UserName == User.Identity.Name && x.ProductID == shopCartProduct.ID).First().Quantity + 1;
                            //First() da exceptie daca nu gaseste nimic, FirstOrDefault() da null
                        }
                        else
                        {
                            db.ShopCart.Add(new ShopCarts() { ProductID = shopCartProduct.ID, Quantity = shopCartProduct.Quantity, UserName = User.Identity.Name });
                        }
                    }
                    db.SaveChanges();
                }
                catch
                {
                    return "Error";
                }
                return "{}"; //for ajax this mean success
            }
            return "Error";
        }




        public string Delete(int? id)
        {
            //todo trebuie sa mai lucrez aici
            if (id != null && User.Identity.IsAuthenticated)
            {
                db.ShopCart.Remove(db.ShopCart.Where(x => x.UserName == User.Identity.Name && x.ProductID == id).First());
                db.SaveChanges();
                //La fel
                return "Succes";
            }
            else
            {
                return "Error";
            }
        }



        public JsonResult JsonTableFill(int draw, int start, int length)
        {
            string json = Request.QueryString["json"] ?? "";
            


            const int TOTAL_ROWS = 999;

            

            //init list of products in shopcart
            IQueryable<ProductShopCartDetailed> list;
            if (User.Identity.IsAuthenticated)
            {
                list = from s in db.ShopCart
                       join p in db.Products on s.ProductID equals p.ProductID
                       join c in db.Categories on p.CategoryID equals c.CategoryID
                       select new ProductShopCartDetailed
                       {
                           Category = c.CategoryName,
                           ID = s.ProductID,
                           ProductName = p.ProductName,
                           Quantity = s.Quantity,
                           UnitPrice = p.UnitPrice ?? 999999
                       };
            }
            else
            {
                if (json != "")
                    list = JsonConvert.DeserializeObject<List<ProductShopCartDetailed>>(json).AsQueryable();
                else
                    list = new List<ProductShopCartDetailed>().AsQueryable();
            }


            int sortColumn = -1;
            string sortDirection = "asc";
            if (length == -1)
            {
                length = TOTAL_ROWS;
            }

            // note: we only sort one column at a time
            try
            {
                if (Request.QueryString["order[0][column]"] != null)
                {
                    sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
                }
            }
            catch (NullReferenceException) { }

            try
            {
                if (Request.QueryString["order[0][dir]"] != null)
                {
                    sortDirection = Request.QueryString["order[0][dir]"];
                }
            }
            catch (NullReferenceException) { }

            //list of product that contain "search"

            //order list
            switch (sortColumn)
            {
                case -1: //sort by first column
                    goto FirstColumn;
                case 0: //first column
                    FirstColumn:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.ProductName);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.ProductName);
                    }
                    break;
                case 1: //second column
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.Quantity);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.Quantity);
                    }
                    break;
                case 2: // and so on
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.UnitPrice);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.UnitPrice);
                    }
                    break;
                case 3:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => (x.UnitPrice * x.Quantity));
                    }
                    else
                    {
                        list = list.OrderByDescending(x => (x.UnitPrice * x.Quantity));
                    }
                    break;
            }



            //object that whill be sent to client
            JsonDataTableObject dataTableData = new JsonDataTableObject()
            {
                draw = draw,
                recordsTotal = db.Products.Count(),
                data = list.Skip(start).Take(length).Select(p => new
                {
                    Category = p.Category,
                    ID = p.ID,
                    ProductName = p.ProductName,
                    Quantity = p.Quantity,
                    UnitPrice = (int)p.UnitPrice,
                }).AsQueryable(),
                recordsFiltered = list.Count(), //need to be below data(ref recordsFiltered)
            };
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public string GetCartCount()
        {
            if (User.Identity.IsAuthenticated)
                return db.ShopCart.Where(x => x.UserName == User.Identity.Name).Count().ToString();
            return "{}";
        }


        [Authorize]
        public ActionResult ConfirmCommand()
        {
            var shopCart = db.ShopCart;
            string userName = User.Identity.GetUserName();
            Orders order = new Orders();
            order.OrderDate = DateTime.Now;
            foreach(var product in shopCart)
            {
                short quantity = 255;
                if (product.UserName == userName)
                {
                    if ((product.Quantity>=1)&& (product.Quantity <= 255))
                    {
                        quantity = (short)product.Quantity;
                    }
                    var productdetails = db.Order_Details.Where(x => x.ProductID == product.ProductID).Select(x => new { UnitPrice = x.UnitPrice, Discount = x.Discount }).FirstOrDefault();
                    order.Order_Details.Add(new Order_Details { ProductID = product.ProductID, Quantity = quantity,UnitPrice=productdetails.UnitPrice,Discount=productdetails.Discount });
                    db.ShopCart.Remove(product);
                }
            }
            int employeerId = db.Employees.Where(e => e.FirstName + e.LastName == userName).Select(e => e.EmployeeID).FirstOrDefault();
            if (employeerId == 0) employeerId = 1;
            order.EmployeeID = employeerId;
            db.Orders.Add(order);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}