using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Web.Mvc;

namespace UnitTestNorthwindWeb
{
    /// <summary>
    /// Summary description for DashboardControllerTest
    /// </summary>
    [TestClass]
    public class DashboardControllerTest
    {
        DashboardController _DashboardControllerUnderTest = new DashboardController();


        /// <summary>
        /// Check what Home action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsHomeView()
        {
            //Arrage

            //Act
            var result = _DashboardControllerUnderTest.Home();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));

        }


        /// <summary>
        /// Check model return from Home action .
        /// </summary>
        [TestMethod]
        public void ReturnsModel()
        {
            //Arrage

            //Act
            var result = _DashboardControllerUnderTest.Home() as ViewResult;
            var model = result.Model;

            //Assert
            Assert.IsNotNull(model);
        }

        /// <summary>
        /// Check what Search action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsSearchView()
        {
            //Arrage

            //Act
            var result = _DashboardControllerUnderTest.Search("102", null, "102");

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));

        }

        /// <summary>
        /// Check model return from Search action .
        /// </summary>
        [TestMethod]
        public void ReturnsSearchModel()
        {
            //Arrage

            //Act
            var result = _DashboardControllerUnderTest.Search("102", null, "102") as ViewResult;
            var model = result.Model;

            //Assert
            Assert.IsNotNull(model);
        }

        /// <summary>
        /// Check what MorrisArea action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsMorrisArea()
        {
            //Arrage

            //Act
            var result = _DashboardControllerUnderTest.MorrisArea();

            //Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));

        }


        /// <summary>
        /// Check model return from MorrisArea action .
        /// </summary>
        [TestMethod]
        public void ReturnsMorrisAreaModel()
        {
            //Arrage

            //Act
            var result = _DashboardControllerUnderTest.MorrisArea();
            var model = result.Data;

            //Assert
            Assert.IsNotNull(model);
        }

        /// <summary>
        /// Check what MorrisBar action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsMorrisBar()
        {
            //Arrage

            //Act
            var result = _DashboardControllerUnderTest.MorrisBar();

            //Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));

        }


        /// <summary>
        /// Check model return from MorrisBar action .
        /// </summary>
        [TestMethod]
        public void ReturnsMorrisBarModel()
        {
            //Arrage

            //Act
            var result = _DashboardControllerUnderTest.MorrisBar();
            var model = result.Data;

            //Assert
            Assert.IsNotNull(model);
        }

        /// <summary>
        /// Check what MorrisDonut action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsMorrisDonut()
        {
            //Arrage

            //Act
            var result = _DashboardControllerUnderTest.MorrisDonut();

            //Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));

        }


        /// <summary>
        /// Check model return from MorrisDonut action .
        /// </summary>
        [TestMethod]
        public void ReturnsMorrisDonutModel()
        {
            //Arrage

            //Act
            var result = _DashboardControllerUnderTest.MorrisDonut();
            var model = result.Data;

            //Assert
            Assert.IsNotNull(model);
        }
        
    }
}
