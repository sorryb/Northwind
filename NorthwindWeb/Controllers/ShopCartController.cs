using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;
using NorthwindWeb.Models.ServerClientCommunication;

namespace NorthwindWeb.Controllers
{
    public class ShopCartController : Controller, NorthwindWeb.Models.Interfaces.IJsonTableFillServerSide
    {
        NorthwindModel db = new NorthwindModel();

        /// <summary>
        /// See the curent shop list
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string json)
        {

            return View();
        }

        public JsonResult JsonTableFill(int draw, int start, int length)
        {
            string json = Request.QueryString["json"] ?? "";

            const int TOTAL_ROWS = 999;

            string search = "";
            try
            {
                search = Request.QueryString["search[value]"] ?? "";
            }
            catch (NullReferenceException) { }


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
            var list = db.Products.Where(p => (p.ProductName.Contains(search)));

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
                        list = list.OrderBy(x => x.UnitPrice);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.UnitPrice);
                    }
                    break;
                case 2: // and so on
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.UnitsInStock);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.UnitsInStock);
                    }
                    break;
                case 3:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.UnitsOnOrder);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.UnitsOnOrder);
                    }
                    break;
                case 4:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.ReorderLevel);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.ReorderLevel);
                    }
                    break;
                case 5:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.Discontinued);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.Discontinued);
                    }
                    break;

            }

            //objet that whill be sent to client
            JsonDataTableObject dataTableData = new JsonDataTableObject()
            {
                draw = draw,
                recordsTotal = db.Products.Count(),
                data = list.Skip(start).Take(length).Select(x => new
                {
                    ID = x.ProductID,
                    ProductName = x.ProductName,
                    Price = x.UnitPrice,
                    InStock = x.UnitsInStock,
                    OnOrders = x.UnitsOnOrder,
                    ReorderLevel = x.ReorderLevel,
                    Discontinued = x.Discontinued
                }),
                recordsFiltered = list.Count(), //need to be below data(ref recordsFiltered)
            };
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
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