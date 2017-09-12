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
            /*
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
            regions[0].Territories.Add(new Territories() { TerritoryID = "1", TerritoryDescription = "Timis" , RegionID =1}); 
            regions[0].Territories.Add(new Territories() { TerritoryID = "2", TerritoryDescription = "Caras - Severin" , RegionID =1});
            regions[1].Territories.Add(new Territories() { TerritoryID = "3", TerritoryDescription = "Botosani" , RegionID =2});
            regions[1].Territories.Add(new Territories() { TerritoryID = "4", TerritoryDescription = "Suceava" , RegionID =2});
            regions[2].Territories.Add(new Territories() { TerritoryID = "5", TerritoryDescription = "Bihor" , RegionID =3});
            regions[2].Territories.Add(new Territories() { TerritoryID = "6", TerritoryDescription = "Arad" , RegionID =3});
            regions[3].Territories.Add(new Territories() { TerritoryID = "7", TerritoryDescription = "Tulcea" , RegionID =4});
            regions[3].Territories.Add(new Territories() { TerritoryID = "8", TerritoryDescription = "Constanta" , RegionID =4});
            regions[4].Territories.Add(new Territories() { TerritoryID = "9", TerritoryDescription = "Satu - Mare", RegionID = 5 });
            regions[4].Territories.Add(new Territories() { TerritoryID = "10", TerritoryDescription = "Maramures" , RegionID =5});
            regions[5].Territories.Add(new Territories() { TerritoryID = "11", TerritoryDescription = "Neamt" , RegionID =6});
            regions[5].Territories.Add(new Territories() { TerritoryID = "12", TerritoryDescription = "Iasi" , RegionID =6});
            regions[5].Territories.Add(new Territories() { TerritoryID = "13", TerritoryDescription = "Bacau" , RegionID =6});
            regions[5].Territories.Add(new Territories() { TerritoryID = "14", TerritoryDescription = "Vaslui" , RegionID =6});
            regions[5].Territories.Add(new Territories() { TerritoryID = "15", TerritoryDescription = "Vrancea" , RegionID =6});
            regions[5].Territories.Add(new Territories() { TerritoryID = "16", TerritoryDescription = "Galati" , RegionID =6});
            regions[6].Territories.Add(new Territories() { TerritoryID = "17", TerritoryDescription = "Braila" , RegionID =7});
            regions[6].Territories.Add(new Territories() { TerritoryID = "18", TerritoryDescription = "Buzau" , RegionID =7});
            regions[6].Territories.Add(new Territories() { TerritoryID = "19", TerritoryDescription = "Calarasi" , RegionID =7});
            regions[6].Territories.Add(new Territories() { TerritoryID = "20", TerritoryDescription = "Prahova" , RegionID =7});
            regions[6].Territories.Add(new Territories() { TerritoryID = "21", TerritoryDescription = "Dambovita" , RegionID =7});
            regions[6].Territories.Add(new Territories() { TerritoryID = "22", TerritoryDescription = "Arges" , RegionID =7});
            regions[6].Territories.Add(new Territories() { TerritoryID = "23", TerritoryDescription = "Ialomita" , RegionID =7});
            regions[6].Territories.Add(new Territories() { TerritoryID = "24", TerritoryDescription = "Calarasi" , RegionID =7});
            regions[6].Territories.Add(new Territories() { TerritoryID = "25", TerritoryDescription = "Ilfov" , RegionID =7});
            regions[6].Territories.Add(new Territories() { TerritoryID = "26", TerritoryDescription = "Bucuresti" , RegionID =7});
            regions[6].Territories.Add(new Territories() { TerritoryID = "27", TerritoryDescription = "Giurgiu" , RegionID =7});
            regions[6].Territories.Add(new Territories() { TerritoryID = "28", TerritoryDescription = "Teleorman" , RegionID =7});
            regions[7].Territories.Add(new Territories() { TerritoryID = "29", TerritoryDescription = "Gorj" , RegionID =8});
            regions[7].Territories.Add(new Territories() { TerritoryID = "30", TerritoryDescription = "Valcea" , RegionID =8});
            regions[7].Territories.Add(new Territories() { TerritoryID = "31", TerritoryDescription = "Olt" , RegionID =8});
            regions[7].Territories.Add(new Territories() { TerritoryID = "32", TerritoryDescription = "Dolj" , RegionID =8});
            regions[7].Territories.Add(new Territories() { TerritoryID = "33", TerritoryDescription = "Mehedinti" , RegionID =8});
            regions[8].Territories.Add(new Territories() { TerritoryID = "34", TerritoryDescription = "Salaj" , RegionID =9});
            regions[8].Territories.Add(new Territories() { TerritoryID = "35", TerritoryDescription = "Bistrita - Nasaud" , RegionID =9});
            regions[8].Territories.Add(new Territories() { TerritoryID = "36", TerritoryDescription = "Cluj" , RegionID =9});
            regions[8].Territories.Add(new Territories() { TerritoryID = "37", TerritoryDescription = "Mures" , RegionID =9});
            regions[8].Territories.Add(new Territories() { TerritoryID = "38", TerritoryDescription = "Harghita" , RegionID =9});
            regions[8].Territories.Add(new Territories() { TerritoryID = "39", TerritoryDescription = "Covasna" , RegionID =9});
            regions[8].Territories.Add(new Territories() { TerritoryID = "40", TerritoryDescription = "Brasov" , RegionID =9});
            regions[8].Territories.Add(new Territories() { TerritoryID = "41", TerritoryDescription = "Sibiu" , RegionID =9});
            regions[8].Territories.Add(new Territories() { TerritoryID = "42", TerritoryDescription = "Alba" , RegionID =9});
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
            employees[2].Territories = context.Territories.Where(x => x.TerritoryID == "11" || x.TerritoryID == "12" || x.TerritoryID == "3"  || x.TerritoryID == "35" || x.TerritoryID == "37" || x.TerritoryID == "38" || x.TerritoryID == "4").ToList();
            employees[3].Territories = context.Territories.Where(x => x.TerritoryID == "11" || x.TerritoryID == "12" || x.TerritoryID == "13" || x.TerritoryID == "14" || x.TerritoryID == "16").ToList();
            employees[4].Territories = context.Territories.Where(x => x.TerritoryID == "1"  || x.TerritoryID == "2"  || x.TerritoryID == "29" || x.TerritoryID == "33" || x.TerritoryID == "42" || x.TerritoryID == "43" || x.TerritoryID == "6").ToList();
            employees[5].Territories = context.Territories.Where(x => x.TerritoryID == "22" || x.TerritoryID == "28" || x.TerritoryID == "29" || x.TerritoryID == "30" || x.TerritoryID == "31" || x.TerritoryID == "32" || x.TerritoryID == "33").ToList();
            employees[6].Territories = context.Territories.Where(x => x.TerritoryID == "36" || x.TerritoryID == "37" || x.TerritoryID == "38" || x.TerritoryID == "39" || x.TerritoryID == "40" || x.TerritoryID == "41" || x.TerritoryID == "42").ToList();
            employees[7].Territories = context.Territories.Where(x => x.TerritoryID == "15" || x.TerritoryID == "16" || x.TerritoryID == "17" || x.TerritoryID == "18" || x.TerritoryID == "20" || x.TerritoryID == "21" || x.TerritoryID == "25" || x.TerritoryID == "26" || x.TerritoryID == "39" || x.TerritoryID == "40" || x.TerritoryID == "7").ToList();
            employees[8].Territories = context.Territories.Where(x => x.TerritoryID == "17" || x.TerritoryID == "18" || x.TerritoryID == "20" || x.TerritoryID == "21" || x.TerritoryID == "23" || x.TerritoryID == "24" || x.TerritoryID == "26" || x.TerritoryID == "27" || x.TerritoryID == "7" || x.TerritoryID == "8").ToList();

            foreach(var empolyee in employees)
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
            
            foreach(var shipper in shippers)
            {
                context.Shippers.Add(shipper);
            }
            context.SaveChanges();


            //customers
            List<Customers> customers = new List<Customers>
            {
                new Customers(){ CustomerID = "ALFKI", CompanyName = "Vinyl Fever",               ContactName = "Danut Gogean"        , ContactTitle = "Reprezentant Vanzari" , Address = "STR. 10 MAI nr. 15, DaMBOVITA"           , City = "Targoviste", Region = "Muntenia"    , PostalCode = "130062", Country = "Romania", Phone = "0245-216 446" },
                new Customers(){ CustomerID = "ANATR", CompanyName = "Kash n",                    ContactName = "Gabriella Anghelescu", ContactTitle = "Patron"               , Address = "Strada Caraiman 3, Constanta"            , City = "Constanta" , Region = "Dobrogea"    , PostalCode = "907021", Country = "Romania", Phone = "0723-564 218"  , Fax = "0251.411688" },
                new Customers(){ CustomerID = "ANTON", CompanyName = "Tech Hifi",                 ContactName = "Dorin Butacu"        , ContactTitle = "Patron"               , Address = "Piata Revolutiei 3/26, Maramures"        , City = "Maramures" , Region = "Maramures"   , PostalCode = "873309", Country = "Romania", Phone = "+40(262)260999", Fax = "+40(262)271338" },
                new Customers(){ CustomerID = "AROUT", CompanyName = "Beatties",                  ContactName = "Ioana Draghici"      , ContactTitle = "Reprezentant Vanzari" , Address = "STR. VULCAN SAMUIL nr. 16, BEIUS"        , City = "BIHOR"     , Region = "Crisana"     , PostalCode = "653271", Country = "Romania", Phone = "0259-320 222"  , Fax = "0251.418803" },
                new Customers(){ CustomerID = "BERGS", CompanyName = "Cut Above",                 ContactName = "Varujan Puscas"      , ContactTitle = "Administrator Comenzi", Address = "Bulevardul Ion Mihalache 148B, Bucuresti", City = "Bucuresti" , Region = "Muntenia"    , PostalCode = "666708", Country = "Romania", Phone = "+40(21)2246714", Fax = "0251.413102" },
                new Customers(){ CustomerID = "BLAUS", CompanyName = "Sears Homelife",            ContactName = "Stefan Manole"       , ContactTitle = "Reprezentant Vanzari" , Address = "STR. 9 MAI, BACAU"                       , City = "Bacau"     , Region = "Moldova"     , PostalCode = "546708", Country = "Romania", Phone = "0740-082 824"  , Fax = "0251.413102" },
                new Customers(){ CustomerID = "BLONP", CompanyName = "Century House",             ContactName = "Varujan Teodorescu"  , ContactTitle = "Director Marketing"   , Address = "STR. BARNUTIU SIMION nr. 67, SALAJ"      , City = "SALAJ"     , Region = "Transilvania", PostalCode = "437945", Country = "Romania", Phone = "0260-616 920"  , Fax = "0251.418803" },
                new Customers(){ CustomerID = "BOLID", CompanyName = "Matrix Interior Design",    ContactName = "Diona Lascar"        , ContactTitle = "Patron"               , Address = "NR. 91/A, COM. BOBOTA"                   , City = "SALAJ"     , Region = "Transilvania", PostalCode = "626705", Country = "Romania", Phone = "0260-652 491"  , Fax = "0251.418803" },
                new Customers(){ CustomerID = "BONAP", CompanyName = "Awthentikz",                ContactName = "Lavinia Ciora"       , ContactTitle = "Patron"               , Address = "STR. GIMNASTICII nr. 11, SIBIU"          , City = "SIBIU"     , Region = "Transilvania", PostalCode = "907892", Country = "Romania", Phone = "0269-245 479"  , Fax = "0251.413102" },
                new Customers(){ CustomerID = "BOTTM", CompanyName = "Afforda Merchant Services", ContactName = "Amelia Raducanu"     , ContactTitle = "Manager Contabilitate", Address = "STR. BERZEI nr. 21, Bucuresti - Sector 1", City = "Bucuresti" , Region = "Muntenia"    , PostalCode = "749447", Country = "Romania", Phone = "0741-108 981"  , Fax = "0251.413102" }
            };

            //todo sterge order id
            customers[0].Orders = new List<Orders> {
                new Orders() { OrderID = 65 , CustomerID = "ALFKI", EmployeeID = 2, OrderDate = new DateTime(1996, 09, 22), ShippedDate = new DateTime(1996, 10, 20), RequiredDate = new DateTime(1996, 10, 02), ShipVia = 2, Freight = (decimal?)40.26, ShipName = "Vinyl Fever", ShipAddress = "STR. 10 MAI nr. 15, DaMBOVITA", ShipCity = "Targoviste", ShipRegion = "Muntenia", ShipPostalCode = "453993", ShipCountry = "Romania" },
                new Orders() { OrderID = 110, CustomerID = "ALFKI", EmployeeID = 1, OrderDate = new DateTime(1996, 11, 18), ShippedDate = new DateTime(1996, 12, 16), RequiredDate = new DateTime(1996, 12, 01), ShipVia = 3, Freight = (decimal?)34.88, ShipName = "Vinyl Fever", ShipAddress = "STR. 10 MAI nr. 15, DaMBOVITA", ShipCity = "Targoviste", ShipRegion = "Muntenia", ShipPostalCode = "453993", ShipCountry = "Romania" },
                new Orders() { OrderID = 127, CustomerID = "ALFKI", EmployeeID = 1, OrderDate = new DateTime(1996, 12, 04), ShippedDate = new DateTime(1997, 01, 01), RequiredDate = new DateTime(1996, 12, 08), ShipVia = 3, Freight = (decimal?)3.94 , ShipName = "Vinyl Fever", ShipAddress = "STR. 10 MAI nr. 15, DaMBOVITA", ShipCity = "Targoviste", ShipRegion = "Muntenia", ShipPostalCode = "453993", ShipCountry = "Romania" }
            };
            customers[1].Orders = new List<Orders>
            {
                new Orders() { OrderID = 32  , CustomerID = "ANATR", EmployeeID = 8, OrderDate = new DateTime(1996, 08, 12), ShippedDate = new DateTime(1996-09-09) 1996-08-15 21:00:00.000 2   25.83   Kash n  Strada Caraiman 3, Constanta    Constanta   Dobrogea    907021  Romania
                new Orders() { OrderID = 193 , CustomerID = "ANATR", EmployeeID = 4, OrderDate = new DateTime(1997, 02, 09), ShippedDate = new DateTime(1997-03-09) 1997-02-27 22:00:00.000 2   86.53   Kash n  Strada Caraiman 3, Constanta    Constanta   Dobrogea    907021  Romania
                new Orders() { OrderID = 247 , CustomerID = "ANATR", EmployeeID = 4, OrderDate = new DateTime(1997, 04, 01), ShippedDate = new DateTime(1997-04-29) 1997-04-08 21:00:00.000 2   65.99   Kash n  Strada Caraiman 3, Constanta    Constanta   Dobrogea    907021  Romania
                new Orders() { OrderID = 378 , CustomerID = "ANATR", EmployeeID = 3, OrderDate = new DateTime(1997, 08, 07), ShippedDate = new DateTime(1997-09-04) 1997-08-13 21:00:00.000 1   43.90   Ana Trujillo Emparedados y helados  Avda. de la Constitución 2222   México D.F. NULL    05021   Mexico
            };
            customers[2].Orders = new List<Orders>
            {
                new Orders() { OrderID = 24  , CustomerID = "ANTON", EmployeeID = 6, OrderDate = new DateTime(1996, 07, 31), ShippedDate = new DateTime(1996-08-28) 1996-08-29 21:00:00.000 2   4.54    Tech Hifi   Piata Revolutiei 3/26, Maramures    Maramures   Maramures   873309  Romania
                new Orders() { OrderID = 78  , CustomerID = "ANTON", EmployeeID = 1, OrderDate = new DateTime(1996, 10, 08), ShippedDate = new DateTime(1996-10-22) 1996-10-13 21:00:00.000 3   64.86   Tech Hifi   Piata Revolutiei 3/26, Maramures    Maramures   Maramures   873309  Romania
            };
            customers[3].Orders = new List<Orders>
            {
                new Orders() { OrderID = 72  , CustomerID = "AROUT", EmployeeID = 7, OrderDate = new DateTime(1996, 10, 01), ShippedDate = new DateTime(1996-10-29) 1996-10-10 21:00:00.000 3   64.50   Beatties    STR. VULCAN SAMUIL nr. 16, BEIUS    BIHOR   Crisana 653271  Romania
                new Orders() { OrderID = 108 , CustomerID = "AROUT", EmployeeID = 6, OrderDate = new DateTime(1996, 11, 14), ShippedDate = new DateTime(1996-12-12) 1996-11-19 22:00:00.000 1   41.95   Around the Horn Brook Farm Stratford St. Mary   Colchester  Essex   CO7 6JX UK
                new Orders() { OrderID = 130 , CustomerID = "AROUT", EmployeeID = 1, OrderDate = new DateTime(1996, 12, 08), ShippedDate = new DateTime(1997-01-05) 1996-12-12 22:00:00.000 3   22.21   Beatties    STR. VULCAN SAMUIL nr. 16, BEIUS    BIHOR   Crisana 653271  Romania
                new Orders() { OrderID = 206 , CustomerID = "AROUT", EmployeeID = 1, OrderDate = new DateTime(1997, 02, 20), ShippedDate = new DateTime(1997-03-20) 1997-02-25 22:00:00.000 2   25.36   Around the Horn Brook Farm Stratford St. Mary   Colchester  Essex   CO7 6JX UK
                new Orders() { OrderID = 305 , CustomerID = "AROUT", EmployeeID = 2, OrderDate = new DateTime(1997, 05, 28), ShippedDate = new DateTime(1997-06-25) 1997-06-04 21:00:00.000 1   83.22   Beatties    STR. VULCAN SAMUIL nr. 16, BEIUS    BIHOR   Crisana 653271  Romania
            };
            customers[4].Orders = new List<Orders>
            {
                new Orders() { OrderID = 23 , CustomerID = "BERGS", EmployeeID = 1, OrderDate = new DateTime(1996, 07, 31), ShippedDate = new DateTime(1996-08-28) 1996-08-01 21:00:00.000 1   136.54  Cut Above   Bulevardul Ion Mihalache 148B, Bucuresti    Bucuresti   Muntenia    666708  Romania
            };
            customers[5].Orders = new List<Orders>
            {
                new Orders() { OrderID = 153 , CustomerID = "BLAUS", EmployeeID = 1, OrderDate = new DateTime(1996, 12, 31), ShippedDate = new DateTime(1997-01-28) 1997-01-15 22:00:00.000 3   83.93   Sears Homelife  STR. 9 MAI, BACAU   Bacau   Moldova 546708  Romania
                new Orders() { OrderID = 160 , CustomerID = "BLAUS", EmployeeID = 2, OrderDate = new DateTime(1997, 01, 06), ShippedDate = new DateTime(1997-02-03) 1997-01-29 22:00:00.000 2   91.48   Sears Homelife  STR. 9 MAI, BACAU   Bacau   Moldova 546708  Romania
                new Orders() { OrderID = 262 , CustomerID = "BLAUS", EmployeeID = 4, OrderDate = new DateTime(1997, 04, 16), ShippedDate = new DateTime(1997-05-14) 1997-04-28 21:00:00.000 1   0.15    Blauer See Delikatessen Forsterstr. 57  Mannheim    NULL    68306   Germany
            };
            customers[6].Orders = new List<Orders>
            {
                new Orders() { OrderID = 18  , CustomerID = "BLONP", EmployeeID = 2, OrderDate = new DateTime(1996, 07, 24), ShippedDate = new DateTime(1996-08-21) 1996-08-11 21:00:00.000 1   55.28   Blondel père et fils    24, place Kléber    Strasbourg  NULL    67000   France
                new Orders() { OrderID = 41  , CustomerID = "BLONP", EmployeeID = 4, OrderDate = new DateTime(1996, 08, 22), ShippedDate = new DateTime(1996-09-19) 1996-09-02 21:00:00.000 1   7.45    Century House   STR. BARNUTIU SIMION nr. 67, SALAJ  SALAJ   Transilvania    437945  Romania
                new Orders() { OrderID = 113 , CustomerID = "BLONP", EmployeeID = 4, OrderDate = new DateTime(1996, 11, 21), ShippedDate = new DateTime(1996-12-19) 1996-12-01 22:00:00.000 3   131.70  Blondel père et fils    24, place Kléber    Strasbourg  NULL    67000   France
                new Orders() { OrderID = 158 , CustomerID = "BLONP", EmployeeID = 1, OrderDate = new DateTime(1997, 01, 05), ShippedDate = new DateTime(1997-02-02) 1997-01-21 22:00:00.000 1   34.82   Century House   STR. BARNUTIU SIMION nr. 67, SALAJ  SALAJ   Transilvania    437945  Romania
            };







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

            context.SaveChanges();

            //chage state of orders
            var orders = JsonConvert.DeserializeObject<List<Orders>>(File.ReadAllText(path + "\\orders.json"));
            foreach (var order in orders)
            {
                context.Entry(order).State = EntityState.Added;
                //save change to keep the orders id the same
                context.SaveChanges();
            }

        */
        }
    }
}