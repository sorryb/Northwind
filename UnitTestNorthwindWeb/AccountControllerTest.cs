using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;

using System.Web.Mvc;
using NorthwindWeb.ViewModels;

namespace UnitTestNorthwindWeb
{
    /// <summary>
    /// Summary description for AccountControllerTest
    /// </summary>
    [TestClass]
    public class AccountControllerTest
    {
        AccountController _accountController = new AccountController();



        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsIndexViewResult()
        {
            //Arrage

            //Act
            var result = _accountController.Index();

            //Assert

            Assert.IsInstanceOfType(result, typeof(ViewResult));


        }

        ///<summary>
        /// Check what Login action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsLoginViewResult()
        {
            //Arrage
            var loginModel = new LoginViewModel();
            loginModel.UserName = "UserName";
            loginModel.Password = "this1_UserName";
            //Act
            var result = _accountController.Login(loginModel, "url");

            //Assert

            Assert.IsInstanceOfType(result, typeof(System.Threading.Tasks.Task<System.Web.Mvc.ActionResult>));


        }


        /// <summary>
        /// Check what Login viewbag returns.
        /// </summary>
        [TestMethod]
        public void ReturnsLoginViewBag()
        {
            //Arrage

            //Act
            var result = _accountController.Login("url") as ViewResult;

            //Assert
            Assert.AreEqual("url", result.ViewBag.ReturnUrl);
        }


        ///<summary>
        /// Check what VerifyCode action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsVerifyCodeResult()
        {
            //Arrage
            var loginModel = new LoginViewModel();
            loginModel.UserName = "UserName";
            loginModel.Password = "this1_UserName";
            //Act
            var result = _accountController.VerifyCode("provider", "returnUrl", false);

            //Assert

            Assert.IsNotNull(result);


        }
        ///<summary>
        /// Check what VerifyCode Post action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsVerifyCodePostResult()
        {
            //Arrage
            var verifyCode = new VerifyCodeViewModel();
            verifyCode.Provider = "UserName";
            verifyCode.Code = "this1_UserName";
            //Act
            var result = _accountController.VerifyCode(verifyCode);

            //Assert

            Assert.IsInstanceOfType(result, typeof(System.Threading.Tasks.Task<ActionResult>));


        }

        ///<summary>
        /// Check what Register action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsRegisterViewResult()
        {
            //Arrage

            //Act
            var result = _accountController.Register();

            //Assert

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        ///<summary>
        /// Check what Register action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsRegisterPostViewResult()
        {
            //Arrage
            RegisterViewModel register = new RegisterViewModel();
            //Act
            var result = _accountController.Register(register);

            //Assert

            Assert.IsInstanceOfType(result, typeof(System.Threading.Tasks.Task<ActionResult>));

        }

        ///<summary>
        /// Check what ConfirmEmail action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsConfirmEmailViewResult()
        {
            //Arrage
            
            //Act
            var result = _accountController.ConfirmEmail("user","1234");

            //Assert
            
            Assert.AreEqual(5, result.Id);

        }

        ///<summary>
        /// Check what ForgotPassword action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsForgotPasswordViewResult()
        {
            //Arrage

            //Act
            var result = _accountController.ForgotPassword();

            //Assert


            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        ///<summary>
        /// Check what ResetPassword action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsResetPasswordViewResult()
        {
            //Arrage

            //Act
            var result = _accountController.ResetPassword("");

            //Assert


            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
    }
}
