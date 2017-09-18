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
    public class ShipperControllerTest
    {
        //Arrange
        ShippersController _shippersControllerTest = new ShippersController();
        NorthwindDatabase db = new NorthwindDatabase();

        /// <summary>
        /// Sample test method.
        /// </summary>
        [TestMethod]
        public void SampleTestShipper()
        {
            //Arrage

            //Act

            //Assert
            Assert.AreEqual("ShippersController", "ShippersController");
        }


        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void ShipperReturnsIndexView()
        {
            //Arrage

            //Act
            var result = _shippersControllerTest.Index("");

            //Assert

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void ShipperReturnsIndexViewResult()
        {
            //Arrage

            //Act
            var result = _shippersControllerTest.Index("");

            //Assert

            Assert.IsNotNull(result);


        }


        /// <summary>
        /// Check Details items from Index action .
        /// </summary>
        [TestMethod]
        public async Task ShipperReturnsDetails()
        {
            //Arrage
            Shippers shipperTest = new Shippers() { CompanyName = "FAN Courier" };
            //Act
            var result = await _shippersControllerTest.Details(shipperTest.ShipperID);


            //Assert
            Assert.IsNotNull(result);
        }


        /// <summary>
        /// Tests if create returns view.
        /// </summary>
        [TestMethod]
        public void ShipperCreateReturnsView()
        {
            //Arrange

            //Act
            var result = _shippersControllerTest.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests if create inserts into database.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ShipperCreate()
        {
            //Arrange
            Shippers shipperTest = new Shippers() { ShipperID = 4, CompanyName = "Nero", Phone = "0240-555-555" };
            //Act
            var expected = db.Shippers.Count() + 1;
            await _shippersControllerTest.Create(shipperTest);
            var actual = db.Shippers.Count();
            var shipper = db.Shippers.Where(s => s.CompanyName == shipperTest.CompanyName && s.Phone == shipperTest.Phone);

            //Assert
            Assert.AreEqual(expected, actual);

            db.Shippers.RemoveRange(shipper);
            db.SaveChanges();

        }



        /// <summary>
        /// Tests if delete returns view
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ShipperDeleteReturnsView()
        {
            //Arrange
            Shippers shipperTest = new Shippers() { CompanyName = "Nero", Phone = "0240-555-555" };
            await _shippersControllerTest.Create(shipperTest);

            //Act
            var result = _shippersControllerTest.Delete(shipperTest.ShipperID);

            //Assert
            Assert.IsNotNull(result);





            var category = db.Shippers.Where(s => s.CompanyName == shipperTest.CompanyName && s.Phone == shipperTest.Phone);
            db.Shippers.RemoveRange(category);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if delete deletes
        /// </summary>
        [TestMethod]
        public async Task ShipperDeleteDeletes()
        {
            //Arrange
            Shippers shipperTest = new Shippers() { CompanyName = "Nero", Phone = "0240-555-555" };
            await _shippersControllerTest.Create(shipperTest);
            int expected = db.Shippers.Count() - 1;

            //Act
            await _shippersControllerTest.DeleteConfirmed(shipperTest.ShipperID);
            int actual = db.Shippers.Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        /// Tests if edit works
        /// </summary>
        [TestMethod]
        public async Task ShipperEditEdits()
        {
            //Arrange
            Shippers shipperTest = new Shippers() { CompanyName = "Express", Phone = "0240-111-111" };
            await _shippersControllerTest.Create(shipperTest);
            db.Entry(shipperTest).State = System.Data.Entity.EntityState.Added;

            var expectedShipper = db.Shippers.Find(shipperTest.ShipperID);

            db.Dispose();
            shipperTest.CompanyName = "Nero Express";
            shipperTest.Phone = "0240-222-222";
            db = new NorthwindDatabase();

            //Act
            await _shippersControllerTest.Edit(shipperTest);
            db.Entry(shipperTest).State = System.Data.Entity.EntityState.Modified;
            var actualShipper = db.Shippers.Find(shipperTest.ShipperID);

            //Assert
            Assert.AreEqual(expectedShipper, actualShipper);


            var shipper = db.Shippers.Where(s => (s.CompanyName == "Express" && s.Phone == "0240-111-111") || (s.CompanyName == "Nero Express" && s.Phone == "0240-222-222"));
            db.Shippers.RemoveRange(shipper);
            db.SaveChanges();
        }

        /// <summary>
        /// Unit test for json response to fill dinamic datatable
        /// </summary>
        [TestMethod]
        public void ShipperJsonTableFill()
        {
            //Arrange
            var controller = new ShippersController();
            var db = new NorthwindDatabase();
            var shipperCount = db.Shippers.Count();


            //Act
            var jsonData = controller.JsonTableFill().Data as IQueryable<ShipperData>;

            //Assert
            Assert.AreEqual(db.Shippers.Count(), jsonData.Count());

            db.Dispose();

        }
    }
}
