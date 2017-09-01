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
    [Authorize]
    public class CustomersController : Controller, IJsonTableFillServerSide
    {
        private NorthwindModel db = new NorthwindModel();

        // GET: Customers
        public ActionResult Index(string search = "", int page = 1)
        {
            int pageSize = 15;
            int pageNumber = page;
            ViewBag.search = search;
            return View(db.Customers.OrderBy(x => x.CustomerID).Where(x => x.CompanyName.Contains(search)).ToPagedList(pageNumber, pageSize));
        }

        // GET: Customers/Details/5
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

        // GET: Customers/Create
        [Authorize(Roles = "Employees, Admins")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Customers/Edit/5
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

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Customers/Delete/5
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

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {

            var order = db.Orders.Any(o => o.CustomerID == id);
           
                if (!order)
                {
                    Customers customers = await db.Customers.FindAsync(id);
                    db.Customers.Remove(customers);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            
            else
            {
                throw new DeleteException("Clientul nu poate fi sters deoarece el are una sau mai multe comenzi.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Customers by Json
        public JsonResult JsonTableFill(int draw, int start, int length)
        {
            const int TOTAL_ROWS = 999;

            string search = Request.QueryString["search[value]"] ?? "";
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

            //list of customers that contain "search"
            var list = db.Customers.Where(x => x.CompanyName.Contains(search)||x.ContactName.Contains(search)||x.ContactTitle.Contains(search));

            //order list
            switch (sortColumn)
            {
                case -1: //sort by first column
                    goto FirstColumn;
                case 0: //first column
                    FirstColumn:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.CustomerID);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.CustomerID);
                    }
                    break;
                case 1: //second column
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.CompanyName);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.CompanyName);
                    }
                    break;
                case 2: // and so on
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.ContactName);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.ContactName);
                    }
                    break;
                case 3:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.ContactTitle);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.ContactTitle);
                    }
                    break;
                case 4:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.City);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.City);
                    }
                    break;
                case 5:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.Country);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.Country);
                    }
                    break;
                case 6:
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
            JsonDataTableObject dataTableData = new JsonDataTableObject()
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


    }
}
