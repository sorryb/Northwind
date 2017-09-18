using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorthwindWeb.Models;

namespace NorthwindWeb.Context
{
    /// <summary>
    /// This class contains functions that fill Region and Territories with Romanian data
    /// </summary>
    public static class NorthwindReferencedTableInitializer
    {
        /// <summary>
        /// Insert referenced data with new NorthwindDatabase() context
        /// </summary>
        public static void InsertNorthwindReferencedData()
        {
            var db = new NorthwindDatabase();
            InsertNorthwindReferencedData(db);
            db.Dispose();
        }

        /// <summary>
        /// Insert referenced data with given context
        /// </summary>
        /// <param name="context">Local Northwind Context</param>
        public static void InsertNorthwindReferencedData(NorthwindDatabase context)
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
        }
    }
}