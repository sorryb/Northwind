using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Linq;
using NorthwindWeb.Models.ServerClientCommunication;
using NorthwindWeb.Context;

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
        NorthwindDatabase db = new NorthwindDatabase();

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void OrdersReturnsIndexView()
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
        public void OrdersReturnsIndexViewResult()
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
        public void OrdersReturnsViewBag()
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
        public async Task OrdersReturnsDetails()
        {
            //Arrage
            Orders orderTest = new Orders() { OrderID = 222, EmployeeID = 3 };
            await _ordersControllerUnderTest.Create(orderTest);

            //Act
            var result = await _ordersControllerUnderTest.Details(orderTest.OrderID) as ViewResult;

            //Assert
            Assert.IsNotNull(result);

            var orders = db.Orders.Where(o => o.OrderID == orderTest.OrderID && o.EmployeeID == orderTest.EmployeeID);
            db.Orders.RemoveRange(orders);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if create returns view.
        /// </summary>
        [TestMethod]
        public void OrdersReturnsCreate()
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
        public async Task OrdersReturnsCreateCreates()
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
        public async Task OrdersReturnsEdit()
        {
            //Arrage
            Orders orderTest = new Orders() { OrderID = 222, EmployeeID = 3 };
            await _ordersControllerUnderTest.Create(orderTest);

            //Act
            var result =await _ordersControllerUnderTest.Edit(orderTest.OrderID) as ViewResult;

            //Assert
            Assert.IsNotNull(result);

            var orders = db.Orders.Where(o => o.OrderID == orderTest.OrderID && o.EmployeeID == orderTest.EmployeeID);
            db.Orders.RemoveRange(orders);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if edit make changes into database.
        /// </summary>
        [TestMethod]
        public async Task OrdersReturnsEditEdits()
        {
            //Arrange
            Orders orderTest = new Orders() { OrderID = 222, EmployeeID = 3 };
            await _ordersControllerUnderTest.Create(orderTest);
            db.Entry(orderTest).State = System.Data.Entity.EntityState.Added;

            var expectedOrder = db.Orders.Find(orderTest.OrderID);

            db.Dispose();
            orderTest.EmployeeID = 4;
            db = new NorthwindDatabase();

            //Act
            await _ordersControllerUnderTest.Edit(orderTest);
            db.Entry(orderTest).State = System.Data.Entity.EntityState.Modified;
            var actualOrder = db.Orders.Find(orderTest.OrderID);

            //Assert
            Assert.AreEqual(expectedOrder, actualOrder);


            var orders = db.Orders.Where(o => o.OrderID == orderTest.OrderID && o.EmployeeID == orderTest.EmployeeID);
            db.Orders.RemoveRange(orders);
            db.SaveChanges();

        }

        /// <summary>
        /// Tests if delete returns view
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task OrdersReturnsDelete()
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
        public async Task OrdersReturnsDeleteDeletes()
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

        /// <summary>
        /// Unit test for json response to fill dinamic datatable
        /// </summary>
        [TestMethod]
        public void OrdersJsonTableFill()
        {
            //Arrange
            var controller = new OrdersController();
            var db = new NorthwindDatabase();
            var ordersCount = db.Orders.Count();
            int draw = 1;
            int row = 20;

            //Act
            var jsonData = controller.JsonTableFill(draw, 0, row).Data as JsonDataTable;

            //Assert
            Assert.AreEqual(jsonData.draw, draw);
            Assert.AreEqual(jsonData.recordsTotal, ordersCount);
            Assert.IsTrue(jsonData.recordsFiltered <= ordersCount);
            db.Dispose();
        }
    }
}
