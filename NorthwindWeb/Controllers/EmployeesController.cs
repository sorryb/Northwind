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

namespace NorthwindWeb.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private NorthwindModel db = new NorthwindModel();

        // GET: Employees
        public ActionResult Index()
        {
            return View();
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = await db.Employees.FindAsync(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // GET: Employees/Create
        [Authorize(Roles = "Employees, Admins")]
        public ActionResult Create()
        {
            ViewBag.ReportsTo = new SelectList(db.Employees, "EmployeeID", "LastName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Create([Bind(Include = "EmployeeID,LastName,FirstName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,City,Region,PostalCode,Country,HomePhone,Extension,Notes,ReportsTo")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employees);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ReportsTo = new SelectList(db.Employees, "EmployeeID", "LastName", employees.ReportsTo);
            return View(employees);
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = await db.Employees.FindAsync(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReportsTo = new SelectList(db.Employees, "EmployeeID", "LastName", employees.ReportsTo);
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employees, Admins")]
        public async Task<ActionResult> Edit([Bind(Include = "EmployeeID,LastName,FirstName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,City,Region,PostalCode,Country,HomePhone,Extension,Notes,ReportsTo")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employees).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ReportsTo = new SelectList(db.Employees, "EmployeeID", "LastName", employees.ReportsTo);
            return View(employees);
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = await db.Employees.FindAsync(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }
        //TODO delete from related table
        // POST: Employees/Delete/5
        /// <summary>
        /// Deletes an employee from the database. If the employee has orders or subordinates returns an error page.
        /// </summary>
        /// <param name="id">The id of the employee that is going to be deleted</param>
        /// <returns>Index if successful, otherwise returns an error page explaining why it failed</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //if(db.Orders.Any())
            //db.Database.SqlQuery<string>($@"
            //    delete from EmployeeTerritories 
            //    where EmployeeID in 
            //    (
            //        select EmployeeID from EmployeeTerritories
            //        left join Employees on EmployeeTerritories.EmployeeID=Employees.EmployeeID
            //        where Employees.EmployeeID={id}
            //    )
            //", null)
            Employees employees = await db.Employees.FindAsync(id);
            db.Employees.Remove(employees);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Employees by Json
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

            //list of product that contain "search"
            var list = db.Employees.
                Where
                (p =>
                    p.LastName.Contains(search) ||
                    p.FirstName.Contains(search) ||
                    p.City.Contains(search) ||
                    p.Title.Contains(search) ||
                    p.Country.Contains(search) ||
                    p.HomePhone.Contains(search)
                );

            //order list
            switch (sortColumn)
            {
                case -1: //sort by first column
                    goto FirstColumn;
                case 0: //first column
                    FirstColumn:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.LastName);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.LastName);
                    }
                    break;
                case 1: //second column
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.FirstName);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.FirstName);
                    }
                    break;
                case 2: // and so on
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.City);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.City);
                    }
                    break;
                case 3:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.Title);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.Title);
                    }
                    break;
                case 4:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.HomePhone);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.HomePhone);
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
            }

            //object that whill be sent to client
            JsonDataTableObject dataTableData = new JsonDataTableObject()
            {
                draw = draw,
                recordsTotal = db.Employees.Count(),
                data = list.Skip(start).Take(length).Select(x => new
                {
                    ID = x.EmployeeID,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    Title = x.Title,
                    City = x.City,
                    Country = x.Country,
                    HomePhone = x.HomePhone

                }),
                recordsFiltered = list.Count(), //need to be below data(ref recordsFiltered)

            };

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
    }
}
