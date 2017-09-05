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
    /// Test Orders controller.
    /// </summary>
    [TestClass]
    public class OrdersControllerTest
    {
        //Arrange
        OrdersController _ordersControllerUnderTest = new OrdersController();
        NorthwindModel db = new NorthwindModel();

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsIndexView()
        {
            //Arrage

            //Act
            var result = _ordersControllerUnderTest.Index("") as ViewResult;

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
            var result = _ordersControllerUnderTest.Index("") as ViewResult;

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
            var result = _ordersControllerUnderTest.Index("") as ViewResult;

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
            var result = await _ordersControllerUnderTest.Details(10249) as ViewResult;
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
            var result = _ordersControllerUnderTest.Create() as ViewResult;

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
            Orders OrderTest = new Orders() {OrderID=22, CustomerID= "ALFKI", EmployeeID=3};
            //Act
            var expected = db.Orders.Count() + 1;
            await _ordersControllerUnderTest.Create(OrderTest);
            var actual = db.Orders.Count();
            var orders = db.Orders.Where(o=>o.OrderID==OrderTest.OrderID &&o.CustomerID == OrderTest.CustomerID && o.EmployeeID == OrderTest.EmployeeID);
            //Assert
            Assert.AreEqual(expected, actual);



            db.Orders.RemoveRange(orders);
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
            var result =await _ordersControllerUnderTest.Edit(10249) as ViewResult;

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
            Orders orderTest = new Orders() { OrderID = 222, EmployeeID = 3 };
            await _ordersControllerUnderTest.Create(orderTest);
            db.Entry(orderTest).State = System.Data.Entity.EntityState.Added;

            var expectedOrder = db.Orders.Find(orderTest.OrderID);

            db.Dispose();
            orderTest.EmployeeID = 4;
            db = new NorthwindModel();

            //Act
            await _ordersControllerUnderTest.Edit(orderTest);
            db.Entry(orderTest).State = System.Data.Entity.EntityState.Modified;
            var actualOrder = db.Orders.Find(orderTest.OrderID);

            //Assert
            Assert.AreEqual(expectedOrder, actualOrder);


            var orders = db.Orders.Where(o => (o.OrderID==222 && o.EmployeeID == 3) || (o.OrderID==222 && o.EmployeeID == 4));
            db.Orders.RemoveRange(orders);
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
            Orders orderTest = new Orders() { OrderID = 222, EmployeeID = 3 };
            await _ordersControllerUnderTest.Create(orderTest);

            //Act
            var result = _ordersControllerUnderTest.Delete(orderTest.OrderID);

            //Assert
            Assert.IsNotNull(result);



            var orders = db.Orders.Where(o => o.OrderID == orderTest.OrderID && o.EmployeeID == orderTest.EmployeeID);
            db.Orders.RemoveRange(orders);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if delete deletes
        /// </summary>
        [TestMethod]
        public async Task ReturnsDeleteDeletes()
        {
            //Arrange
            Orders orderTest = new Orders() { OrderID = 222, EmployeeID = 3 };
            await _ordersControllerUnderTest.Create(orderTest);
            int expected = db.Orders.Count() - 1;

            //Act
            await _ordersControllerUnderTest.DeleteConfirmed(orderTest.OrderID);
            int actual = db.Orders.Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
