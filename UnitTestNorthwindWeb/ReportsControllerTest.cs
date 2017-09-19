using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.ViewModels;
using System;

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
            var login = new ReportLoginViewModel()
            {
                Username = "",
                Password = ""
            };
            //Act
            try
            {
                var result = _ReportsControllerUnderTest.Index(login);

                //Assert
                Assert.IsNotNull(result);
            }
            catch (Exception e)
            {
                if (!(e is ArgumentException))
                    Assert.Fail();
            }

        }
    }
}
