using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using NorthwindWeb.Models.ServerClientCommunication;
using NorthwindWeb.Models.ExceptionHandler;
using NorthwindWeb.Context;
using Microsoft.AspNetCore.Authorization;
using NorthwindWeb.Core.Context;
using Microsoft.AspNetCore.Mvc;
using NorthwindWeb.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace NorthwindWeb.Controllers
{
    /// <summary>
    /// Class that contains all the neccessary functions to perform CRUD operations on the database employees table.
    /// </summary>
    //todo decomenteaza [Authorize()]
    //[Authorize(Roles = "Admins, Managers")]
    public class EmployeesController : Controller
    {
        private readonly NorthwindDatabase db;
        //todo
        //private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(EmployeesController));

        public EmployeesController(NorthwindDatabase context)
        {
            db = context;
        }

        /// <summary>
        /// Displays a page with all the employees in the database.
        /// </summary>
        /// <returns>Employees index view</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays a page showing all the information about one employee.
        /// </summary>
        /// <param name="id">The id of the employee whose information to show</param>
        /// <returns>Employees details view</returns>
        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(400);
            }
            Employees employees = await db.Employees.FindAsync(id);
            if (employees == null)
            {
                return StatusCode(404);
            }
            return View(employees);
        }


        /// <summary>
        /// Returns the view containing the form neccesary for creating a new employee.
        /// </summary>
        /// <returns>Create view.</returns>
        public ActionResult Create()
        {
            ViewBag.ReportsTo = new SelectList(db.Employees, "EmployeeID", "LastName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Inserts an employee into the database table. If it fails, goes back to the form.
        /// </summary>
        /// <param name="employees">The employee entity to be inserted</param>
        /// <returns>If successful returns employees index view, else goes back to form.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("EmployeeID,LastName,FirstName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,City,Region,PostalCode,Country,HomePhone,Extension,Notes,ReportsTo")] Employees employees)
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
        /// <summary>
        /// Returns the view containing the form necessary for editing an existing employee.
        /// </summary>
        /// <param name="id">The id of the employee that is going to be edited</param>
        /// <returns>Employees edit view</returns>
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return StatusCode(400);
            }
            Employees employees = await db.Employees.FindAsync(id);
            if (employees == null)
            {
                return StatusCode(404);
            }
            ViewBag.ReportsTo = new SelectList(db.Employees, "EmployeeID", "LastName", employees.ReportsTo);
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Updates the database 
        /// changing the fields of
        /// the employee whose id is equal to the id of the provided employees parameter
        /// to those of the parameter.
        /// </summary>
        /// <param name="employees">The changed employee.</param>
        /// <returns>Employees index view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("EmployeeID,LastName,FirstName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,City,Region,PostalCode,Country,HomePhone,Extension,Notes,ReportsTo")] Employees employees)
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


        /// <summary>
        /// Displays a confirmation page for the following delete.
        /// </summary>
        /// <param name="id">The employee that is going to be deleted.</param>
        /// <returns>Delete view</returns>
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return StatusCode(400);
            }

            Employees employees = await db.Employees.FindAsync(id);

            if (employees == null)
            {
                return StatusCode(404);
            }
            return View(employees);
        }


        /// <summary>
        /// Deletes an employee from the database. The employee must not have orders or subordinates
        /// </summary>
        /// <param name="id">The id of the employee that is going to be deleted</param>
        /// <returns>Employees index view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            Employees employees = await db.Employees.FindAsync(id);
            try
            {
                if (employees.Orders.Any())
                    throw new DeleteException("Angajatul nu poate fi sters pentru ca detine comenzi");
                if (employees.Employees1.Any())
                    throw new DeleteException("Angajatul nu poate fi sters pentru ca are angajati in subordine");
                db.Employees.Remove(employees);

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (DeleteException e)
            {
                //todo
                //logger.Error(e.ToString());
                throw e;
            }
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

        /// <summary>
        /// Function used to control the dashboard datatables from the server
        /// </summary>
        /// <param name="draw"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns>A JSON filtered employees list.</returns>
        // GET: Employees by Json
        public JsonResult JsonTableFill(int draw, int start, int length)
        {
            const int TOTAL_ROWS = 999;


            string search = Request.Query["search[value]"];
            int sortColumn = -1;
            string sortDirection = "asc";
            if (length == -1)
            {
                length = TOTAL_ROWS;
            }

            // note: we only sort one column at a time
            if (!String.IsNullOrEmpty(Request.Query["order[0][column]"]))
            {
                sortColumn = int.Parse(Request.Query["order[0][column]"]);
            }
            if (!String.IsNullOrEmpty(Request.Query["order[0][dir]"]))
            {
                sortDirection = Request.Query["order[0][dir]"];
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
                case 3: // and so on
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.City);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.City);
                    }
                    break;
                case 2:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.Title);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.Title);
                    }
                    break;
                case 5:
                    if (sortDirection == "asc")
                    {
                        list = list.OrderBy(x => x.HomePhone);
                    }
                    else
                    {
                        list = list.OrderByDescending(x => x.HomePhone);
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
            }

            //object that whill be sent to client
            JsonDataTable dataTableData = new JsonDataTable()
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

            return Json(dataTableData);
        }
    }
}