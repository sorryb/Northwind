using NorthwindWeb.Models.ServerClientCommunication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using System.Threading.Tasks;
using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Linq;
using System;
using NorthwindWeb.Context;

namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class RegionsControllerTest
    {
        //Arrange
        RegionsController _regionsControllerTest = new RegionsController();
        NorthwindDatabase db = new NorthwindDatabase();

        /// <summary>
        /// Sample test method.
        /// </summary>
        [TestMethod]
        public void RegionSampleTest()
        {
            //Arrage

            //Act

            //Assert
            Assert.AreEqual("RegionsController", "RegionsController");
        }

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void RegionReturnsIndexView()
        {
            //Arrage

            //Act
            var result = _regionsControllerTest.Index("");


            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void RegionReturnsIndexViewResult()
        {
            //Arrage

            //Act
            var result = _regionsControllerTest.Index("");

            //Assert
            Assert.IsNotNull(result);


        }


        /// <summary>
        /// Check Details items from Index action .
        /// </summary>
        [TestMethod]
        public async Task RegionReturnsDetails()
        {
            //Arrage
            Region regionTest = new Region() { RegionID = 100, RegionDescription = "Acasa" };
            //Act
            var result = await _regionsControllerTest.Details(regionTest.RegionID) ;
            

            //Assert
            Assert.IsNotNull(result);

            var region = db.Regions.Where(r => r.RegionDescription == regionTest.RegionDescription);
            db.Regions.RemoveRange(region);
            db.SaveChanges();
        }



        /// <summary>
        /// Tests if create returns view.
        /// </summary>
        [TestMethod]
        public void RegionsCreateReturnsView()
        {
            //Arrange

            //Act
            var result = _regionsControllerTest.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests if create inserts into database.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task RegionCreate()
        {
            //Arrange
            Region regionTest = new Region() {RegionID=70, RegionDescription = "Acasa" };
            //Act
            var expected = db.Regions.Count() + 1;
            await _regionsControllerTest.Create(regionTest);
            var actual = db.Regions.Count();
            var region = db.Regions.Where(r => r.RegionDescription == regionTest.RegionDescription );

            //Assert
            Assert.AreEqual(expected, actual);
            var regions = db.Regions.Where(r => r.RegionDescription.Contains("Acasa"));
            db.Regions.RemoveRange(region);
            db.SaveChanges();

        }



        /// <summary>
        /// Tests if delete returns view
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task RegionsDeleteReturnsView()
        {
            //Arrange
            Region regionTest = new Region() { RegionID = 60, RegionDescription = "Acasa" };
            await _regionsControllerTest.Create(regionTest);

            //Act
            var result = _regionsControllerTest.Delete(regionTest.RegionID);

            //Assert
            Assert.IsNotNull(result);
            
            var region = db.Regions.Where(r => r.RegionDescription == regionTest.RegionDescription);
            db.Regions.RemoveRange(region);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if delete deletes
        /// </summary>
        [TestMethod]
        public async Task RegionDeleteDeletes()
        {
            //Arrange
            Region regionTest = new Region() { RegionID = 100, RegionDescription = "Acasa" };
            await _regionsControllerTest.Create(regionTest);
            int expected = db.Regions.Count() - 1;

            //Act
            await _regionsControllerTest.DeleteConfirmed(regionTest.RegionID);
            int actual = db.Regions.Count();

            //Assert
            Assert.AreEqual(expected, actual);
            
            var region = db.Regions.Where(r => r.RegionDescription == regionTest.RegionDescription);
            db.Regions.RemoveRange(region);
            db.SaveChanges();
        }

       
        /// <summary>
        /// Tests if edit works
        /// </summary>
        [TestMethod]
        public async System.Threading.Tasks.Task RegionEditEdits()
        {
            //Arrange
            Region regionTest = new Region() { RegionID = 100, RegionDescription = "Aici" };
            await _regionsControllerTest.Create(regionTest);
            db.Entry(regionTest).State = System.Data.Entity.EntityState.Added;

            var expectedRegion = db.Regions.Find(regionTest.RegionID);

            db.Dispose();
            regionTest.RegionDescription = "Acolo";
            db = new NorthwindDatabase();

            //Act
            await _regionsControllerTest.Edit(regionTest);
            db.Entry(regionTest).State = System.Data.Entity.EntityState.Modified;
            var actualRegion = db.Regions.Find(regionTest.RegionID);

            //Assert
            Assert.AreEqual(expectedRegion, actualRegion);


            var region = db.Regions.Where(r => (r.RegionDescription == "Aici") || (r.RegionDescription == "Acolo"));
            db.Regions.RemoveRange(region);
            db.SaveChanges();
        }


        /// <summary>
        /// Unit test for json response to fill dinamic datatable
        /// </summary>
        [TestMethod]
        public void RegionJsonTableFill()
        {
            //Arrange
            var controller = new RegionsController();
            var db = new NorthwindDatabase();
            var regionCount = db.Regions.Count();
            

            //Act
            var jsonData = controller.JsonTableFill("").Data as IQueryable<RegionData>;

            //Assert
            Assert.AreEqual(db.Regions.Count(), jsonData.Count());

            db.Dispose();
        }

    }
}
