using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Linq;
using NorthwindWeb.ViewModels;
using NorthwindWeb.Context;

namespace UnitTestNorthwindWeb
{
    /// <summary>
    /// Test Services controller.
    /// </summary>
    [TestClass]
    public class ServicesControllerTest
    {
        //Arrange
        ServicesController _servicesControllerUnderTest = new ServicesController();
        NorthwindDatabase db = new NorthwindDatabase();

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void ServicesReturnsIndexView()
        {
            //Arrage

            //Act
            var result = _servicesControllerUnderTest.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void ServicesReturnsIndexViewResult()
        {
            //Arrage

            //Act
            var result = _servicesControllerUnderTest.Index() as ViewResult;

            //Assert

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void ServicesReturnsModel()
        {
            //Arrange

            //Act
            var result = _servicesControllerUnderTest.Index() as ViewResult;

            //Assert
            Assert.IsInstanceOfType(result.Model, typeof(ServicesIndex));
        }

        /// <summary>
        /// Check what Index viewbag returns.
        /// </summary>
        [TestMethod]
        public void ServicesReturnsViewBag()
        {

            //Arrage

            //Act
            var result = _servicesControllerUnderTest.Index() as ViewResult;
            var result1 = result.Model as ServicesIndex;
            var countTop4Name = result1.TopFourName.Count();
            var countTop4Products = result1.TopFourProducts.Count();
            var countLast3 = result1.LastThreeProducts.Count();

            //Assert
            Assert.AreEqual(4,countTop4Name);
            Assert.AreEqual(4, countTop4Products);
            Assert.AreEqual(3, countLast3);
        }
    }
}
