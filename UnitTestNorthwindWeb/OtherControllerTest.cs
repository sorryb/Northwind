using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using System.Web.Mvc;

namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class OtherControllerTest
    {
        //Arrange
        private OtherController _OtherControllerUnderTest = new OtherController();

        [TestMethod]
        public void OtherFullWidthReturnsView()
        {
            //Arrange

            //Act
            var result = _OtherControllerUnderTest.FullWidth() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OtherSideBarReturnsView()
        {
            //Arrange

            //Act
            var result = _OtherControllerUnderTest.SideBar() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OtherFaqReturnsView()
        {
            //Arrange

            //Act
            var result = _OtherControllerUnderTest.Faq() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OtherFourOhFourReturnsView()
        {
            //Arrange

            //Act
            var result = _OtherControllerUnderTest.FourOhFour() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
