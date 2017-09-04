using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Threading.Tasks;

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
        /// Check Create items from Index action.
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
        /// Check Create items from Index action .
        /// </summary>
        [TestMethod]
        public async Task ReturnsCreateCreates()
        {
            //Arrage

            //Act
            var result = await _ordersControllerUnderTest.Details(10249) as ViewResult;
            var model = result.Model;

            //Assert
            Assert.IsNotNull(model);
        }
    }
}
