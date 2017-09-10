using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NorthwindWeb.Models;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System;

namespace NorthwindWeb.Context
{

    /// <summary>
    /// Initializa with Users and Roles.
    /// </summary>
    public class NorthwindDatabaseInitializer : CreateDatabaseIfNotExists<NorthwindModel>
    {
        /// <summary>
        /// Seed database ; fill tables.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(NorthwindModel context)
        {
            InsertInDatabase(context);

            base.Seed(context);
        }

        private void InsertInDatabase(NorthwindModel context)
        {
            //configuration
            string path = System.Configuration.ConfigurationManager.AppSettings["JsonDataInitializationPath"];
            bool romanianTerritories = System.Configuration.ConfigurationManager.AppSettings["RomanianTerritoriesRegions"].Equals("true");
            bool testDatabaseValues = System.Configuration.ConfigurationManager.AppSettings["TestDatabaseValues"].Equals("true");

            //insert into database
            if (romanianTerritories || testDatabaseValues)
            {
                //chage state of regions
                var regions = JsonConvert.DeserializeObject<List<Region>>(File.ReadAllText(path + "\\regions.json"));
                foreach (var region in regions)
                {
                    context.Entry(region).State = EntityState.Added;
                    //save change to keep the regions id the same
                    context.SaveChanges();
                }

                //chage state of teritories
                var territories = JsonConvert.DeserializeObject<List<Territories>>(File.ReadAllText(path + "\\territories.json"));
                foreach (var territory in territories)
                {
                    context.Entry(territory).State = EntityState.Added;
                    //save change to keep the territories id the same
                    context.SaveChanges();
                }
            }
            if (testDatabaseValues)
            {
                //change state of customers
                var customers = JsonConvert.DeserializeObject<List<Customers>>(File.ReadAllText(path + "\\customers.json"));
                foreach (var customer in customers)
                {
                    context.Entry(customer).State = EntityState.Added;
                    //save change to keep the customers id the same
                    context.SaveChanges();
                }

                //chage state of shippers
                var shippers = JsonConvert.DeserializeObject<List<Shippers>>(File.ReadAllText(path + "\\shippers.json"));
                foreach (var shipper in shippers)
                {
                    context.Entry(shipper).State = EntityState.Added;
                    //save change to keep the shippers id the same
                    context.SaveChanges();
                }

                //chage state of suppliers
                var suppliers = JsonConvert.DeserializeObject<List<Suppliers>>(File.ReadAllText(path + "\\suppliers.json"));
                foreach (var supplier in suppliers)
                {
                    context.Entry(supplier).State = EntityState.Added;
                    //save change to keep the suppliers id the same
                    context.SaveChanges();
                }

                //chage state of categories
                var categories = JsonConvert.DeserializeObject<List<Categories>>(File.ReadAllText(path + "\\categories.json"));
                foreach (var category in categories)
                {
                    context.Entry(category).State = EntityState.Added;
                    //save change to keep the categories id the same
                    context.SaveChanges();
                }

                //chage state of product
                var products = JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText(path + "\\products.json"));
                foreach (var product in products)
                {
                    context.Entry(product).State = EntityState.Added;
                    //save change to keep the products id the same
                    context.SaveChanges();
                }

                //chage state of employees
                var employees = JsonConvert.DeserializeObject<List<Employees>>(File.ReadAllText(path + "\\emploayees.json"));
                List<int> reportsTo = new List<int>();
                foreach (var employee in employees)
                {
                    List<Territories> teritoryOfEmployees = new List<Territories>();
                    foreach (var ter in employee.Territories)
                    {
                        teritoryOfEmployees.Add(context.Territories.Find(ter.TerritoryID));
                    }
                    employee.Territories = teritoryOfEmployees;
                    //change reportsTo to the first emploayee (foreignkey exception)
                    reportsTo.Add(employee.ReportsTo ?? -1);
                    employee.ReportsTo = null;
                    context.Entry(employee).State = EntityState.Added;
                    //save change to keep the employees id the same
                    context.SaveChanges();
                }
                //change back reportsTo
                var emploayeeReportChange = context.Employees.ToList();
                for (int i = 0; i < emploayeeReportChange.Count(); i++)
                {
                    if (reportsTo[i] != -1)
                    {
                        emploayeeReportChange[i].ReportsTo = reportsTo[i];
                    }
                    else
                    {
                        emploayeeReportChange[i].ReportsTo = null;
                    }
                }
                context.SaveChanges();

                //chage state of orders
                var orders = JsonConvert.DeserializeObject<List<Orders>>(File.ReadAllText(path + "\\orders.json"));
                foreach (var order in orders)
                {
                    context.Entry(order).State = EntityState.Added;
                    //save change to keep the orders id the same
                    context.SaveChanges();
                }
            }
        }
    }
}