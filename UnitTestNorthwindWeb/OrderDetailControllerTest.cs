using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Linq;
using NorthwindWeb.Context;

namespace UnitTestNorthwindWeb
{
    /// <summary>
    /// Test OrderDetail controller.
    /// </summary>
    [TestClass]
    public class OrderDetailControllerTest
    {
        //Arrange
        OrderDetailController _detailsControllerUnderTest = new OrderDetailController();
        NorthwindDatabase db = new NorthwindDatabase();

       

        /// <summary>
        /// Check Details items from Index action .
        /// </summary>
        [TestMethod]
        public async Task OrderDetailReturnsDetails()
        {
            //Arrage
            Orders OrderTest = new Orders() { OrderID = 22, CustomerID = "ALFKI", EmployeeID = 3 };
            Order_Details detailsTest = new Order_Details() { ProductID = 20, UnitPrice = 23, Quantity = 12, Discount = 1, Order = OrderTest };
            await _detailsControllerUnderTest.Create(detailsTest, OrderTest.OrderID);

            //Act
            var result = await _detailsControllerUnderTest.Details(OrderTest.OrderID, detailsTest.ProductID) as ViewResult;

            //Assert
            Assert.IsNotNull(result);

            var details = db.Order_Details.Where(o => o.OrderID == detailsTest.OrderID && o.ProductID == detailsTest.ProductID && o.UnitPrice == detailsTest.UnitPrice && o.Quantity == detailsTest.Quantity && o.Discount == detailsTest.Discount);
            var orders = db.Orders.Find(OrderTest.OrderID);
            foreach (var orderdet in details)
                db.Order_Details.Remove(orderdet);

            db.Orders.Remove(orders);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if create returns view.
        /// </summary>
        [TestMethod]
        public void OrderDetailReturnsCreate()
        {

            //Arrage

            //Act
            var result = _detailsControllerUnderTest.Create(10222) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests if create inserts into database.
        /// </summary>
        [TestMethod]
        public async Task OrderDetailReturnsCreateCreates()
        {
            //Arrange
            Orders OrderTest = new Orders() { OrderID = 22, CustomerID = "ALFKI", EmployeeID = 3 };
            Order_Details detailsTest = new Order_Details() { ProductID = 17,UnitPrice=23,Quantity=12,Discount=1,Order=OrderTest};
           
            //Act
            var expected = db.Order_Details.Count() + 1;
            await _detailsControllerUnderTest.Create(detailsTest,OrderTest.OrderID);
            var actual = db.Order_Details.Count();
            var details = db.Order_Details.Where(o => o.OrderID == detailsTest.OrderID && o.ProductID == detailsTest.ProductID && o.UnitPrice==detailsTest.UnitPrice && o.Quantity==detailsTest.Quantity && o.Discount==detailsTest.Discount);
            var orders = db.Orders.Find(OrderTest.OrderID);
            
            //Assert
            Assert.AreEqual(expected, actual);


            foreach (var orderdet in details)
                db.Order_Details.Remove(orderdet);

            db.Orders.Remove(orders);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if edit returns view.
        /// </summary>
        [TestMethod]
        public async Task OrderDetailReturnsEdit()
        {
            //Arrage
            Orders OrderTest = new Orders() { OrderID = 22, CustomerID = "ALFKI", EmployeeID = 3 };
            Order_Details detailsTest = new Order_Details() { ProductID = 20, UnitPrice = 23, Quantity = 12, Discount = 1, Order = OrderTest };
            await _detailsControllerUnderTest.Create(detailsTest, OrderTest.OrderID);

            //Act
            var result = await _detailsControllerUnderTest.Edit(OrderTest.OrderID,detailsTest.ProductID) as ViewResult;

            //Assert
            Assert.IsNotNull(result);

            var details = db.Order_Details.Where(o => o.OrderID == detailsTest.OrderID && o.ProductID == detailsTest.ProductID && o.UnitPrice == detailsTest.UnitPrice && o.Quantity == detailsTest.Quantity && o.Discount == detailsTest.Discount);
            var orders = db.Orders.Find(OrderTest.OrderID);
            foreach (var orderdet in details)
                db.Order_Details.Remove(orderdet);

            db.Orders.Remove(orders);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if edit make changes into database.
        /// </summary>
        [TestMethod]
        public async Task OrderDetailReturnsEditEdits()
        {
            //Arrange
            Orders OrderTest = new Orders() { OrderID = 22, CustomerID = "ALFKI", EmployeeID = 3 };
            Order_Details detailsTest = new Order_Details() {ProductID = 20, UnitPrice = 23, Quantity = 12, Discount = 1,Order=OrderTest };
            await _detailsControllerUnderTest.Create(detailsTest,OrderTest.OrderID);
            db.Entry(detailsTest).State = System.Data.Entity.EntityState.Added;

            var expectedDetails = db.Order_Details.Find(detailsTest.OrderID,detailsTest.ProductID);

            db.Dispose();
            detailsTest.UnitPrice = 43;
            detailsTest.Quantity = 22;
            db = new NorthwindDatabase();

            //Act
            await _detailsControllerUnderTest.Edit(detailsTest);
            db.Entry(detailsTest).State = System.Data.Entity.EntityState.Modified;
            var actualDetails = db.Order_Details.Find(detailsTest.OrderID, detailsTest.ProductID);

            //Assert
            Assert.AreEqual(expectedDetails, actualDetails);


            var details = db.Order_Details.Where(o => o.OrderID == detailsTest.OrderID && o.ProductID == detailsTest.ProductID && o.UnitPrice == detailsTest.UnitPrice && o.Quantity == detailsTest.Quantity && o.Discount == detailsTest.Discount);
            var orders = db.Orders.Find(OrderTest.OrderID);
            foreach (var orderdet in details)
                db.Order_Details.Remove(orderdet);

            db.Orders.Remove(orders);
            db.SaveChanges();

        }

        /// <summary>
        /// Tests if delete returns view
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task OrderDetailReturnsDelete()
        {
            //Arrange
            Orders OrderTest = new Orders() { OrderID = 22, CustomerID = "ALFKI", EmployeeID = 3 };
            Order_Details detailsTest = new Order_Details() { ProductID = 17, UnitPrice = 23, Quantity = 12, Discount = 1 ,Order=OrderTest};
            await _detailsControllerUnderTest.Create(detailsTest,OrderTest.OrderID);

            //Act
            var result = _detailsControllerUnderTest.Delete(detailsTest.OrderID,detailsTest.ProductID);

            //Assert
            Assert.IsNotNull(result);



            var details = db.Order_Details.Where(o => o.OrderID == detailsTest.OrderID && o.ProductID == detailsTest.ProductID&&o.UnitPrice==detailsTest.UnitPrice&&o.Quantity==detailsTest.Quantity&&o.Discount==detailsTest.Discount);
            var orders = db.Orders.Find(OrderTest.OrderID);
            foreach (var orderdet in details)
                db.Order_Details.Remove(orderdet);

            db.Orders.Remove(orders);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if delete deletes
        /// </summary>
        [TestMethod]
        public async Task OrderDetailReturnsDeleteDeletes()
        {
            //Arrange
            Orders OrderTest = new Orders() { OrderID = 22, CustomerID = "ALFKI", EmployeeID = 3 };
            Order_Details detailsTest = new Order_Details() { ProductID = 17, UnitPrice = 23, Quantity = 12, Discount = 1 ,Order=OrderTest};
            await _detailsControllerUnderTest.Create(detailsTest,OrderTest.OrderID);
            int expected = db.Order_Details.Count() - 1;

            //Act
            await _detailsControllerUnderTest.DeleteConfirmed(detailsTest.OrderID,detailsTest.ProductID);
            int actual = db.Order_Details.Count();

            //Assert
            Assert.AreEqual(expected, actual);

            var details = db.Order_Details.Where(o => o.OrderID == detailsTest.OrderID && o.ProductID == detailsTest.ProductID && o.UnitPrice == detailsTest.UnitPrice && o.Quantity == detailsTest.Quantity && o.Discount == detailsTest.Discount);
            var orders = db.Orders.Find(OrderTest.OrderID);
            foreach (var orderdet in details)
                db.Order_Details.Remove(orderdet);

            db.Orders.Remove(orders);
            db.SaveChanges();
        }
    }
}
