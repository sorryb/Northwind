using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;

using System.Web.Mvc;

namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class HomeControllerTest
    {
        //Arrange
        HomeController _homeControllerUnderTest = new HomeController();

        [TestMethod]
        public void ReturnsIndexView()
        {
            //Arrage

            //Act
            var result = _homeControllerUnderTest.Index() as ViewResult;

            //Assert
            Assert.AreEqual("Index", result.ViewName);

        }

        [TestMethod]
        public void ReturnsIndexViewResult()
        {
            //Arrage

            //Act
            var result = _homeControllerUnderTest.Index() as ViewResult;

            //Assert

            Assert.IsInstanceOfType(result, typeof(ViewResult));


        }

        [TestMethod]
        public void ReturnsViewBag()
        {
            //Arrage

            //Act
            var result = _homeControllerUnderTest.Index() as ViewResult;

            //Assert
            Assert.AreEqual("Northwind Phone Shop", result.ViewBag.SiteName);
        }

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
