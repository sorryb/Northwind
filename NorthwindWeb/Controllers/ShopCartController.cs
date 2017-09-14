﻿using System;
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
using System.Threading.Tasks;
using System.Data;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Contains all the methods neccessary for interacting with the shopcart.
    /// For example adding/removing products from the shopcart, or creating an order with those products.
    /// </summary>
    public class ShopCartController : Controller, NorthwindWeb.Models.Interfaces.IJsonTableFillServerSide
    {
        NorthwindModel db = new NorthwindModel();
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ShopCartController));

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
        public string Create(int id, int quantity)
        {
            try
            {
                if (!(db.ShopCart.Any(x => x.ProductID == id && x.UserName == User.Identity.Name)))
                {
                    ShopCarts cart = new ShopCarts() { UserName = User.Identity.Name, ProductID = id, Quantity = quantity };
                    db.ShopCart.Add(cart);
                    db.SaveChanges();
                }
                else
                {
                    Update(id, quantity);
                }
                return "{}";
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                return "Error";
            }
        }

        private void Update(int id, int quantity)
        {
            var cart = db.ShopCart.Where(x => x.ProductID == id && x.UserName == User.Identity.Name).FirstOrDefault();
            cart.Quantity = cart.Quantity >= 255 ? 255 : cart.Quantity + quantity;
            db.SaveChanges();
        }



        /// <summary>
        /// Updates the quantity of a product in the database. The product must exist.
        /// </summary>
        /// <param name="id">The id of the product.</param>
        /// <param name="quantity">The new quantity value.</param>
        /// <returns>"{}" on success, "Error" on failure.</returns>
        public string UpdateQuantity(int id, int quantity)
        {
            if (quantity <= 0 || quantity >= 255 || !(db.ShopCart.Any(x => x.ProductID == id && x.UserName == User.Identity.Name)))
            {
                return "Error";
            }

            db.ShopCart.Where(x => x.ProductID == id && x.UserName == User.Identity.Name).First().Quantity = quantity;
            db.SaveChanges();
            return "{}";//for ajax this means success
        }






        /// <summary>
        /// Import data from local storage when the user will log in.
        /// </summary>
        /// <param name="json">data will be parsed as a json string</param>
        /// <returns>"{}" on success, "Error" on fail</returns>
        public string ImportFromLocal(string json = "")
        {
            var shopCartProducts = JsonConvert.DeserializeObject<List<ProductShopCart>>(json).AsQueryable();
            try
            {
                if (User.Identity.IsAuthenticated && shopCartProducts.Count() != 0)
                {
                    foreach (var shopCartProduct in shopCartProducts)
                    {
                        if (db.ShopCart.Any(x => x.UserName == User.Identity.Name && x.ProductID == shopCartProduct.ID))
                        {
                            db.ShopCart.Where(x => x.UserName == User.Identity.Name && x.ProductID == shopCartProduct.ID).First().Quantity = db.ShopCart.Where(x => x.UserName == User.Identity.Name && x.ProductID == shopCartProduct.ID).First().Quantity + shopCartProduct.Quantity;
                        }
                        else
                        {
                            db.ShopCart.Add(new ShopCarts() { ProductID = shopCartProduct.ID, Quantity = shopCartProduct.Quantity, UserName = User.Identity.Name });
                        }
                    }
                    db.SaveChanges();
                    return "{}"; //for ajax this means success
                }
                else throw(new Exception());
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                return "Error";
            }
        }



        /// <summary>
        /// Deletes a product from the shopcart table in the database.
        /// </summary>
        /// <param name="id">The id of the product that is going to be deleted.</param>
        /// <returns>"{}" on success, "Error" on failure.</returns>
        public string Delete(int? id)
        {
            if (id != null && User.Identity.IsAuthenticated)
            {
                db.ShopCart.Remove(db.ShopCart.Where(x => x.UserName == User.Identity.Name && x.ProductID == id).First());
                db.SaveChanges();
                return "{}";
            }
            else
            {
                return "Error";
            }
        }


        /// <summary>
        /// Send back a JsonDataTableObject as json with all the information that we need to populate datatable
        /// </summary>
        /// <param name="draw">Draw order. Client send a draw id in request to keep track of asyncron response</param>
        /// <param name="start">Start from this item</param>
        /// <param name="length">Take a list with "lenght" (if exists) objects inside.</param>
        /// <returns>JsonDataTableObject</returns>
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
                       where s.UserName == User.Identity.Name
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
                if (Request.QueryString["order[0][column]"] != null)
                {
                    sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
                }

                if (Request.QueryString["order[0][dir]"] != null)
                {
                    sortDirection = Request.QueryString["order[0][dir]"];
                }

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
            JsonDataTable dataTableData = new JsonDataTable()
            {
                Draw = draw,
                RecordsTotal = db.Products.Count(),
                Data = list.Skip(start).Take(length).Select(p => new
                {
                    Category = p.Category,
                    ID = p.ID,
                    ProductName = p.ProductName,
                    Quantity = p.Quantity,
                    UnitPrice = (int)p.UnitPrice,
                }).AsQueryable(),
                RecordsFiltered = list.Count(), //need to be below data(ref recordsFiltered)
            };
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        //todo GetCartCount() pe server ajax...
        /// <summary>
        /// Returns how many products a user has in hit shopcart.
        /// </summary>
        /// <returns>Number of products on success, "Error" on failure.</returns>
        public string GetCartCount()
        {
            if (User.Identity.IsAuthenticated)
                return db.ShopCart.Where(x => x.UserName == User.Identity.Name).Count().ToString();
            return "Error";
        }

        /// <summary>
        /// Creates an order with the products in the logged user's shopcart.
        /// </summary>
        /// <returns>Home index view</returns>
        [Authorize]
        public ActionResult ConfirmOrder()
        {
            var shopCart = db.ShopCart;
            string userName = User.Identity.GetUserName();
            string customerId = db.Customers.Where(c => c.ContactName == userName).Select(c => c.CustomerID).FirstOrDefault();
            if (String.IsNullOrEmpty(customerId)) {return RedirectToAction("CreateCustomers", "ShopCart"); }
            Orders order = new Orders()
            {
                OrderID = db.Orders.Count() + 1,
                CustomerID = customerId,
                OrderDate = DateTime.Now,
                ShipVia =1
            };
           
            foreach (var product in shopCart)
            {
                short quantity = 255;
                if (product.UserName == userName)
                {
                    if ((product.Quantity >= 1) && (product.Quantity <= 255))
                    {
                        quantity = (short)product.Quantity;
                    }
                    var productdetails = db.Order_Details.Where(x => x.ProductID == product.ProductID).Select(x => new { UnitPrice = x.UnitPrice, Discount = x.Discount }).FirstOrDefault();

                    Order_Details orderDetail = new Order_Details
                    {
                        ProductID = product.ProductID,
                        Quantity = quantity,
                        UnitPrice = productdetails.UnitPrice,
                        Discount = productdetails.Discount,
                        OrderID = order.OrderID
                    };
                    order.Order_Details.Add(orderDetail);
                    db.Order_Details.Add(orderDetail);



                    db.ShopCart.Remove(product);
                }
            }

            db.Orders.Add(order);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Returns the view containing the form neccesary for creating a new customer.
        /// </summary>
        /// <returns>Shopart createCustomer view.</returns>
        public ActionResult CreateCustomers()
        {
            return View();
        }

        /// <summary>
        /// Inserts an customer into the database table. If it fails, goes back to the form.
        /// </summary>
        /// <param name="customers">The customer entity to be inserted</param>
        /// <returns>If successful returns customers index view, else goes back to form.</returns>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> CreateCustomers([Bind(Include = "CompanyName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customers customers)
        {
            try
            {
                
                if (!String.IsNullOrEmpty(customers.Address))
                {
                    if (!String.IsNullOrEmpty(customers.Phone))
                    {
                        Customers custom = new Customers()
                        {
                            CustomerID = User.Identity.GetUserName().Substring(0, 5),
                            CompanyName = String.IsNullOrEmpty(customers.CompanyName) ? "Persoana fizica" : customers.CompanyName,
                            ContactName = User.Identity.GetUserName(),
                            ContactTitle = customers.ContactTitle,
                            Address = customers.Address,
                            City = customers.City,
                            Region = customers.Region,
                            PostalCode = customers.PostalCode,
                            Country = customers.Country,
                            Phone = customers.Phone,
                            Fax = customers.Fax
                        };
                        db.Customers.Add(custom);
                        await db.SaveChangesAsync();
                        return RedirectToAction("AssignCustomers","Account");
                    }
                    else
                    {
                        ModelState.AddModelError("Phone", "Introduceti numarul de telefon");
                    }
                }
                else
                {
                    ModelState.AddModelError("Address", "Introduceti adresa");
                }

            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

            }
            return View(customers);
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