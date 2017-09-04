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
        public void IndexReturnsView()
        {
            //Arrange

            //Act
            var result = _ReportsControllerUnderTest.Index() as ViewResult;

            //Assert
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
