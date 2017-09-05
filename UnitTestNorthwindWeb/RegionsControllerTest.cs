using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Linq;

namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class RegionsControllerTest
    {
        //Arrange
        RegionsController _RegionsControllerUnderTest = new RegionsController();
        NorthwindModel db = new NorthwindModel();

        /// <summary>
        /// Sample test method.
        /// </summary>
        [TestMethod]
        public void SampleTestRegion()
        {
            //Arrage

            //Act

            //Assert
            Assert.AreEqual("RegionsController", "Regionsontroller");
        }


        /// <summary>
        /// Tests if create returns view.
        /// </summary>
        [TestMethod]
        public void CreateReturnsViewRegions()
        {
            //Arrange

            //Act
            var result = _RegionsControllerUnderTest.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests if create inserts into database.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task CreateRegion()
        {
            //Arrange
            Region regionTest = new Region() { RegionID = 4, RegionDescription = "Acasa" };
            //Act
            var expected = db.Regions.Count() + 1;
            await _RegionsControllerUnderTest.Create(regionTest);
            var actual = db.Regions.Count();
            var region = db.Regions.Where(c => c.RegionDescription == regionTest.RegionDescription );

            //Assert
            Assert.AreEqual(expected, actual);

            db.Regions.RemoveRange(region);
            db.SaveChanges();

        }



        /// <summary>
        /// Tests if delete returns view
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task DeleteReturnsViewRegionsAsync()
        {
            //Arrange
            Region regionTest = new Region() { RegionDescription = "Acasa" };
            await _RegionsControllerUnderTest.Create(regionTest);

            //Act
            var result = _RegionsControllerUnderTest.Delete(regionTest.RegionID);

            //Assert
            Assert.IsNotNull(result);





            var region = db.Regions.Where(c => c.RegionDescription == regionTest.RegionDescription);
            db.Regions.RemoveRange(region);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if delete deletes
        /// </summary>
        [TestMethod]
        public async System.Threading.Tasks.Task DeleteDeletesRegionAsync()
        {
            //Arrange
            Region regionTest = new Region() { RegionDescription = "Acasa" };
            await _RegionsControllerUnderTest.Create(regionTest);
            int expected = db.Shippers.Count() - 1;

            //Act
            await _RegionsControllerUnderTest.DeleteConfirmed(regionTest.RegionID);
            int actual = db.Shippers.Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests if edit works
        /// </summary>
        [TestMethod]
        public async System.Threading.Tasks.Task EditEditsShipperAsync()
        {
            //Arrange
            Region regionTest = new Region() { RegionDescription = "Aici" };
            await _RegionsControllerUnderTest.Create(regionTest);
            db.Entry(regionTest).State = System.Data.Entity.EntityState.Added;

            var expectedRegion = db.Regions.Find(regionTest.RegionID);

            db.Dispose();
            regionTest.RegionDescription = "Acolo";
            db = new NorthwindModel();

            //Act
            await _RegionsControllerUnderTest.Edit(regionTest);
            db.Entry(regionTest).State = System.Data.Entity.EntityState.Modified;
            var actualRegion = db.Regions.Find(regionTest.RegionID);

            //Assert
            Assert.AreEqual(expectedRegion, actualRegion);


            var region = db.Regions.Where(c => (c.RegionDescription == "Aici") || (c.RegionDescription == "Acolo"));
            db.Regions.RemoveRange(region);
            db.SaveChanges();
        }
    }
}
