using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NorthwindWeb.Models;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

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
            string path = System.Configuration.ConfigurationManager.AppSettings["JsonDataInitializationPath"];

            context.Entry(new Shippers() { CompanyName = "asd", Phone = "asd", ShipperID = 1 }).State = EntityState.Added;
            context.SaveChanges();
            
            var categories = JsonConvert.DeserializeObject<List<Categories>>(File.ReadAllText(path + "\\categories.json"));
            var customers = JsonConvert.DeserializeObject<List<Customers>>(File.ReadAllText(path + "\\customers.json"));
            var employees = JsonConvert.DeserializeObject<List<Employees>>(File.ReadAllText(path + "\\emploayees.json"));
            var territories = JsonConvert.DeserializeObject<List<Territories>>(File.ReadAllText(path + "\\territories.json"));
            var suppliers = JsonConvert.DeserializeObject<List<Suppliers>>(File.ReadAllText(path + "\\suppliers.json"));
            var shippers = JsonConvert.DeserializeObject<List<Shippers>>(File.ReadAllText(path + "\\shippers.json"));
            var regions = JsonConvert.DeserializeObject<List<Region>>(File.ReadAllText(path + "\\regions.json"));
            var orders = JsonConvert.DeserializeObject<List<Orders>>(File.ReadAllText(path + "\\orders.json"));
            var products = JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText(path + "\\products.json"));
            var orderDetails = JsonConvert.DeserializeObject<List<Order_Details>>(File.ReadAllText(path + "\\orderDetails.json"));
            

            //chage state of teritories
            foreach(var region in regions)
            {
                context.Entry(region).State = EntityState.Added;
            }

            //chage state of teritories
            foreach (var territory in territories)
            {
                context.Entry(territory).State = EntityState.Added;
            }

            //change state of customers
            foreach (var customer in customers)
            {
                context.Entry(customer).State = EntityState.Added;
            }

            //chage state of shippers
            foreach (var shipper in shippers)
            {
                context.Entry(shipper).State = EntityState.Added;
            }

            //chage state of teritories
            foreach (var region in regions)
            {
                context.Entry(region).State = EntityState.Added;
            }

            //chage state of suppliers
            foreach (var supplier in suppliers)
            {
                context.Entry(supplier).State = EntityState.Added;
            }

            //chage state of categories
            foreach (var category in categories)
            {
                context.Entry(category).State = EntityState.Added;
            }

            //chage state of teritories
            foreach (var region in regions)
            {
                context.Entry(region).State = EntityState.Added;
            }

            //chage state of employees
            foreach (var employee in employees)
            {
                List<Territories> teritoryOfEmployees = new List<Territories>();
                foreach (var ter in employee.Territories)
                {
                    teritoryOfEmployees.Add(context.Territories.Find(ter.TerritoryID));
                }
                employee.Territories = teritoryOfEmployees;
                context.Entry(employee).State = EntityState.Added;
            }

            //chage state of product
            foreach (var product in products)
            {
                context.Entry(product).State = EntityState.Added;
            }

            //chage state of teritories
            foreach (var region in regions)
            {
                context.Entry(region).State = EntityState.Added;
            }

            //chage state of orders
            foreach (var order in orders)
            {
                context.Entry(order).State = EntityState.Added;
            }

            //chage state of order_details
            foreach (var orderDetail in orderDetails)
            {
                context.Entry(orderDetail).State = EntityState.Added;
            }

            context.SaveChanges();
        }
    }
}