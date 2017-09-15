using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class ManageControllerTests
    {
        //[TestMethod]
        //[ExpectedException(typeof(NotImplementedException))]
        //public void ManageIndex()
        //{
        //    //Arrange
        //    var controller = new NorthwindWeb.Controllers.ManageController();

        //    //Act
        //    var a = controller.Index();
        //}

        [TestMethod]
        public void ManageRemoveLogin()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.ManageController();

            //Act
            var result = controller.RemoveLogin("", "");

            //Assert
            Assert.IsTrue(result.IsCompleted);
        }

        [TestMethod]
        public void ManageAddPhoneNumber()
        {
            //Arrange   
            var controller = new NorthwindWeb.Controllers.ManageController();

            //Act
            var result = controller.AddPhoneNumber() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ManageAddPhoneNumberItem()
        {
            //Arrange   
            var controller = new NorthwindWeb.Controllers.ManageController();

            //Act
            var result = controller.AddPhoneNumber(new NorthwindWeb.ViewModels.AddPhoneNumberViewModel());

            //Assert
            Assert.IsTrue(result.IsCompleted);
        }

        [TestMethod]
        public void ManageEnableTwoFactorAuthentication()
        {
            //Arrange   
            var controller = new NorthwindWeb.Controllers.ManageController();

            //Act
            var result = controller.EnableTwoFactorAuthentication();

            //Assert
            Assert.IsTrue(result.IsCompleted);
        }

        [TestMethod]
        public void ManageDisableTwoFactorAuthentication()
        {
            //Arrange   
            var controller = new NorthwindWeb.Controllers.ManageController();

            //Act
            var result = controller.DisableTwoFactorAuthentication();

            //Assert
            Assert.IsTrue(result.IsCompleted);
        }

        [TestMethod]
        public void ManageVerifyPhoneNumber()
        {
            //Arrange   
            var controller = new NorthwindWeb.Controllers.ManageController();

            //Act
            var result = controller.VerifyPhoneNumber("0734570099");
            var result2 = controller.VerifyPhoneNumber(new NorthwindWeb.ViewModels.VerifyPhoneNumberViewModel());

            //Assert
            Assert.IsTrue(result.IsCompleted);
            Assert.IsTrue(result2.IsCompleted);
        }

        [TestMethod]
        public void ManageRemovePhoneNumber()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.ManageController();

            //Act
            var result = controller.RemovePhoneNumber();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ManageChangePassword()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.ManageController();

            //Act
            var result = controller.ChangePassword();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ManageChangePasswordItem()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.ManageController();

            //Act
            var result = controller.ChangePassword(new NorthwindWeb.ViewModels.ChangePasswordViewModel());

            //Assert
            Assert.IsTrue(result.IsCompleted);
        }

        [TestMethod]
        public void ManageSetPassword()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.ManageController();

            //Act
            var result = controller.SetPassword();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ManageSetPasswordItem()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.ManageController();

            //Act
            var result = controller.SetPassword(new NorthwindWeb.ViewModels.SetPasswordViewModel());

            //Assert
            Assert.IsTrue(result.IsCompleted);
        }

        [TestMethod]
        public void ManageManageLogin()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.ManageController();

            //Act
            var result = controller.ManageLogins(new NorthwindWeb.Controllers.ManageController.ManageMessageId());

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]

        public void ManangeLinkLogin()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.ManageController();

            //Act
            var result = controller.LinkLogin("");
        }

        [TestMethod]
        public void ManageLinkLoginCallback()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.ManageController();

            //Act
            var result = controller.LinkLoginCallback();

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
