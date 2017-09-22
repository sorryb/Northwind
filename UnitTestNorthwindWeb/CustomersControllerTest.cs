using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Linq;
using NorthwindWeb.Models.ServerClientCommunication;
using NorthwindWeb.Context;
using NorthwindWeb.ViewModels;

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
            NorthwindDatabase db = new NorthwindDatabase();

            /// <summary>
            /// Check what Index action returns.
            /// </summary>
        [TestMethod]
        public void CustomersReturnsIndexView()
        {
            //Arrage

            //Act
            var result = _customersControllerUnderTest.Index() as ViewResult;

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
            var result = _customersControllerUnderTest.Index() as ViewResult;

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
            var result = _customersControllerUnderTest.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        private static Random random = new Random();
        private string CustomerId()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string result = new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            if (db.Customers.Find(result) != null)
                result = CustomerId();
            return result;
        }

        /// <summary>
        /// Check Details items from Index action .
        /// </summary>
        [TestMethod]
        public async Task CustomersReturnsDetails()
        {
            //Arrage
            DashbordCustomer customers = new DashbordCustomer() { CompanyName = "test" };           
            
            await _customersControllerUnderTest.Create(customers);
            var customer = db.Customers.Where(c => c.CompanyName == customers.CompanyName).FirstOrDefault();

            //Act
            var result = await _customersControllerUnderTest.Details(customer.CustomerID) as ViewResult;

            //Assert
            Assert.IsNotNull(result);

            var customerss = db.Customers.Where(c => c.CustomerID == customer.CustomerID && c.CompanyName == customers.CompanyName);
            db.Customers.RemoveRange(customerss);
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
            DashbordCustomer customers = new DashbordCustomer() { CompanyName = "test" };
            await _customersControllerUnderTest.Create(customers);
            var customer = db.Customers.Where(c => c.CompanyName == customers.CompanyName).FirstOrDefault();
           
            //Act
            var expected = db.Customers.Count() + 1;
            await _customersControllerUnderTest.Create(customers);
            var actual = db.Customers.Count();
            var customerss = db.Customers.Where(c => c.CustomerID == customer.CustomerID && c.CompanyName == customer.CompanyName);
            //Assert
            Assert.AreEqual(expected, actual);



            db.Customers.RemoveRange(customerss);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if edit returns view.
        /// </summary>
        [TestMethod]
        public async Task CustomersReturnsEdit()
        {
            //Arrage
            DashbordCustomer customers = new DashbordCustomer() { CompanyName = "test" };
            await _customersControllerUnderTest.Create(customers);
            var customer = db.Customers.Where(c => c.CompanyName == customers.CompanyName).FirstOrDefault();           


            //Act
            var result = await _customersControllerUnderTest.Edit(customer.CustomerID) as ViewResult;

            //Assert
            Assert.IsNotNull(result);

            var customerss = db.Customers.Where(c => c.CustomerID == customer.CustomerID && c.CompanyName == customer.CompanyName);
            db.Customers.RemoveRange(customerss);
            db.SaveChanges();

        }

        /// <summary>
        /// Tests if edit make changes into database.
        /// </summary>
        [TestMethod]
        public async Task CustomersReturnsEditEdits()
        {
            //Arrange
            var expectedCustomer = new Customers() {CustomerID="ZZZZZ", CompanyName = "test" };
            db.Customers.Add(expectedCustomer);
            db.SaveChanges();
            db.Entry(expectedCustomer).State = System.Data.Entity.EntityState.Added;

            db.Dispose();
            expectedCustomer.CompanyName = "test2";
            db = new NorthwindDatabase();

            //Act
            await _customersControllerUnderTest.Edit(expectedCustomer);
            db.Entry(expectedCustomer).State = System.Data.Entity.EntityState.Modified;
            var actualCustomer = db.Customers.Find(expectedCustomer.CustomerID);

            //Assert
            Assert.AreEqual(expectedCustomer.CustomerID, actualCustomer.CustomerID);


            var customerss = db.Customers.Where(c => c.CompanyName == expectedCustomer.CompanyName||c.CompanyName==actualCustomer.CompanyName);
            db.Customers.RemoveRange(customerss);
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
            DashbordCustomer customers = new DashbordCustomer() { CompanyName = "test" };
            await _customersControllerUnderTest.Create(customers);
            var customer = db.Customers.Where(c => c.CompanyName == customers.CompanyName).FirstOrDefault();          

            //Act
            var result = _customersControllerUnderTest.Delete(customer.CustomerID);

            //Assert
            Assert.IsNotNull(result);


            var customerss = db.Customers.Where(c => c.CustomerID == customer.CustomerID && c.CompanyName == customer.CompanyName);
            db.Customers.RemoveRange(customerss);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if delete deletes
        /// </summary>
        [TestMethod]
        public async Task CustomersReturnsDeleteDeletes()
        {
            //Arrange
            DashbordCustomer customers = new DashbordCustomer() { CompanyName = "test" };
            await _customersControllerUnderTest.Create(customers);
            var customer = db.Customers.Where(c => c.CompanyName == customers.CompanyName).FirstOrDefault();
            int expected = db.Customers.Count() - 1;

            //Act
            await _customersControllerUnderTest.DeleteConfirmed(customer.CustomerID);
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
            var db = new NorthwindDatabase();
            var customersCount = db.Customers.Count();
            int draw = 1;
            int row = 20;

            //Act
            var jsonData = controller.JsonTableFill(draw, 0, row).Data as JsonDataTable;

            //Assert
            Assert.AreEqual(jsonData.draw, draw);
            Assert.AreEqual(jsonData.recordsTotal, customersCount);
            Assert.IsTrue(jsonData.recordsFiltered <= customersCount);
            db.Dispose();
        }
    }
    
}
