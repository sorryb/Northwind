﻿using System;
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
    public class AboutControllerTest
    {
        //Arrange
        AboutController _aboutControllerUnderTest = new AboutController();

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsIndexView()
        {
            //Arrage

            //Act
            var result = _aboutControllerUnderTest.Index() as ViewResult;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsIndexViewResult()
        {
            //Arrage

            //Act
            var result = _aboutControllerUnderTest.Index() as ViewResult;

            //Assert

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        /// <summary>
        /// Check what Index viewbag returns.
        /// </summary>
        [TestMethod]
        public void ReturnsAboutUs()
        {
            //Arrage

            //Act
            var aboutus = _aboutControllerUnderTest.Index() as ViewResult;
            var model = aboutus.Model;

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
            Assert.AreEqual("AboutController", "AboutController");
        }
    }
}
