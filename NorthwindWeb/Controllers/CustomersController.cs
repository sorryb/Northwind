using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorthwindWeb.Models;
using PagedList;
using NorthwindWeb.Models.ServerClientCommunication;
using NorthwindWeb.Models.Interfaces;
using NorthwindWeb.Models.ExceptionHandler;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Customers Controller. For table Customers
    /// </summary>
    [Authorize]
    public class CustomersController : Controller, IJsonTableFillServerSide
    {
        private NorthwindModel db = new NorthwindModel();

        /// <summary>
        /// Displays a page with all the customers in the database.
        /// </summary>
        /// <param name="search">The search look to find something asked</param>
        /// <param name="page">Required for paged list to work</param>
        /// <returns>Customers index view</returns>
        public ActionResult Index(string search = "", int page = 1)
        {
            int pageSize = 15;
            int pageNumber = page;
            ViewBag.search = search;
            return View(db.Customers.OrderBy(x => x.CustomerID).Where(x => x.CompanyName.Contains(search)).ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Displays a page showing all the information about one customer.
        /// </summary>
        /// <param name="id">The id of the customer whose information to show</param>
        /// <returns>Customers details view</returns>
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = await db.Customers.FindAsync(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        /// <summary>
        /// Returns the view containing the form neccesary for creating a new customer.
        /// </summary>
        /// <returns>Create view.</returns>
        [Authorize(Roles = "Employees, Admins")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Inserts an customer into the database table. If it fails, goes back to the form.
        /// </summary>
        /// <param name="customers">The customer entity to be inserted</param>
        /// <returns>If successful returns customers index view, else goes back to form.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Create([Bind(Include = "CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customers);
        }

        /// <summary>
        /// Returns the view containing the form necessary for editing an existing customer.
        /// </summary>
        /// <param name="id">The id of the customer that is going to be edited</param>
        /// <returns>Customers edit view</returns>
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = await db.Customers.FindAsync(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        /// <summary>
        /// Updates the database 
        /// changing the fields of the customer whose id is equal to the id of the provided customers parameter
        /// to those of the parameter.
        /// </summary>
        /// <param name="customers">The changed customer.</param>
        /// <returns>Customers index view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit([Bind(Include = "CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customers);
        }

        /// <summary>
        /// Displays a confirmation page for the following delete.
        /// </summary>
        /// <param name="id">The customer that is going to be deleted.</param>
        /// <returns>Delete view</returns>
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = await db.Customers.FindAsync(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        /// <summary>
        /// Deletes an customer from the database. The customer will be deleted only if he does not have orders.
        /// </summary>
        /// <param name="id">The id of the customer that is going to be deleted</param>
        /// <returns>Customers index view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {

            try
            {
                Customers customers = await db.Customers.FindAsync(id);
                db.Customers.Remove(customers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                string listOfOrders = "";
                var orderId = db.Orders.Include(x => x.Customer).Where(x => x.CustomerID == id).Select(x => new { x.OrderID });
                foreach (var itemInOrders in orderId)
                {
                    listOfOrders = listOfOrders + itemInOrders.OrderID.ToString() + "\n ";
                }
                throw new DeleteException("Clientul nu poate fi sters deoarece are comenzile cu id-urile:\n" + listOfOrders + "\nPentru a putea sterge acest client trebuie sterse comenzile si detaliile lor.");
            }
        }



        /// <summary>
        /// Function used to control the dashboard datatables from the server
        /// </summary>
        /// <param name="draw"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns>A JSON filtered customer list.</returns>
        public JsonResult JsonTableFill(int draw, int start, int length)
        {
            const int totalRows = 999;

            string search = "";

            search = Request.QueryString["search[value]"] ?? "";


            int sortColumn = -1;
            string sortDirection = "asc";
            if (length == -1)
            {
                length = totalRows;
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

            //list of customers that contain "search"
            var list = db.Customers.Where(x => x.CompanyName.Contains(search) ||
                                          x.ContactName.Contains(search) ||
                                          x.ContactTitle.Contains(search) ||
                                          x.City.Contains(search) ||
                                          x.Country.Contains(search) ||
                                          x.Phone.Contains(search));

            //order list
            switch (sortColumn)
            {
                case -1: //sort by first column
                    goto FirstColumn;
                case 0: //first column
                    FirstColumn:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.CompanyName);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.CompanyName);
                    }
                    break;

                case 1: //second column
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.ContactName);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.ContactName);
                    }
                    break;
                case 2: // and so on
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.ContactTitle);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.ContactTitle);
                    }
                    break;
                case 3:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.City);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.City);
                    }
                    break;
                case 4:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.Country);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.Country);
                    }
                    break;
                case 5:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.Phone);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.Phone);
                    }
                    break;


            }

            //objet that whill be sent to client
            JsonDataTable dataTableData = new JsonDataTable()
            {
                draw = draw,
                recordsTotal = db.Customers.Count(),
                data = list.Skip(start).Take(length).Select(x => new
                {
                    ID = x.CustomerID,
                    CompanyName = x.CompanyName,
                    ContactName = x.ContactName,
                    ContactTitle = x.ContactTitle,
                    City = x.City,
                    Country = x.Country,
                    Phone = x.Phone

                }),
                recordsFiltered = list.Count(), //need to be below data(ref recordsFiltered)

            };

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
