using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Linq;
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
        public void ReturnsIndexView()
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
        public void ReturnsIndexViewResult()
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
        public void ReturnsViewBag()
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
        public async Task ReturnsDetails()
        {
            //Arrage

            //Act
            var result = await _customersControllerUnderTest.Details("ALFKI") as ViewResult;
            var model = result.Model;

            //Assert
            Assert.IsNotNull(model);
        }

        /// <summary>
        /// Tests if create returns view.
        /// </summary>
        [TestMethod]
        public void ReturnsCreate()
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
        public async Task ReturnsCreateCreates()
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
        public async Task ReturnsEdit()
        {
            //Arrage

            //Act
            var result = await _customersControllerUnderTest.Edit("ALFKI") as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests if edit make changes into database.
        /// </summary>
        [TestMethod]
        public async Task ReturnsEditEdits()
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


            var customers = db.Customers.Where(c => (c.CustomerID == "BBBBB" && c.CompanyName == "test") || (c.CustomerID == "BBBBB" && c.CompanyName == "test2"));
            db.Customers.RemoveRange(customers);
            db.SaveChanges();

        }

        /// <summary>
        /// Tests if delete returns view
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ReturnsDelete()
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
        public async Task ReturnsDeleteDeletes()
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
    }
    
}
