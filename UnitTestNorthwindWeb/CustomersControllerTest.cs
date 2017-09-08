using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Linq;
using NorthwindWeb.Models.ServerClientCommunication;

namespace UnitTestNorthwindWeb
{
    /// <summary>
    /// Test Customers controller.
    /// </summary>
    [TestClass]
    public class CustomersControllerTest
    {
       
            //Arrange
            CustomersController _customersControllerUnderTest = new CustomersController();
            NorthwindModel db = new NorthwindModel();

            /// <summary>
            /// Check what Index action returns.
            /// </summary>
        [TestMethod]
        public void CustomersReturnsIndexView()
        {
            //Arrage

            //Act
            var result = _customersControllerUnderTest.Index("",1) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void CustomersReturnsIndexViewResult()
        {
            //Arrage

            //Act
            var result = _customersControllerUnderTest.Index("",1) as ViewResult;

            //Assert

            Assert.IsInstanceOfType(result, typeof(ViewResult));


        }

        /// <summary>
        /// Check what Index viewbag returns.
        /// </summary>
        [TestMethod]
        public void CustomersReturnsViewBag()
        {

            //Arrage

            //Act
            var result = _customersControllerUnderTest.Index("",1) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Check Details items from Index action .
        /// </summary>
        [TestMethod]
        public async Task CustomersReturnsDetails()
        {
            //Arrage
            Customers customerTest = new Customers() { CustomerID = "BBBBB", CompanyName = "test" };
            await _customersControllerUnderTest.Create(customerTest);

            //Act
            var result = await _customersControllerUnderTest.Details(customerTest.CustomerID) as ViewResult;

            //Assert
            Assert.IsNotNull(result);

            var customers = db.Customers.Where(c => c.CustomerID == customerTest.CustomerID && c.CompanyName == customerTest.CompanyName);
            db.Customers.RemoveRange(customers);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if create returns view.
        /// </summary>
        [TestMethod]
        public void CustomersReturnsCreate()
        {

            //Arrage

            //Act
            var result = _customersControllerUnderTest.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests if create inserts into database.
        /// </summary>
        [TestMethod]
        public async Task CustomersReturnsCreateCreates()
        {
            //Arrange
            Customers customerTest = new Customers() { CustomerID="AABBC",CompanyName="test" };
            //Act
            var expected = db.Customers.Count() + 1;
            await _customersControllerUnderTest.Create(customerTest);
            var actual = db.Customers.Count();
            var customers = db.Customers.Where(c => c.CustomerID == customerTest.CustomerID && c.CompanyName == customerTest.CompanyName);
            //Assert
            Assert.AreEqual(expected, actual);



            db.Customers.RemoveRange(customers);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if edit returns view.
        /// </summary>
        [TestMethod]
        public async Task CustomersReturnsEdit()
        {
            //Arrage
            Customers customerTest = new Customers() { CustomerID = "BBBBB", CompanyName = "test" };
            await _customersControllerUnderTest.Create(customerTest);

            //Act
            var result = await _customersControllerUnderTest.Edit(customerTest.CustomerID) as ViewResult;

            //Assert
            Assert.IsNotNull(result);

            var customers = db.Customers.Where(c => c.CustomerID == customerTest.CustomerID && c.CompanyName == customerTest.CompanyName);
            db.Customers.RemoveRange(customers);
            db.SaveChanges();

        }

        /// <summary>
        /// Tests if edit make changes into database.
        /// </summary>
        [TestMethod]
        public async Task CustomersReturnsEditEdits()
        {
            //Arrange
            Customers customerTest = new Customers() { CustomerID = "BBBBB", CompanyName = "test" };
            await _customersControllerUnderTest.Create(customerTest);
            db.Entry(customerTest).State = System.Data.Entity.EntityState.Added;

            var expectedCustomer = db.Customers.Find(customerTest.CustomerID);

            db.Dispose();
            customerTest.CompanyName="test2";
            db = new NorthwindModel();

            //Act
            await _customersControllerUnderTest.Edit(customerTest);
            db.Entry(customerTest).State = System.Data.Entity.EntityState.Modified;
            var actualCustomer = db.Customers.Find(customerTest.CustomerID);

            //Assert
            Assert.AreEqual(expectedCustomer, actualCustomer);


            var customers = db.Customers.Where(c => c.CustomerID == customerTest.CustomerID && c.CompanyName == customerTest.CompanyName);
            db.Customers.RemoveRange(customers);
            db.SaveChanges();

        }

        /// <summary>
        /// Tests if delete returns view
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CustomersReturnsDelete()
        {
            //Arrange
            Customers customerTest = new Customers() { CustomerID = "BBBBB", CompanyName = "test" };
            await _customersControllerUnderTest.Create(customerTest);

            //Act
            var result = _customersControllerUnderTest.Delete(customerTest.CustomerID);

            //Assert
            Assert.IsNotNull(result);


            var customers = db.Customers.Where(c => c.CustomerID == customerTest.CustomerID && c.CompanyName == customerTest.CompanyName);
            db.Customers.RemoveRange(customers);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if delete deletes
        /// </summary>
        [TestMethod]
        public async Task CustomersReturnsDeleteDeletes()
        {
            //Arrange
            Customers customerTest = new Customers() { CustomerID = "BBBBB", CompanyName = "test" };
            await _customersControllerUnderTest.Create(customerTest);
            int expected = db.Customers.Count() - 1;

            //Act
            await _customersControllerUnderTest.DeleteConfirmed(customerTest.CustomerID);
            int actual = db.Customers.Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Unit test for json response to fill dinamic datatable
        /// </summary>
        [TestMethod]
        public void CustomersJsonTableFill()
        {
            //Arrange
            var controller = new CustomersController();
            var db = new NorthwindModel();
            var customersCount = db.Customers.Count();
            int draw = 1;
            int row = 20;

            //Act
            var jsonData = controller.JsonTableFill(draw, 0, row).Data as JsonDataTableObject;

            //Assert
            Assert.AreEqual(jsonData.draw, draw);
            Assert.AreEqual(jsonData.recordsTotal, customersCount);
            Assert.IsTrue(jsonData.recordsFiltered <= customersCount);
            db.Dispose();
        }
    }
    
}
