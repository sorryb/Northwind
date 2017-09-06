using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using System.Web.Mvc;

namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class ReportsControllerTest
    {
        //Arrange
        ReportsController _ReportsControllerUnderTest = new ReportsController();

        [TestMethod]
        public void ReportsIndexReturnsView()
        {
            //Arrange

            //Act
            var result = _ReportsControllerUnderTest.Index();

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
