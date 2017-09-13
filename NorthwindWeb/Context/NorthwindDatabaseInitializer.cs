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

            //list of region
            List<Region> regions = new List<Region>
            {
                //add region in list
                new Region() { RegionID = 1, RegionDescription = "Banat" },
                new Region() { RegionID = 2, RegionDescription = "Bucovina" },
                new Region() { RegionID = 3, RegionDescription = "Crisana" },
                new Region() { RegionID = 4, RegionDescription = "Dobrogea" },
                new Region() { RegionID = 5, RegionDescription = "Maramures" },
                new Region() { RegionID = 6, RegionDescription = "Moldova" },
                new Region() { RegionID = 7, RegionDescription = "Muntenia" },
                new Region() { RegionID = 8, RegionDescription = "Oltenia" },
                new Region() { RegionID = 9, RegionDescription = "Transilvania" }
            };

            //add territories in regions
            regions[0].Territories.Add(new Territories() { TerritoryID = "1", TerritoryDescription = "Timis", RegionID = 1 });
            regions[0].Territories.Add(new Territories() { TerritoryID = "2", TerritoryDescription = "Caras - Severin", RegionID = 1 });
            regions[1].Territories.Add(new Territories() { TerritoryID = "3", TerritoryDescription = "Botosani", RegionID = 2 });
            regions[1].Territories.Add(new Territories() { TerritoryID = "4", TerritoryDescription = "Suceava", RegionID = 2 });
            regions[2].Territories.Add(new Territories() { TerritoryID = "5", TerritoryDescription = "Bihor", RegionID = 3 });
            regions[2].Territories.Add(new Territories() { TerritoryID = "6", TerritoryDescription = "Arad", RegionID = 3 });
            regions[3].Territories.Add(new Territories() { TerritoryID = "7", TerritoryDescription = "Tulcea", RegionID = 4 });
            regions[3].Territories.Add(new Territories() { TerritoryID = "8", TerritoryDescription = "Constanta", RegionID = 4 });
            regions[4].Territories.Add(new Territories() { TerritoryID = "9", TerritoryDescription = "Satu - Mare", RegionID = 5 });
            regions[4].Territories.Add(new Territories() { TerritoryID = "10", TerritoryDescription = "Maramures", RegionID = 5 });
            regions[5].Territories.Add(new Territories() { TerritoryID = "11", TerritoryDescription = "Neamt", RegionID = 6 });
            regions[5].Territories.Add(new Territories() { TerritoryID = "12", TerritoryDescription = "Iasi", RegionID = 6 });
            regions[5].Territories.Add(new Territories() { TerritoryID = "13", TerritoryDescription = "Bacau", RegionID = 6 });
            regions[5].Territories.Add(new Territories() { TerritoryID = "14", TerritoryDescription = "Vaslui", RegionID = 6 });
            regions[5].Territories.Add(new Territories() { TerritoryID = "15", TerritoryDescription = "Vrancea", RegionID = 6 });
            regions[5].Territories.Add(new Territories() { TerritoryID = "16", TerritoryDescription = "Galati", RegionID = 6 });
            regions[6].Territories.Add(new Territories() { TerritoryID = "17", TerritoryDescription = "Braila", RegionID = 7 });
            regions[6].Territories.Add(new Territories() { TerritoryID = "18", TerritoryDescription = "Buzau", RegionID = 7 });
            regions[6].Territories.Add(new Territories() { TerritoryID = "19", TerritoryDescription = "Calarasi", RegionID = 7 });
            regions[6].Territories.Add(new Territories() { TerritoryID = "20", TerritoryDescription = "Prahova", RegionID = 7 });
            regions[6].Territories.Add(new Territories() { TerritoryID = "21", TerritoryDescription = "Dambovita", RegionID = 7 });
            regions[6].Territories.Add(new Territories() { TerritoryID = "22", TerritoryDescription = "Arges", RegionID = 7 });
            regions[6].Territories.Add(new Territories() { TerritoryID = "23", TerritoryDescription = "Ialomita", RegionID = 7 });
            regions[6].Territories.Add(new Territories() { TerritoryID = "24", TerritoryDescription = "Calarasi", RegionID = 7 });
            regions[6].Territories.Add(new Territories() { TerritoryID = "25", TerritoryDescription = "Ilfov", RegionID = 7 });
            regions[6].Territories.Add(new Territories() { TerritoryID = "26", TerritoryDescription = "Bucuresti", RegionID = 7 });
            regions[6].Territories.Add(new Territories() { TerritoryID = "27", TerritoryDescription = "Giurgiu", RegionID = 7 });
            regions[6].Territories.Add(new Territories() { TerritoryID = "28", TerritoryDescription = "Teleorman", RegionID = 7 });
            regions[7].Territories.Add(new Territories() { TerritoryID = "29", TerritoryDescription = "Gorj", RegionID = 8 });
            regions[7].Territories.Add(new Territories() { TerritoryID = "30", TerritoryDescription = "Valcea", RegionID = 8 });
            regions[7].Territories.Add(new Territories() { TerritoryID = "31", TerritoryDescription = "Olt", RegionID = 8 });
            regions[7].Territories.Add(new Territories() { TerritoryID = "32", TerritoryDescription = "Dolj", RegionID = 8 });
            regions[7].Territories.Add(new Territories() { TerritoryID = "33", TerritoryDescription = "Mehedinti", RegionID = 8 });
            regions[8].Territories.Add(new Territories() { TerritoryID = "34", TerritoryDescription = "Salaj", RegionID = 9 });
            regions[8].Territories.Add(new Territories() { TerritoryID = "35", TerritoryDescription = "Bistrita - Nasaud", RegionID = 9 });
            regions[8].Territories.Add(new Territories() { TerritoryID = "36", TerritoryDescription = "Cluj", RegionID = 9 });
            regions[8].Territories.Add(new Territories() { TerritoryID = "37", TerritoryDescription = "Mures", RegionID = 9 });
            regions[8].Territories.Add(new Territories() { TerritoryID = "38", TerritoryDescription = "Harghita", RegionID = 9 });
            regions[8].Territories.Add(new Territories() { TerritoryID = "39", TerritoryDescription = "Covasna", RegionID = 9 });
            regions[8].Territories.Add(new Territories() { TerritoryID = "40", TerritoryDescription = "Brasov", RegionID = 9 });
            regions[8].Territories.Add(new Territories() { TerritoryID = "41", TerritoryDescription = "Sibiu", RegionID = 9 });
            regions[8].Territories.Add(new Territories() { TerritoryID = "42", TerritoryDescription = "Alba", RegionID = 9 });
            regions[8].Territories.Add(new Territories() { TerritoryID = "43", TerritoryDescription = "Hunedoara", RegionID = 9 });

            foreach (var region in regions)
            {
                context.Regions.Add(region);
            }
            context.SaveChanges();

            //employees
            List<Employees> employees = new List<Employees>()
            {
                new Employees() {FirstName = "Florea", LastName = "Andrei", Title = "Vice Presedinte, Vanzari", TitleOfCourtesy = "Dr.", BirthDate = new DateTime(1952, 02, 18), HireDate = new DateTime(1992, 08, 13), Address = "Str.Hornului, nr. 22, ap 3, Bucuresti Sector 4", City = "Bucuresti", Region = "4", PostalCode = "031317", Country = "Romania", HomePhone = "0753255371", Extension = "", Notes = "Andrei a primit diploma de tehnician superior in 1984 si un doctorat in marketing international de la Universitatea din Dallas in 1991.Este fluent in franceza si italiana si citeste limba germana.A intrat in companie ca reprezentant de vanzari, a fost promovat in functia de director de vanzari in ianuarie 2002 si vicepresedinte al vanzarilor in martie 2003.Andrew este membru al camerei de comert din Seattle." },
                new Employees() {FirstName = "Danciu", LastName = "Nicoleta", Title = "Reprezentant Vanzari", TitleOfCourtesy = "Dra.", BirthDate = new DateTime(1948, 12, 07), HireDate = new DateTime(1992, 04, 30), Address = "Spl.Independentei, nr. 27, ap 27, Bucuresti Sector 6", City = "Bucuresti", Region = "6", PostalCode = "050082", Country = "Romania", HomePhone = "0751292371", Extension = "", Notes = "Are o diploma de licenta in psihologie de la Universitatea de Stat din Colorado in 1980.Ea a completat, de asemenea, \"Arta apelului rece\".Nicoleta este membru al Toastmasters International.", ReportsTo = 2 },
                new Employees() {FirstName = "Leca", LastName = "Ioana", Title = "Reprezentant Vanzari", TitleOfCourtesy = "Dra.", BirthDate = new DateTime(1963, 08, 29), HireDate = new DateTime(1992, 03, 31), Address = "Str.Cupolei, nr 54, ap 15, Bucuresti Sector 6", City = "Bucuresti", Region = "6", PostalCode = "061158", Country = "Romania", HomePhone = "0723292441", Extension = "", Notes = "Ioana are o diploma in chimie din Boston College(1994). A finalizat un program de certificare in domeniul managementul vanzarii cu amanuntul a produselor alimentare. Ioana a fost angajata ca vanzatoare in 2001 si a fost promovata pe postul reprezentant vanzari in februarie 2002.", ReportsTo = 2 },
                new Employees() {FirstName = "Pescaru", LastName = "Margareta", Title = "Reprezentant Vanzari", TitleOfCourtesy = "Dra", BirthDate = new DateTime(1937, 09, 18), HireDate = new DateTime(1993, 05, 02), Address = "Str.Hornului, nr. 22, ap 4, Bucuresti Sector 4", City = "Bucuresti", Region = "4", PostalCode = "031317", Country = "Romania", HomePhone = "0752212371", Extension = "", Notes = "Margareta detine o diploma de licenta in literatura engleza de la Concordia College(1978) si o diploma de licenta de la Institutul American de Arta culinara(1986).", ReportsTo = 2 },
                new Employees() {FirstName = "Bucurescu", LastName = "Stefan", Title = "Manager Vanzari", TitleOfCourtesy = "Dl.", BirthDate = new DateTime(1955, 03, 03), HireDate = new DateTime(1993, 10, 16), Address = "Str.Preciziei, nr 2, ap 32, Bucuresti Sector 6", City = "Bucuresti", Region = "6", PostalCode = "062203", Country = "Romania", HomePhone = "0712292991", Extension = "", Notes = "Stefan a absolvit Universitatea din St. Andrews, Scotia, cu diploma in stiinte in 1986.Dupa ce s-a alaturat companiei ca reprezentant de vanzari in 2002, a petrecut 6 luni intr-un program de orientare la biroul din Seattle si apoi a revenit la postul sau permanent in Londra.A fost promovat manager de vanzari in martie 1993.Domnul Buchanan a absolvit cursurile \"Telemarketing de succes\" si \"Managementul vanzarilor internationale\".Este fluent in franceza.", ReportsTo = 2 },
                new Employees() {FirstName = "Surdu", LastName = "Mihai", Title = "Reprezentant Vanzari", TitleOfCourtesy = "Dl.", BirthDate = new DateTime(1963, 07, 01), HireDate = new DateTime(1993, 10, 16), Address = "Str Ghirlandei, nr 5, ap 40, Bucuresti Sector 6", City = "Bucuresti", Region = "6", PostalCode = "062242", Country = "Romania", HomePhone = "0751217471", Extension = "", Notes = "Mihai este absolvent al Universitatii din Sussex(MA, economie, 1993) si al Universitatii din California, Los Angeles(MBA, marketing, 1996). De asemenea, a luat cursurile \"Vanzari multiculturale\" si \"Managementul timpului pentru profesionistii in vanzari\".Este fluent in limba japoneza si poate citi si scrie franceza, portugheza si spaniola.", ReportsTo = 5 },
                new Employees() {FirstName = "Kiritescu", LastName = "Robert", Title = "Reprezentant Vanzari", TitleOfCourtesy = "Dl.", BirthDate = new DateTime(1960, 05, 28), HireDate = new DateTime(1994, 01, 01), Address = "Str.Fabricii, nr 4, ap 4, Bucuresti Sector 6", City = "Bucuresti", Region = "6", PostalCode = "060823", Country = "Romania", HomePhone = "0718642371", Extension = "", Notes = "Robert a fost in Corpul Pacii si a calatorit mult inaintae de a - si incheia studiile in limba engleza la Universitatea din Michigan in 2002, anul in care sa alatura companiei. Dupa finalizarea cursului de \"Vanzarea in Europa\" a fost transferat la biroul din Londra in martie 2003.", ReportsTo = 5 },
                new Employees() {FirstName = "Cojocaru", LastName = "Laura", Title = "Coordonator Vanzari Intern", TitleOfCourtesy = "Dra.", BirthDate = new DateTime(1958, 01, 08), HireDate = new DateTime(1994, 03, 04), Address = "Str.Lugoj, nr 2, ap 12, Bucuresti Sector 6", City = "Bucuresti", Region = "6", PostalCode = "012212", Country = "Romania", HomePhone = "0712392335", Extension = "", Notes = "Laura a obtinut o diploma in psihologie la Universitatea din Washington.De asemenea, a absolvit un curs de afaceri francez. Citeste si scrie franceza.", ReportsTo = 2 },
                new Employees() {FirstName = "Dobrescu", LastName = "Ana", Title = "Reprezentant Vanzari", TitleOfCourtesy = "Dra.", BirthDate = new DateTime(1966, 01, 26), HireDate = new DateTime(1994, 11, 14), Address = "Str.Comana, nr 2, ap 23, Bucuresti Sector 6", City = "Bucuresti", Region = "6", PostalCode = "011274", Country = "Romania", HomePhone = "0764882331", Extension = "", Notes = "Ana are o diploma de licenta in limba engleza de la Colegiul St. Lawrence.Este fluenta in franceza si germana.", ReportsTo = 5 }
            };

            employees[0].Territories = context.Territories.Where(x => x.TerritoryID == "12" || x.TerritoryID == "19" || x.TerritoryID == "23" || x.TerritoryID == "26" || x.TerritoryID == "27").ToList();
            employees[1].Territories = context.Territories.Where(x => x.TerritoryID == "10" || x.TerritoryID == "34" || x.TerritoryID == "35" || x.TerritoryID == "36" || x.TerritoryID == "5" || x.TerritoryID == "9").ToList();
            employees[2].Territories = context.Territories.Where(x => x.TerritoryID == "11" || x.TerritoryID == "12" || x.TerritoryID == "3" || x.TerritoryID == "35" || x.TerritoryID == "37" || x.TerritoryID == "38" || x.TerritoryID == "4").ToList();
            employees[3].Territories = context.Territories.Where(x => x.TerritoryID == "11" || x.TerritoryID == "12" || x.TerritoryID == "13" || x.TerritoryID == "14" || x.TerritoryID == "16").ToList();
            employees[4].Territories = context.Territories.Where(x => x.TerritoryID == "1" || x.TerritoryID == "2" || x.TerritoryID == "29" || x.TerritoryID == "33" || x.TerritoryID == "42" || x.TerritoryID == "43" || x.TerritoryID == "6").ToList();
            employees[5].Territories = context.Territories.Where(x => x.TerritoryID == "22" || x.TerritoryID == "28" || x.TerritoryID == "29" || x.TerritoryID == "30" || x.TerritoryID == "31" || x.TerritoryID == "32" || x.TerritoryID == "33").ToList();
            employees[6].Territories = context.Territories.Where(x => x.TerritoryID == "36" || x.TerritoryID == "37" || x.TerritoryID == "38" || x.TerritoryID == "39" || x.TerritoryID == "40" || x.TerritoryID == "41" || x.TerritoryID == "42").ToList();
            employees[7].Territories = context.Territories.Where(x => x.TerritoryID == "15" || x.TerritoryID == "16" || x.TerritoryID == "17" || x.TerritoryID == "18" || x.TerritoryID == "20" || x.TerritoryID == "21" || x.TerritoryID == "25" || x.TerritoryID == "26" || x.TerritoryID == "39" || x.TerritoryID == "40" || x.TerritoryID == "7").ToList();
            employees[8].Territories = context.Territories.Where(x => x.TerritoryID == "17" || x.TerritoryID == "18" || x.TerritoryID == "20" || x.TerritoryID == "21" || x.TerritoryID == "23" || x.TerritoryID == "24" || x.TerritoryID == "26" || x.TerritoryID == "27" || x.TerritoryID == "7" || x.TerritoryID == "8").ToList();

            foreach (var empolyee in employees)
            {
                context.Employees.Add(empolyee);
                //to keep the order
                context.SaveChanges();
            }

            //shippers
            List<Shippers> shippers = new List<Shippers>
            {
                new Shippers(){ CompanyName = "FedEx        ", Phone = "+40213034567  "},
                new Shippers(){ CompanyName = "Urgent Cargus", Phone = "021 9330      "},
                new Shippers(){ CompanyName = "FAN Courier  ", Phone = "+40742552233  " },
            };

            foreach (var shipper in shippers)
            {
                context.Shippers.Add(shipper);
            }
            context.SaveChanges();

            //category (they need to be added in this order)
            context.Categories.Add(new Categories() { CategoryName = "Clasice", Description = "Telefoane cu butoane" });
            context.SaveChanges();
            context.Categories.Add(new Categories() { CategoryName = "Smartphone", Description = "Touchscreen" });
            context.SaveChanges();
            context.Categories.Add(new Categories() { CategoryName = "Accesorii", Description = "Selfie sticks, Incarcatoare, Casti, Baterii, Huse" });
            context.SaveChanges();
            context.Categories.Add(new Categories() { CategoryName = "Gadgeturi", Description = "Boxe, Ochelari VR, Telecomenzi" });
            context.SaveChanges();
            context.Categories.Add(new Categories() { CategoryName = "eBook Reader", Description = "Bookreader" });
            context.SaveChanges();
            context.Categories.Add(new Categories() { CategoryName = "Servicii", Description = "Servicii oferite" });
            context.SaveChanges();

            //Suppliers (they need to be added in this order)
            context.Suppliers.Add(new Suppliers() { CompanyName = "EURO GSM IMPEX S.R.L.", ContactName = "Ion Vasilde", ContactTitle = "Proprietar", Address = "B-dul Muncii nr.18", City = "CLUJ - NAPOCA", PostalCode = "400641", Country = "Romania", Phone = "0264450450", HomePage = "https://eurogsm.ro" });
            context.SaveChanges();
            context.Suppliers.Add(new Suppliers() { CompanyName = "GERSIM IMPEX S.R.L.", ContactName = "Mircea Daniel", ContactTitle = "Manager depozit", Address = "Strada Bilciure?ti 9A", City = "BUCURESTI", PostalCode = "014012", Country = "Romania", Phone = "0213264850", Fax = "0213264851", HomePage = "http://www.gersim.ro" });
            context.SaveChanges();
            context.Suppliers.Add(new Suppliers() { CompanyName = "EMAG S.A.", ContactName = "Dumitru George", ContactTitle = "Agent Vanzari", Address = "Windsor Building Sos.Bucuresti Nord nr. 15 - 23 ", City = "ILFOV", PostalCode = "077190", Country = "Romania", Phone = "0722.25.00.00", HomePage = "https://emag.ro" });
            context.SaveChanges();
            context.Suppliers.Add(new Suppliers() { CompanyName = "SC MEDIA GALAXY S.R.L.", ContactName = "Popescu Mihai ", ContactTitle = "Reprezentant Vanzari", Address = "Bulevardul Poligrafiei Nr.1, Sector 1", City = "Bucuresti", PostalCode = "400641", Country = "Romania", Phone = "0212062000", Fax = "0213199939", HomePage = "www.mediagalaxy.ro" });
            context.SaveChanges();

            //products (they need to be added in this order)
            context.Products.Add(new Products() { ProductName = "Kindle 6 Glare Touch Screen WiFi Black", SupplierID = 1, CategoryID = 5, QuantityPerUnit = "1", UnitPrice = (decimal?)339.00, UnitsInStock = 39, UnitsOnOrder = 0, ReorderLevel = 10, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Kindle PaperWhite Model 2015 Black", SupplierID = 2, CategoryID = 5, QuantityPerUnit = "1", UnitPrice = (decimal?)629.00, UnitsInStock = 17, UnitsOnOrder = 40, ReorderLevel = 25, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Kindle PaperWhite Model 2015 White", SupplierID = 2, CategoryID = 5, QuantityPerUnit = "1", UnitPrice = (decimal?)599.00, UnitsInStock = 13, UnitsOnOrder = 70, ReorderLevel = 25, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "PocketBook Touch LUX 3 Red pb626", SupplierID = 1, CategoryID = 5, QuantityPerUnit = "1", UnitPrice = (decimal?)569.00, UnitsInStock = 53, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "PocketBook Touch LUX 3 White pb626", SupplierID = 2, CategoryID = 5, QuantityPerUnit = "1", UnitPrice = (decimal?)569.00, UnitsInStock = 0, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "PocketBook Touch LUX 3 Grey pb626", SupplierID = 1, CategoryID = 5, QuantityPerUnit = "1", UnitPrice = (decimal?)569.00, UnitsInStock = 120, UnitsOnOrder = 0, ReorderLevel = 25, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "PocketBook Touch HD Black pb631", SupplierID = 4, CategoryID = 5, QuantityPerUnit = "1", UnitPrice = (decimal?)799.00, UnitsInStock = 15, UnitsOnOrder = 0, ReorderLevel = 10, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Bookeen CybooK Muse FrontLight Black", SupplierID = 3, CategoryID = 5, QuantityPerUnit = "1", UnitPrice = (decimal?)699.00, UnitsInStock = 6, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Prestigio MultiReader SUPREME 4GB Black", SupplierID = 2, CategoryID = 5, QuantityPerUnit = "1", UnitPrice = (decimal?)549.00, UnitsInStock = 29, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Bookeen Cybook Muse HD 8GB Black", SupplierID = 4, CategoryID = 5, QuantityPerUnit = "1", UnitPrice = (decimal?)569.00, UnitsInStock = 31, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Bookeen Cybook Muse Light 4GB Black", SupplierID = 3, CategoryID = 5, QuantityPerUnit = "1", UnitPrice = (decimal?)579.00, UnitsInStock = 22, UnitsOnOrder = 30, ReorderLevel = 30, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "CAT B25 Dual SIM Black", SupplierID = 2, CategoryID = 1, QuantityPerUnit = "1", UnitPrice = (decimal?)229.00, UnitsInStock = 86, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Nokia 3310 Dual SIM Dark Blue", SupplierID = 1, CategoryID = 1, QuantityPerUnit = "1", UnitPrice = (decimal?)249.00, UnitsInStock = 24, UnitsOnOrder = 0, ReorderLevel = 5, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Alcatel Tiger X3 1016G Black", SupplierID = 4, CategoryID = 1, QuantityPerUnit = "1", UnitPrice = (decimal?)69.00, UnitsInStock = 35, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Nokia 3310 Single Sim Orange", SupplierID = 1, CategoryID = 1, QuantityPerUnit = "1", UnitPrice = (decimal?)249.00, UnitsInStock = 39, UnitsOnOrder = 0, ReorderLevel = 5, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Nokia 130 Dual SIM Red", SupplierID = 2, CategoryID = 1, QuantityPerUnit = "1", UnitPrice = (decimal?)99.00, UnitsInStock = 29, UnitsOnOrder = 0, ReorderLevel = 10, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Alcatel 1054 White", SupplierID = 1, CategoryID = 1, QuantityPerUnit = "1", UnitPrice = (decimal?)83.00, UnitsInStock = 0, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Nokia 150 Single Sim White", SupplierID = 3, CategoryID = 1, QuantityPerUnit = "1", UnitPrice = (decimal?)141.00, UnitsInStock = 42, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "MaxCom MM 141 Dual Sim Grey", SupplierID = 2, CategoryID = 1, QuantityPerUnit = "1", UnitPrice = (decimal?)101.00, UnitsInStock = 25, UnitsOnOrder = 0, ReorderLevel = 5, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Alcatel 2008G Black-Silver", SupplierID = 1, CategoryID = 1, QuantityPerUnit = "1", UnitPrice = (decimal?)165.00, UnitsInStock = 40, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Nokia 216 Dual Sim Black", SupplierID = 3, CategoryID = 1, QuantityPerUnit = "1", UnitPrice = (decimal?)156.00, UnitsInStock = 3, UnitsOnOrder = 40, ReorderLevel = 5, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Nokia 216 Dual SIM Grey", SupplierID = 1, CategoryID = 1, QuantityPerUnit = "1", UnitPrice = (decimal?)156.00, UnitsInStock = 104, UnitsOnOrder = 0, ReorderLevel = 25, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Karbonn K-flip Dual Sim White", SupplierID = 1, CategoryID = 1, QuantityPerUnit = "1", UnitPrice = (decimal?)127.00, UnitsInStock = 61, UnitsOnOrder = 0, ReorderLevel = 25, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "MyPhone Metro Red", SupplierID = 2, CategoryID = 1, QuantityPerUnit = "1", UnitPrice = (decimal?)209.00, UnitsInStock = 20, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "MyPhone 6310 Dual Sim Black", SupplierID = 3, CategoryID = 1, QuantityPerUnit = "1", UnitPrice = (decimal?)104.00, UnitsInStock = 76, UnitsOnOrder = 0, ReorderLevel = 30, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Bratara Xiaomi Silicon - Roz", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)24.00, UnitsInStock = 15, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Bratara Xiaomi Silicon - Verde", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)24.00, UnitsInStock = 49, UnitsOnOrder = 0, ReorderLevel = 30, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Curea Ceas 910XT GPS Negru", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)86.00, UnitsInStock = 26, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Bratara Gear S3 Silicon Maron", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)127.00, UnitsInStock = 0, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Curea Apple Watch 38mm Piele Neagra", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)964.00, UnitsInStock = 10, UnitsOnOrder = 0, ReorderLevel = 15, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Dock Slate Union Pentru Apple Watch", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)217.00, UnitsInStock = 0, UnitsOnOrder = 70, ReorderLevel = 20, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Dock Native Union Luxury Tech Marble", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)423.00, UnitsInStock = 9, UnitsOnOrder = 40, ReorderLevel = 25, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Stand de incarcare Huawei Watch", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)95.00, UnitsInStock = 112, UnitsOnOrder = 0, ReorderLevel = 20, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Cablu de incarcare Fitbit Flex", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)25.00, UnitsInStock = 111, UnitsOnOrder = 0, ReorderLevel = 15, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Husa Apple Watch 38mm", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)49.00, UnitsInStock = 20, UnitsOnOrder = 0, ReorderLevel = 15, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Bratara Smartwatch Silicon Argintiur", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)128.00, UnitsInStock = 112, UnitsOnOrder = 0, ReorderLevel = 20, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Bratara Smartwatch Piele Neagra", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)115.00, UnitsInStock = 11, UnitsOnOrder = 50, ReorderLevel = 25, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Bratara Smartwatch Silicon Khaki", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)86.00, UnitsInStock = 17, UnitsOnOrder = 0, ReorderLevel = 15, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Bratara Smartwatch Silicon Blue Black", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)86.00, UnitsInStock = 69, UnitsOnOrder = 0, ReorderLevel = 5, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Folie Protectie Curbata 42 mm Negra", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)36.00, UnitsInStock = 123, UnitsOnOrder = 0, ReorderLevel = 30, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Folie Protectie Curbata 38 mm Negra", SupplierID = 4, CategoryID = 3, QuantityPerUnit = "1", UnitPrice = (decimal?)36.00, UnitsInStock = 85, UnitsOnOrder = 0, ReorderLevel = 10, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Apple iPhone 7 32GB Black", SupplierID = 1, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)2999.00, UnitsInStock = 26, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "OnePlus 5 A5000 64GB Dual SIM 4G Black", SupplierID = 2, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)2599.00, UnitsInStock = 17, UnitsOnOrder = 10, ReorderLevel = 25, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Samsung Galaxy A3(2017) 4G Black", SupplierID = 3, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)1199.00, UnitsInStock = 27, UnitsOnOrder = 0, ReorderLevel = 15, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Samsung Galaxy J5(2016) Dual SIM Gold", SupplierID = 4, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)849.00, UnitsInStock = 5, UnitsOnOrder = 70, ReorderLevel = 15, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Samsung Galaxy S8 G950F 64GB 4G Black", SupplierID = 1, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)2989.00, UnitsInStock = 95, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Apple iPhone 6 32GB Space Gray", SupplierID = 2, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)1899.00, UnitsInStock = 36, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Samsung Galaxy J1 Mini Prime Black", SupplierID = 3, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)349.00, UnitsInStock = 15, UnitsOnOrder = 70, ReorderLevel = 25, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Samsung Galaxy J1 Mini Prime Gold", SupplierID = 2, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)349.00, UnitsInStock = 10, UnitsOnOrder = 60, ReorderLevel = 15, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Xiaomi Redmi 4A 32GB Dark Grey", SupplierID = 4, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)499.00, UnitsInStock = 65, UnitsOnOrder = 0, ReorderLevel = 30, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Lenovo Moto Z 32GB Dual Sim 4G Black", SupplierID = 3, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)1659.00, UnitsInStock = 20, UnitsOnOrder = 0, ReorderLevel = 10, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Samsung Galaxy S8 Plus 64GB 4G Black", SupplierID = 2, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)3549.00, UnitsInStock = 38, UnitsOnOrder = 0, ReorderLevel = 25, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "HTC 10 32GB 4G Gold", SupplierID = 1, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)1799.00, UnitsInStock = 0, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Huawei P10 Lite 32GB Dual Sim 4G Gold", SupplierID = 3, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)1199.00, UnitsInStock = 21, UnitsOnOrder = 0, ReorderLevel = 10, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Apple iPhone SE 32GB Space Gray", SupplierID = 1, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)1699.00, UnitsInStock = 115, UnitsOnOrder = 0, ReorderLevel = 20, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Huawei P10 Lite 32GB Dual Sim 4G Black", SupplierID = 3, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)1199.00, UnitsInStock = 21, UnitsOnOrder = 10, ReorderLevel = 30, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Samsung Galaxy J1 Prime White", SupplierID = 1, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)339.00, UnitsInStock = 36, UnitsOnOrder = 0, ReorderLevel = 20, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Huawei P10 Lite 32GB Blue", SupplierID = 4, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)1199.00, UnitsInStock = 62, UnitsOnOrder = 0, ReorderLevel = 20, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Samsung Galaxy S6 Edge 32GB Black", SupplierID = 4, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)1899.00, UnitsInStock = 79, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Sony Xperia X Compact 32GB 4G Black", SupplierID = 4, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)1599.00, UnitsInStock = 19, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "LG G5 SE H840 32GB Titanium Grey", SupplierID = 4, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)1349.00, UnitsInStock = 113, UnitsOnOrder = 0, ReorderLevel = 25, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "iPhone 6s 32GB 32gb Space Grey", SupplierID = 4, CategoryID = 2, QuantityPerUnit = "1", UnitPrice = (decimal?)2599.00, UnitsInStock = 17, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Boxa Portabila Emie Cybertron Wireless", SupplierID = 1, CategoryID = 4, QuantityPerUnit = "1", UnitPrice = (decimal?)749.00, UnitsInStock = 24, UnitsOnOrder = 0, ReorderLevel = 5, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Ochelari Samsung Gear VR 2 SM - R323 Negru", SupplierID = 1, CategoryID = 4, QuantityPerUnit = "1", UnitPrice = (decimal?)209.00, UnitsInStock = 22, UnitsOnOrder = 80, ReorderLevel = 30, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Manusi cu Casca Bluetooth Hi - Fun M Black", SupplierID = 1, CategoryID = 4, QuantityPerUnit = "1", UnitPrice = (decimal?)229.00, UnitsInStock = 76, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Dispozitiv monitorizare somn SenSe Sleep", SupplierID = 1, CategoryID = 4, QuantityPerUnit = "1", UnitPrice = (decimal?)119.00, UnitsInStock = 4, UnitsOnOrder = 100, ReorderLevel = 20, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Telecomanda Bluetooth Esperanza", SupplierID = 1, CategoryID = 4, QuantityPerUnit = "1", UnitPrice = (decimal?)28.00, UnitsInStock = 52, UnitsOnOrder = 0, ReorderLevel = 10, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Caciula Stereo cu Microfon Negru", SupplierID = 1, CategoryID = 4, QuantityPerUnit = "1", UnitPrice = (decimal?)37.00, UnitsInStock = 6, UnitsOnOrder = 10, ReorderLevel = 15, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Dispozitiv localizare cu Seeker", SupplierID = 1, CategoryID = 4, QuantityPerUnit = "1", UnitPrice = (decimal?)29.00, UnitsInStock = 26, UnitsOnOrder = 0, ReorderLevel = 15, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Telecomanda Media-Tech pentru VR", SupplierID = 1, CategoryID = 4, QuantityPerUnit = "1", UnitPrice = (decimal?)39.00, UnitsInStock = 15, UnitsOnOrder = 10, ReorderLevel = 30, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Drona Arcade Orbit", SupplierID = 1, CategoryID = 4, QuantityPerUnit = "1", UnitPrice = (decimal?)149.00, UnitsInStock = 26, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Camera Video Fondi OnReal Negru", SupplierID = 1, CategoryID = 4, QuantityPerUnit = "1", UnitPrice = (decimal?)514.00, UnitsInStock = 14, UnitsOnOrder = 0, ReorderLevel = 0, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Telecomanda Arcade Bluetooth", SupplierID = 1, CategoryID = 4, QuantityPerUnit = "1", UnitPrice = (decimal?)74.00, UnitsInStock = 101, UnitsOnOrder = 0, ReorderLevel = 5, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Robot Inteligent Interactiv Ubtech Alpha", SupplierID = 1, CategoryID = 4, QuantityPerUnit = "1", UnitPrice = (decimal?)2369.00, UnitsInStock = 4, UnitsOnOrder = 20, ReorderLevel = 5, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Robot Inteligent de Serviciu Uno", SupplierID = 1, CategoryID = 4, QuantityPerUnit = "1", UnitPrice = (decimal?)3249.00, UnitsInStock = 125, UnitsOnOrder = 0, ReorderLevel = 25, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Boxa Portabila Flip 4 Waterproof Black", SupplierID = 1, CategoryID = 4, QuantityPerUnit = "1", UnitPrice = (decimal?)579.00, UnitsInStock = 57, UnitsOnOrder = 0, ReorderLevel = 20, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Boxa Portabila  Wireless Cu Microfon", SupplierID = 1, CategoryID = 4, QuantityPerUnit = "1", UnitPrice = (decimal?)499.00, UnitsInStock = 32, UnitsOnOrder = 0, ReorderLevel = 15, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Diagnosticare", SupplierID = 1, CategoryID = 6, QuantityPerUnit = "1", UnitPrice = (decimal?)60.00, UnitsInStock = 0, UnitsOnOrder = 10, ReorderLevel = 1, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Inlocuire Baterie", SupplierID = 1, CategoryID = 6, QuantityPerUnit = "1", UnitPrice = (decimal?)120.00, UnitsInStock = 0, UnitsOnOrder = 9, ReorderLevel = 1, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Inlocuire ecran", SupplierID = 1, CategoryID = 6, QuantityPerUnit = "1", UnitPrice = (decimal?)400.00, UnitsInStock = 0, UnitsOnOrder = 9, ReorderLevel = 1, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Inlocuire folie de protectie", SupplierID = 1, CategoryID = 6, QuantityPerUnit = "1", UnitPrice = (decimal?)70.00, UnitsInStock = 0, UnitsOnOrder = 9, ReorderLevel = 1, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Inlocuire placa de baza", SupplierID = 1, CategoryID = 6, QuantityPerUnit = "1", UnitPrice = (decimal?)900.00, UnitsInStock = 0, UnitsOnOrder = 9, ReorderLevel = 1, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Instalare android", SupplierID = 1, CategoryID = 6, QuantityPerUnit = "1", UnitPrice = (decimal?)80.00, UnitsInStock = 0, UnitsOnOrder = 9, ReorderLevel = 1, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Instalare IOS", SupplierID = 1, CategoryID = 6, QuantityPerUnit = "1", UnitPrice = (decimal?)120.00, UnitsInStock = 0, UnitsOnOrder = 9, ReorderLevel = 1, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Instalare windows phone", SupplierID = 1, CategoryID = 6, QuantityPerUnit = "1", UnitPrice = (decimal?)80.00, UnitsInStock = 0, UnitsOnOrder = 9, ReorderLevel = 1, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Recuperare date windows phone", SupplierID = 1, CategoryID = 6, QuantityPerUnit = "1", UnitPrice = (decimal?)140.00, UnitsInStock = 0, UnitsOnOrder = 9, ReorderLevel = 1, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Recuperare date android", SupplierID = 1, CategoryID = 6, QuantityPerUnit = "1", UnitPrice = (decimal?)120.00, UnitsInStock = 0, UnitsOnOrder = 9, ReorderLevel = 1, Discontinued = false });
            context.SaveChanges();
            context.Products.Add(new Products() { ProductName = "Recuperare date iOS", SupplierID = 1, CategoryID = 6, QuantityPerUnit = "1", UnitPrice = (decimal?)160.00, UnitsInStock = 0, UnitsOnOrder = 9, ReorderLevel = 1, Discontinued = false });
            context.SaveChanges();

            //customers and orders
            List<Customers> customers = new List<Customers>
            {
                new Customers(){ CustomerID = "ALFKI", CompanyName = "Vinyl Fever",               ContactName = "Danut Gogean"        , ContactTitle = "Reprezentant Vanzari" , Address = "STR. 10 MAI nr. 15, DaMBOVITA"           , City = "Targoviste", Region = "Muntenia"    , PostalCode = "130062", Country = "Romania", Phone = "0245-216 446" },
                new Customers(){ CustomerID = "ANATR", CompanyName = "Kash n",                    ContactName = "Gabriella Anghelescu", ContactTitle = "Patron"               , Address = "Strada Caraiman 3, Constanta"            , City = "Constanta" , Region = "Dobrogea"    , PostalCode = "907021", Country = "Romania", Phone = "0723-564 218"  , Fax = "0251.411688" },
                new Customers(){ CustomerID = "ANTON", CompanyName = "Tech Hifi",                 ContactName = "Dorin Butacu"        , ContactTitle = "Patron"               , Address = "Piata Revolutiei 3/26, Maramures"        , City = "Maramures" , Region = "Maramures"   , PostalCode = "873309", Country = "Romania", Phone = "+40(262)260999", Fax = "+40(262)271338" },
                new Customers(){ CustomerID = "AROUT", CompanyName = "Beatties",                  ContactName = "Ioana Draghici"      , ContactTitle = "Reprezentant Vanzari" , Address = "STR. VULCAN SAMUIL nr. 16, BEIUS"        , City = "BIHOR"     , Region = "Crisana"     , PostalCode = "653271", Country = "Romania", Phone = "0259-320 222"  , Fax = "0251.418803" },
                new Customers(){ CustomerID = "BERGS", CompanyName = "Cut Above",                 ContactName = "Varujan Puscas"      , ContactTitle = "Administrator Comenzi", Address = "Bulevardul Ion Mihalache 148B, Bucuresti", City = "Bucuresti" , Region = "Muntenia"    , PostalCode = "666708", Country = "Romania", Phone = "+40(21)2246714", Fax = "0251.413102" },
                new Customers(){ CustomerID = "BLAUS", CompanyName = "Sears Homelife",            ContactName = "Stefan Manole"       , ContactTitle = "Reprezentant Vanzari" , Address = "STR. 9 MAI, BACAU"                       , City = "Bacau"     , Region = "Moldova"     , PostalCode = "546708", Country = "Romania", Phone = "0740-082 824"  , Fax = "0251.413102" },
                new Customers(){ CustomerID = "BLONP", CompanyName = "Century House",             ContactName = "Varujan Teodorescu"  , ContactTitle = "Director Marketing"   , Address = "STR. BARNUTIU SIMION nr. 67, SALAJ"      , City = "SALAJ"     , Region = "Transilvania", PostalCode = "437945", Country = "Romania", Phone = "0260-616 920"  , Fax = "0251.418803" },
             };

            foreach(var customer in customers)
            {
                context.Customers.Add(customer);
            }
            context.SaveChanges();

            //orders
            context.Orders.Add(new Orders() { CustomerID = "ALFKI", EmployeeID = 2, OrderDate = new DateTime(1996, 09, 22), ShippedDate = new DateTime(1996, 10, 20), RequiredDate = new DateTime(1996, 10, 02), ShipVia = 2, Freight = (decimal?)40.26, ShipName = "Vinyl Fever", ShipAddress = "STR. 10 MAI nr. 15, DaMBOVITA", ShipCity = "Targoviste", ShipRegion = "Muntenia", ShipPostalCode = "453993", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "ALFKI", EmployeeID = 1, OrderDate = new DateTime(1996, 11, 18), ShippedDate = new DateTime(1996, 12, 16), RequiredDate = new DateTime(1996, 12, 01), ShipVia = 3, Freight = (decimal?)34.88, ShipName = "Vinyl Fever", ShipAddress = "STR. 10 MAI nr. 15, DaMBOVITA", ShipCity = "Targoviste", ShipRegion = "Muntenia", ShipPostalCode = "453993", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "ALFKI", EmployeeID = 1, OrderDate = new DateTime(1996, 12, 04), ShippedDate = new DateTime(1997, 01, 01), RequiredDate = new DateTime(1996, 12, 08), ShipVia = 3, Freight = (decimal?)3.94, ShipName = "Vinyl Fever", ShipAddress = "STR. 10 MAI nr. 15, DaMBOVITA", ShipCity = "Targoviste", ShipRegion = "Muntenia", ShipPostalCode = "453993", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "ANATR", EmployeeID = 8, OrderDate = new DateTime(1996, 08, 12), ShippedDate = new DateTime(1996, 09,  09), RequiredDate = new DateTime(1996, 08, 15), ShipVia = 2, Freight = (decimal?)25.83, ShipName = "Kash n", ShipAddress = "Strada Caraiman 3, Constanta", ShipCity = "Constanta", ShipRegion = "Dobrogea", ShipPostalCode = "907021", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "ANATR", EmployeeID = 4, OrderDate = new DateTime(1997, 02, 09), ShippedDate = new DateTime(1997, 03, 09), RequiredDate = new DateTime(1997, 02, 27), ShipVia = 2, Freight = (decimal?)86.53, ShipName = "Kash n", ShipAddress = "Strada Caraiman 3, Constanta", ShipCity = "Constanta", ShipRegion = "Dobrogea", ShipPostalCode = "907021", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "ANATR", EmployeeID = 4, OrderDate = new DateTime(1997, 04, 01), ShippedDate = new DateTime(1997, 04, 29), RequiredDate = new DateTime(1997, 04, 08), ShipVia = 2, Freight = (decimal?)65.99, ShipName = "Kash n", ShipAddress = "Strada Caraiman 3, Constanta", ShipCity = "Constanta", ShipRegion = "Dobrogea", ShipPostalCode = "907021", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "ANATR", EmployeeID = 3, OrderDate = new DateTime(1997, 08, 07), ShippedDate = new DateTime(1997, 09, 04), RequiredDate = new DateTime(1997, 08, 13), ShipVia = 1, Freight = (decimal?)43.90, ShipName = "Ana", ShipAddress = "Strada Caraiman 3, Constanta", ShipCity = "Constanta", ShipRegion = "Dobrogea", ShipPostalCode = "05021", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "ANTON", EmployeeID = 6, OrderDate = new DateTime(1996, 07, 31), ShippedDate = new DateTime(1996, 08, 28), RequiredDate = new DateTime(1996, 08, 29), ShipVia = 2, Freight = (decimal?)4.54, ShipName = "Tech Hifi", ShipAddress = "Piata Revolutiei 3/26, Maramures", ShipCity = "Maramures", ShipRegion = "Maramures", ShipPostalCode = "873309", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "ANTON", EmployeeID = 1, OrderDate = new DateTime(1996, 10, 08), ShippedDate = new DateTime(1996, 10, 22), RequiredDate = new DateTime(1996, 10, 13), ShipVia = 3, Freight = (decimal?)64.86, ShipName = "Tech Hifi", ShipAddress = "Piata Revolutiei 3/26, Maramures", ShipCity = "Maramures", ShipRegion = "Maramures", ShipPostalCode = "873309", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "AROUT", EmployeeID = 7, OrderDate = new DateTime(1996, 10, 01), ShippedDate = new DateTime(1996, 10, 29), RequiredDate = new DateTime(1996, 10, 10), ShipVia = 3, Freight = (decimal?)64.50, ShipName = "Beatties", ShipAddress = "STR. VULCAN SAMUIL nr. 16, BEIUS", ShipCity = "BIHOR", ShipRegion = "Crisana", ShipPostalCode = "653271", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "AROUT", EmployeeID = 6, OrderDate = new DateTime(1996, 11, 14), ShippedDate = new DateTime(1996, 12, 12), RequiredDate = new DateTime(1996, 11, 19), ShipVia = 1, Freight = (decimal?)41.95, ShipName = "Around the Horn", ShipAddress = "Brook Farm Stratford St. Mary", ShipCity = "Colchester", ShipRegion = "Essex", ShipPostalCode = "CO7 6JX", ShipCountry = "UK" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "AROUT", EmployeeID = 1, OrderDate = new DateTime(1996, 12, 08), ShippedDate = new DateTime(1997, 01, 05), RequiredDate = new DateTime(1996, 12, 12), ShipVia = 3, Freight = (decimal?)22.21, ShipName = "Beatties", ShipAddress = "STR. VULCAN SAMUIL nr. 16, BEIUS", ShipCity = "BIHOR", ShipRegion = "Crisana", ShipPostalCode = "653271", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "AROUT", EmployeeID = 1, OrderDate = new DateTime(1997, 02, 20), ShippedDate = new DateTime(1997, 03, 20), RequiredDate = new DateTime(1997, 02, 25), ShipVia = 2, Freight = (decimal?)25.36, ShipName = "Around the Horn", ShipAddress = "Brook Farm Stratford St. Mary", ShipCity = "Colchester", ShipRegion = "Essex", ShipPostalCode = "CO7 6JX", ShipCountry = "UK" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "AROUT", EmployeeID = 2, OrderDate = new DateTime(1997, 05, 28), ShippedDate = new DateTime(1997, 06, 25), RequiredDate = new DateTime(1997, 06, 04), ShipVia = 1, Freight = (decimal?)83.22, ShipName = "Beatties", ShipAddress = "STR. VULCAN SAMUIL nr. 16, BEIUS", ShipCity = "BIHOR", ShipRegion = "Crisana", ShipPostalCode = "653271", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "BERGS", EmployeeID = 1, OrderDate = new DateTime(1996, 07, 31), ShippedDate = new DateTime(1996, 08, 28), RequiredDate = new DateTime(1996, 08, 01), ShipVia = 1, Freight = (decimal?)136.54, ShipName = "Cut Above", ShipAddress = "Bulevardul Ion Mihalache 148B, Bucuresti", ShipCity = "Bucuresti", ShipRegion = "Muntenia", ShipPostalCode = "666708", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "BLAUS", EmployeeID = 1, OrderDate = new DateTime(1996, 12, 31), ShippedDate = new DateTime(1997, 01, 28), RequiredDate = new DateTime(1997, 01, 15), ShipVia = 3, Freight = (decimal?)83.93, ShipName = "Sears Homelife", ShipAddress = "STR. 9 MAI, BACAU", ShipCity = "Bacau", ShipRegion = "Moldova 546708", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "BLAUS", EmployeeID = 2, OrderDate = new DateTime(1997, 01, 06), ShippedDate = new DateTime(1997, 02, 03), RequiredDate = new DateTime(1997, 01, 29), ShipVia = 2, Freight = (decimal?)91.48, ShipName = "Sears Homelife", ShipAddress = "STR. 9 MAI, BACAU", ShipCity = "Bacau", ShipRegion = "Moldova 546708", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "BLAUS", EmployeeID = 4, OrderDate = new DateTime(1997, 04, 16), ShippedDate = new DateTime(1997, 05, 14), RequiredDate = new DateTime(1997, 04, 28), ShipVia = 1, Freight = (decimal?)0.15, ShipName = "Blauer See", ShipAddress = "Delikatessen Forsterstr. 57 ", ShipCity = "Mannheim", ShipPostalCode = "68306", ShipCountry = "Germany" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "BLONP", EmployeeID = 2, OrderDate = new DateTime(1996, 07, 24), ShippedDate = new DateTime(1996, 08, 21), RequiredDate = new DateTime(1996, 08, 11), ShipVia = 1, Freight = (decimal?)55.28, ShipName = "Blondel père et fils", ShipAddress = "STR. BARNUTIU SIMION nr. 67, SALAJ", ShipCity = "SALAJ", ShipRegion = "Transilvania", ShipPostalCode = "67000", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "BLONP", EmployeeID = 4, OrderDate = new DateTime(1996, 08, 22), ShippedDate = new DateTime(1996, 09, 19), RequiredDate = new DateTime(1996, 09, 02), ShipVia = 1, Freight = (decimal?)7.45, ShipName = "Century House", ShipAddress = "STR. BARNUTIU SIMION nr. 67, SALAJ", ShipCity = "SALAJ", ShipRegion = "Transilvania", ShipPostalCode = "67000", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "BLONP", EmployeeID = 4, OrderDate = new DateTime(1996, 11, 21), ShippedDate = new DateTime(1996, 12, 19), RequiredDate = new DateTime(1996, 12, 01), ShipVia = 3, Freight = (decimal?)131.70, ShipName = "Blondel père et fils", ShipAddress = "STR. BARNUTIU SIMION nr. 67, SALAJ", ShipCity = "SALAJ", ShipPostalCode = "67000", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "BLONP", EmployeeID = 1, OrderDate = new DateTime(1997, 01, 05), ShippedDate = new DateTime(1997, 02, 02), RequiredDate = new DateTime(1997, 01, 21), ShipVia = 1, Freight = (decimal?)34.82, ShipName = "Century House", ShipAddress = "STR. BARNUTIU SIMION nr. 67, SALAJ", ShipCity = "SALAJ", ShipRegion = "Transilvania", ShipPostalCode = "67000", ShipCountry = "Romania" });
            context.SaveChanges();
            context.Orders.Add(new Orders() { CustomerID = "BLONP", EmployeeID = 1, OrderDate = new DateTime(1997, 01, 05), ShippedDate = new DateTime(1997, 02, 02), RequiredDate = new DateTime(1997, 01, 21), ShipVia = 1, Freight = (decimal?)34.82, ShipName = "Services", ShipAddress = "STR. BARNUTIU SIMION nr. 67, SALAJ", ShipCity = "SALAJ", ShipRegion = "Transilvania", ShipPostalCode = "67000", ShipCountry = "Romania" });
            context.SaveChanges();

            //order details 
            List<Order_Details> orderDetails = new List<Order_Details>
            {
                new Order_Details(){OrderID = 1, ProductID = 11, UnitPrice = 14.00M, Quantity =12 , Discount =  0   },
                new Order_Details(){OrderID = 1, ProductID = 42, UnitPrice = 9.80M, Quantity =10 , Discount =  0   },
                new Order_Details(){OrderID = 1, ProductID = 72, UnitPrice = 34.80M, Quantity =5  , Discount =  0   },
                new Order_Details(){OrderID = 1, ProductID = 31, UnitPrice = 10.00M , Quantity =15 , Discount =  0.05f},
                new Order_Details(){OrderID = 1, ProductID = 33, UnitPrice = 2.00M  , Quantity =20 , Discount =  0   },
                new Order_Details(){OrderID = 1, ProductID = 40, UnitPrice = 14.70M , Quantity =60 , Discount =  0.05f},
                new Order_Details(){OrderID = 2, ProductID = 14, UnitPrice = 18.60M, Quantity =9  , Discount =  0   },
                new Order_Details(){OrderID = 2, ProductID = 51, UnitPrice = 42.40M, Quantity =40 , Discount =  0   },
                new Order_Details(){OrderID = 2, ProductID = 41, UnitPrice = 7.70M, Quantity =10 , Discount =  0   },
                new Order_Details(){OrderID = 3, ProductID = 51, UnitPrice = 42.40M, Quantity =35 , Discount =  0.15f},
                new Order_Details(){OrderID = 3, ProductID = 65, UnitPrice = 16.80M, Quantity =15 , Discount =  0.15f},
                new Order_Details(){OrderID = 3, ProductID = 22, UnitPrice = 16.80M, Quantity =6  , Discount =  0.05f},
                new Order_Details(){OrderID = 3, ProductID = 76, UnitPrice = 14.40M , Quantity =33 , Discount =  0.05f},
                new Order_Details(){OrderID = 3, ProductID = 71, UnitPrice = 17.20M , Quantity =20 , Discount =  0   },
                new Order_Details(){OrderID = 3, ProductID = 72, UnitPrice = 27.80M , Quantity =7  , Discount =  0   },
                new Order_Details(){OrderID = 4, ProductID = 57, UnitPrice = 15.60M, Quantity =15 , Discount =  0.05f},
                new Order_Details(){OrderID = 4, ProductID = 65, UnitPrice = 16.80M, Quantity =20 , Discount =  0   },
                new Order_Details(){OrderID = 4, ProductID = 20, UnitPrice = 64.80M, Quantity =40 , Discount =  0.05f},
                new Order_Details(){OrderID = 5, ProductID = 33, UnitPrice = 2.00M, Quantity =25 , Discount =  0.05f},
                new Order_Details(){OrderID = 5, ProductID = 60, UnitPrice = 27.20M, Quantity =40 , Discount =  0   },
                new Order_Details(){OrderID = 5, ProductID = 31, UnitPrice = 10.00M, Quantity =20 , Discount =  0   },
                new Order_Details(){OrderID = 6, ProductID = 39, UnitPrice = 14.40M, Quantity =42 , Discount =  0   },
                new Order_Details(){OrderID = 6, ProductID = 49, UnitPrice = 16.00M, Quantity =40 , Discount =  0   },
                new Order_Details(){OrderID = 6, ProductID = 24, UnitPrice = 3.60M, Quantity =15 , Discount =  0.15f},
                new Order_Details(){OrderID = 6, ProductID = 25, UnitPrice = 3.60M  , Quantity =12 , Discount =  0.05f},
                new Order_Details(){OrderID = 6, ProductID = 59, UnitPrice = 44.00M , Quantity =6  , Discount =  0.05f},
                new Order_Details(){OrderID = 6, ProductID = 10, UnitPrice = 24.80M , Quantity =15 , Discount =  0   },
                new Order_Details(){OrderID = 7, ProductID = 55, UnitPrice = 19.20M, Quantity =21 , Discount =  0.15f},
                new Order_Details(){OrderID = 7, ProductID = 74, UnitPrice = 8.00M, Quantity =21 , Discount =  0   },
                new Order_Details(){OrderID = 7, ProductID = 2 , UnitPrice = 15.20M, Quantity =20 , Discount =  0   },
                new Order_Details(){OrderID = 8, ProductID = 16, UnitPrice = 13.90M, Quantity =35 , Discount =  0   },
                new Order_Details(){OrderID = 8, ProductID = 36, UnitPrice = 15.20M, Quantity =25 , Discount =  0   },
                new Order_Details(){OrderID = 8, ProductID = 59, UnitPrice = 44.00M, Quantity =30 , Discount =  0   },
                new Order_Details(){OrderID = 8, ProductID = 13, UnitPrice = 4.80M  , Quantity =10 , Discount =  0   },
                new Order_Details(){OrderID = 8, ProductID = 28, UnitPrice = 36.40M , Quantity =20 , Discount =  0   },
                new Order_Details(){OrderID = 8, ProductID = 62, UnitPrice = 39.40M , Quantity =12 , Discount =  0   },
                new Order_Details(){OrderID = 8, ProductID = 44, UnitPrice = 15.50M , Quantity =16 , Discount =  0   },
                new Order_Details(){OrderID = 9, ProductID = 53, UnitPrice = 26.20M, Quantity =15 , Discount =  0   },
                new Order_Details(){OrderID = 9, ProductID = 77, UnitPrice = 10.40M, Quantity =12 , Discount =  0   },
                new Order_Details(){OrderID = 9, ProductID = 27, UnitPrice = 35.10M, Quantity =25 , Discount =  0   },
                new Order_Details(){OrderID = 10, ProductID = 39, UnitPrice = 14.40M, Quantity =6  , Discount =  0   },
                new Order_Details(){OrderID = 10, ProductID = 77, UnitPrice = 10.40M, Quantity =15 , Discount =  0   },
                new Order_Details(){OrderID = 10, ProductID = 2 , UnitPrice = 15.20M, Quantity =50 , Discount =  0.2f },
                new Order_Details(){OrderID = 11, ProductID = 5 , UnitPrice = 17.00M , Quantity =65 , Discount =  0.2f },
                new Order_Details(){OrderID = 11, ProductID = 32, UnitPrice = 25.60M , Quantity =6  , Discount =  0.2f },
                new Order_Details(){OrderID = 11, ProductID = 21, UnitPrice = 8.00M  , Quantity =10 , Discount =  0   },
                new Order_Details(){OrderID = 12, ProductID = 37, UnitPrice = 20.80M , Quantity =1  , Discount =  0   },
                new Order_Details(){OrderID = 12, ProductID = 41, UnitPrice = 7.70M  , Quantity =16 , Discount =  0.25f},
                new Order_Details(){OrderID = 12, ProductID = 57, UnitPrice = 15.60M , Quantity =50 , Discount =  0   },
                new Order_Details(){OrderID = 13, ProductID = 62, UnitPrice = 39.40M , Quantity =15 , Discount =  0.25f},
                new Order_Details(){OrderID = 13, ProductID = 70, UnitPrice = 12.00M , Quantity =21 , Discount =  0.25f},
                new Order_Details(){OrderID = 13, ProductID = 21, UnitPrice = 8.00M , Quantity =20 , Discount =  0   },
                new Order_Details(){OrderID = 14, ProductID = 35, UnitPrice = 14.40M , Quantity =20 , Discount =  0   },
                new Order_Details(){OrderID = 14, ProductID = 5 , UnitPrice = 17.00M , Quantity =12 , Discount =  0.2f },
                new Order_Details(){OrderID = 14, ProductID = 7 , UnitPrice = 24.00M , Quantity =15 , Discount =  0   },
                new Order_Details(){OrderID = 15, ProductID = 56, UnitPrice = 30.40M , Quantity =2  , Discount =  0   },
                new Order_Details(){OrderID = 15, ProductID = 16, UnitPrice = 13.90M , Quantity =60 , Discount =  0.25f},
                new Order_Details(){OrderID = 15, ProductID = 24, UnitPrice = 3.60M  , Quantity =28 , Discount =  0   },
                new Order_Details(){OrderID = 16, ProductID = 30, UnitPrice = 20.70M , Quantity =60 , Discount =  0.25f},
                new Order_Details(){OrderID = 16, ProductID = 74, UnitPrice = 8.00M  , Quantity =36 , Discount =  0.25f},
                new Order_Details(){OrderID = 16, ProductID = 2 , UnitPrice = 15.20M , Quantity =35 , Discount =  0   },
                new Order_Details(){OrderID = 16, ProductID = 24, UnitPrice = 3.60M  , Quantity =12 , Discount =  0   },
                new Order_Details(){OrderID = 16, ProductID = 55, UnitPrice = 19.20M , Quantity =20 , Discount =  0   },
                new Order_Details(){OrderID = 16, ProductID = 75, UnitPrice = 6.20M  , Quantity =30 , Discount =  0   },
                new Order_Details(){OrderID = 16, ProductID = 19, UnitPrice = 7.30M  , Quantity =1  , Discount =  0 },
                new Order_Details(){OrderID = 17, ProductID = 41, UnitPrice = 7.70M , Quantity =25 , Discount =  0.15f},
                new Order_Details(){OrderID = 17, ProductID = 17, UnitPrice = 31.20M , Quantity =30 , Discount =  0   },
                new Order_Details(){OrderID = 17, ProductID = 70, UnitPrice = 12.00M , Quantity =20 , Discount =  0   },
                new Order_Details(){OrderID = 18, ProductID = 12, UnitPrice = 30.40M , Quantity =12 , Discount =  0.05f},
                new Order_Details(){OrderID = 18, ProductID = 40, UnitPrice = 14.70M , Quantity =50 , Discount =  0   },
                new Order_Details(){OrderID = 18, ProductID = 59, UnitPrice = 44.00M , Quantity =70 , Discount =  0.15f},
                new Order_Details(){OrderID = 19, ProductID = 76, UnitPrice = 14.40M , Quantity =15 , Discount =  0.15f},
                new Order_Details(){OrderID = 19, ProductID = 29, UnitPrice = 99.00M , Quantity =10 , Discount =  0   },
                new Order_Details(){OrderID = 19, ProductID = 72, UnitPrice = 27.80M , Quantity =4  , Discount =  0   },
                new Order_Details(){OrderID = 20, ProductID = 33, UnitPrice = 2.00M  , Quantity =60 , Discount =  0.05f},
                new Order_Details(){OrderID = 20, ProductID = 72, UnitPrice = 27.80M , Quantity =20 , Discount =  0.05f},
                new Order_Details(){OrderID = 20, ProductID = 36, UnitPrice = 15.20M , Quantity =30 , Discount =  0   },
                new Order_Details(){OrderID = 20, ProductID = 59, UnitPrice = 44.00M , Quantity =15 , Discount =  0   },
                new Order_Details(){OrderID = 20, ProductID = 63, UnitPrice = 35.10M , Quantity =8  , Discount =  0   },
                new Order_Details(){OrderID = 20, ProductID = 73, UnitPrice = 12.00M , Quantity =25 , Discount =  0   },
                new Order_Details(){OrderID = 20, ProductID = 17, UnitPrice = 31.20M , Quantity =15 , Discount =  0.25f},
                new Order_Details(){OrderID = 21, ProductID = 43, UnitPrice = 36.80M , Quantity =25 , Discount =  0   },
                new Order_Details(){OrderID = 21, ProductID = 33, UnitPrice = 2.00M  , Quantity =24 , Discount =  0   },
                new Order_Details(){OrderID = 21, ProductID = 20, UnitPrice = 64.80M , Quantity =6  , Discount =  0   },
                new Order_Details(){OrderID = 22, ProductID = 31, UnitPrice = 10.00M , Quantity =40 , Discount =  0   },
                new Order_Details(){OrderID = 22, ProductID = 72, UnitPrice = 27.80M , Quantity =24 , Discount =  0   },
                new Order_Details(){OrderID = 22, ProductID = 10, UnitPrice = 24.80M , Quantity =24 , Discount =  0.05f},
                new Order_Details(){OrderID = 23, ProductID = 83, UnitPrice = 120.00M , Quantity =24 , Discount =  0.05f},
            };

            foreach (var orderDetail in orderDetails)
            {
                context.Order_Details.Add(orderDetail);
            }
            context.SaveChanges();

        }
    }
}