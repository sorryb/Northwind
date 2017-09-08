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
using NorthwindWeb.Models.Interfaces;
using NorthwindWeb.Models.ServerClientCommunication;
using NorthwindWeb.Models.ExceptionHandler;

namespace NorthwindWeb.Controllers
{
    [Authorize(Roles = "Admins, Employees")]
    public class SuppliersController : Controller, IJsonTableFillServerSide
    {
        private NorthwindModel db = new NorthwindModel();

        // GET: Suppliers
        public ActionResult Index()
        {
            return View();
        }

        // GET: Suppliers/Details/5
        public async Task<ActionResult> Details(int? id)
        {//if a vendor has been selected
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suppliers suppliers = await db.Suppliers.FindAsync(id);
            //if a vendor has been found
            if (suppliers == null)
            {
                return HttpNotFound();
            }
            return View(suppliers);
        }

        // GET: Suppliers/Create
        [Authorize(Roles = "Employees, Admins")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Create([Bind(Include = "SupplierID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage")] Suppliers suppliers)
        {//if inputs data correspond to the model
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(suppliers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(suppliers);
        }

        // GET: Suppliers/Edit/5
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit(int? id)
        {//if a vendor has been selected
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suppliers suppliers = await db.Suppliers.FindAsync(id);
            //if a vendor has been found
            if (suppliers == null)
            {
                return HttpNotFound();
            }
            return View(suppliers);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit([Bind(Include = "SupplierID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage")] Suppliers suppliers)
        {//if inputs data correspond to the model
            if (ModelState.IsValid)
            {
                db.Entry(suppliers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(suppliers);
        }

        // GET: Suppliers/Delete/5
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Delete(int? id)
        {//if a vendor has been selected
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suppliers suppliers = await db.Suppliers.FindAsync(id);
            //if a vendor has been found
            if (suppliers == null)
            {
                return HttpNotFound();
            }
            return View(suppliers);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {//if the supplier exists and can be deleted
            try
            {
                Suppliers suppliers = await db.Suppliers.FindAsync(id);
                db.Suppliers.Remove(suppliers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw new DeleteException("Nu puteti sterge un furnizor cu comenzi alocate");
            }
        }

        //return a list a suppliers to complete table
        public JsonResult JsonTableFill(int draw, int start, int length)
        {
            const int TOTAL_ROWS = 999;

            string search = "";
            try
            {
                search = Request.QueryString["search[value]"] ?? "";
            }
            catch (NullReferenceException e) { }

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
            catch (NullReferenceException e) { }

            try
            {
                if (Request.QueryString["order[0][dir]"] != null)
                {
                    sortDirection = Request.QueryString["order[0][dir]"];
                }
            }
            catch (NullReferenceException e) { }

            //list of product that contain "search"
            var suppliersInfo = db.Suppliers.OrderBy(x => x.SupplierID).Where(s => s.CompanyName.Contains(search) || s.ContactName.Contains(search)
            || s.ContactTitle.Contains(search) || s.Address.Contains(search) || s.City.Contains(search) || s.Country.Contains(search) || s.Phone.Contains(search));


            //order list
            switch (sortColumn)
            {
                case -1: //sort by first column
                    goto FirstColumn;
                case 0: //first column
                    FirstColumn:
                    if (sortDirection == "asc")
                    {
                        suppliersInfo = suppliersInfo.OrderBy(x => x.CompanyName);
                    }
                    else
                    {
                        suppliersInfo = suppliersInfo.OrderByDescending(x => x.CompanyName);
                    }
                    break;
                case 1: //second column
                    if (sortDirection == "asc")
                    {
                        suppliersInfo = suppliersInfo.OrderBy(x => x.ContactName);
                    }
                    else
                    {
                        suppliersInfo = suppliersInfo.OrderByDescending(x => x.ContactName);
                    }
                    break;
                case 2: // and so on
                    if (sortDirection == "asc")
                    {
                        suppliersInfo = suppliersInfo.OrderBy(x => x.ContactTitle);
                    }
                    else
                    {
                        suppliersInfo = suppliersInfo.OrderByDescending(x => x.ContactTitle);
                    }
                    break;
                case 3:
                    if (sortDirection == "asc")
                    {
                        suppliersInfo = suppliersInfo.OrderBy(x => x.Address);
                    }
                    else
                    {
                        suppliersInfo = suppliersInfo.OrderByDescending(x => x.Address);
                    }
                    break;
                case 4:
                    if (sortDirection == "asc")
                    {
                        suppliersInfo = suppliersInfo.OrderBy(x => x.City);
                    }
                    else
                    {
                        suppliersInfo = suppliersInfo.OrderByDescending(x => x.City);
                    }
                    break;
                case 5:
                    if (sortDirection == "asc")
                    {
                        suppliersInfo = suppliersInfo.OrderBy(x => x.Country);
                    }
                    else
                    {
                        suppliersInfo = suppliersInfo.OrderByDescending(x => x.Country);
                    }
                    break;
                case 6:
                    if (sortDirection == "asc")
                    {
                        suppliersInfo = suppliersInfo.OrderBy(x => x.Phone);
                    }
                    else
                    {
                        suppliersInfo = suppliersInfo.OrderByDescending(x => x.Phone);
                    }
                    break;
            }

            //objet that whill be sent to client
            JsonDataTableObject dataTableData = new JsonDataTableObject()
            {
                draw = draw,
                recordsTotal = db.Suppliers.Count(),
                data = suppliersInfo.Skip(start).Take(length).Select(x => new
                {
                    SupplierID = x.SupplierID,
                    CompanyName = x.CompanyName,
                    ContactName = x.ContactName,
                    ContactTitle = x.ContactTitle,
                    Address = x.Address,
                    City = x.City,
                    Country = x.Country,
                    Phone = x.Phone
                }),
                recordsFiltered = suppliersInfo.Count(), //need to be below data(ref recordsFiltered)
            };
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
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
