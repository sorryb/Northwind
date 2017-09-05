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
    /// Test OrderDetail controller.
    /// </summary>
    [TestClass]
    public class OrderDetailControllerTest
    {
        //Arrange
        OrderDetailController _detailsControllerUnderTest = new OrderDetailController();
        NorthwindModel db = new NorthwindModel();

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsIndexView()
        {
            //Arrage

            //Act
            var result = _detailsControllerUnderTest.Index(1) as ViewResult;

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
            var result = _detailsControllerUnderTest.Index(1) as ViewResult;

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
            var result = _detailsControllerUnderTest.Index(1) as ViewResult;

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
            var result = await _detailsControllerUnderTest.Details(10249,14) as ViewResult;
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
            var result = _detailsControllerUnderTest.Create(10222) as ViewResult;

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
            Order_Details OrderTest = new Order_Details() { ProductID = 17,UnitPrice=23,Quantity=12,Discount=1};
            //Act
            var expected = db.Order_Details.Count() + 1;
            await _detailsControllerUnderTest.Create(OrderTest,10249);
            var actual = db.Order_Details.Count();
            var details = db.Order_Details.Where(o => o.OrderID == OrderTest.OrderID && o.ProductID == OrderTest.ProductID && o.UnitPrice==OrderTest.UnitPrice && o.Quantity==OrderTest.Quantity && o.Discount==OrderTest.Discount);
            //Assert
            Assert.AreEqual(expected, actual);


            foreach (var orderdet in details)
                db.Order_Details.Remove(orderdet);
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
            var result = await _detailsControllerUnderTest.Edit(10249,14) as ViewResult;

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
            Order_Details detailsTest = new Order_Details() {ProductID = 20, UnitPrice = 23, Quantity = 12, Discount = 1 };
            await _detailsControllerUnderTest.Create(detailsTest,10249);
            db.Entry(detailsTest).State = System.Data.Entity.EntityState.Added;

            var expectedDetails = db.Order_Details.Find(detailsTest.OrderID,detailsTest.ProductID);

            db.Dispose();
            detailsTest.UnitPrice = 43;
            detailsTest.Quantity = 22;
            db = new NorthwindModel();

            //Act
            await _detailsControllerUnderTest.Edit(detailsTest);
            db.Entry(detailsTest).State = System.Data.Entity.EntityState.Modified;
            var actualDetails = db.Order_Details.Find(detailsTest.OrderID, detailsTest.ProductID);

            //Assert
            Assert.AreEqual(expectedDetails, actualDetails);


            var details = db.Order_Details.Where(o => (o.OrderID == 10249 && o.ProductID == 20 && o.UnitPrice == 23 && o.Quantity == 12 && o.Discount==1) || (o.OrderID == 10249 && o.ProductID == 20 && o.UnitPrice == 43 && o.Quantity == 22 && o.Discount==1));
            foreach (var orderdet in details)
                db.Order_Details.Remove(orderdet);
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
            Order_Details detailsTest = new Order_Details() { ProductID = 17, UnitPrice = 23, Quantity = 12, Discount = 1 };
            await _detailsControllerUnderTest.Create(detailsTest,10249);

            //Act
            var result = _detailsControllerUnderTest.Delete(detailsTest.OrderID,detailsTest.ProductID);

            //Assert
            Assert.IsNotNull(result);



            var details = db.Order_Details.Where(o => o.OrderID == detailsTest.OrderID && o.ProductID == detailsTest.ProductID&&o.UnitPrice==detailsTest.UnitPrice&&o.Quantity==detailsTest.Quantity&&o.Discount==detailsTest.Discount);
            foreach (var orderdet in details)
                db.Order_Details.Remove(orderdet);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if delete deletes
        /// </summary>
        [TestMethod]
        public async Task ReturnsDeleteDeletes()
        {
            //Arrange
            Order_Details detailsTest = new Order_Details() { ProductID = 17, UnitPrice = 23, Quantity = 12, Discount = 1 };
            await _detailsControllerUnderTest.Create(detailsTest,10249);
            int expected = db.Order_Details.Count() - 1;

            //Act
            await _detailsControllerUnderTest.DeleteConfirmed(detailsTest.OrderID,detailsTest.ProductID);
            int actual = db.Order_Details.Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
