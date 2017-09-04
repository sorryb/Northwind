using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;

using System.Web.Mvc;

namespace UnitTestNorthwindWeb
{
    /// <summary>
    /// Test Home controller.
    /// </summary>
    [TestClass]
    public class HomeControllerTest
    {
        //Arrange
        HomeController _homeControllerUnderTest = new HomeController();

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsIndexView()
        {
            //Arrage

            //Act
            var result = _homeControllerUnderTest.Index() as ViewResult;

            //Assert
            Assert.AreEqual("Index", result.ViewName);

        }

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsIndexViewResult()
        {
            //Arrage

            //Act
            var result = _homeControllerUnderTest.Index() as ViewResult;

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
            var result = _homeControllerUnderTest.Index() as ViewResult;

            //Assert
            Assert.AreEqual("Northwind Phone Shop", result.ViewBag.SiteName);
        }

        /// <summary>
        /// Check Menu items from Index action .
        /// </summary>
        [TestMethod]
        public void ReturnsMenuCategories()
        {
            //Arrage

            //Act
            var result = _homeControllerUnderTest.Menu() as ViewResult;
            var model = result.Model;

            //Assert
            Assert.IsNotNull(model);
        }

        /// <summary>
        /// Sample test method.
        /// </summary>
        [TestMethod]
        public void SampleTest()
        {
            //Arrage

            //Act

            //Assert
            Assert.AreEqual("HomeController", "HomeController");
        }
    }
}
