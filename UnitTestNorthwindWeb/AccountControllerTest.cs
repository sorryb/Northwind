using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;

using NorthwindWeb;
using System.Web.Mvc;
using NorthwindWeb.ViewModels;
using NorthwindWeb.Context;
using Moq;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using System.Web;
using System.IO;
using System.Security.Principal;
using System.Security.Claims;

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
            var result = _accountController.Index("succes");

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

        ///<summary>
        /// Check what Login action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsLoginGetViewResult()
        {
            //Arrage

            //Act
            var result = _accountController.Login("url");

            //Assert

            Assert.IsInstanceOfType(result, typeof(ViewResult));


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
            var testId = _accountController.ConfirmEmail("user", "1234");
            //Act
            var result = _accountController.ConfirmEmail("user", "1234");

            //Assert

            Assert.AreEqual(testId.Id + 1, result.Id);

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
        /// Check what ForgotPasswordPost action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsForgotPasswordPostViewResult()
        {
            //Arrage
            ForgotPasswordViewModel model = new ForgotPasswordViewModel();
            model.Email = "asd@gmail.com";
            //Act
            var result = _accountController.ForgotPassword(model);

            //Assert
            Assert.IsInstanceOfType(result, typeof(System.Threading.Tasks.Task<ActionResult>));
        }

        ///<summary>
        /// Check what ForgotPasswordConfirmation action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsForgotPasswordConfirmationViewResult()
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

        ///<summary>
        /// Check what ResetPasswordPost action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsResetPasswordPostViewResult()
        {
            //Arrage
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.Email = "ceva@gmail.com";
            model.Password = "123456";
            model.ConfirmPassword = "123456";
            //Act
            var result = _accountController.ResetPassword(model);

            //Assert


            Assert.IsInstanceOfType(result, typeof(System.Threading.Tasks.Task<ActionResult>));
        }

        ///<summary>
        /// Check what ResetPasswordConfirmation action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsResetPasswordConfirmationViewResult()
        {
            //Arrage

            //Act
            var result = _accountController.ResetPasswordConfirmation();

            //Assert


            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        ///<summary>
        /// Check what SendCode action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsSendCodeViewResult()
        {
            //Arrage

            //Act
            var result = _accountController.SendCode("url", true);

            //Assert


            Assert.IsInstanceOfType(result, typeof(System.Threading.Tasks.Task<ActionResult>));
        }

        ///<summary>
        /// Check what SendCodePost action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsSendCodePostViewResult()
        {
            //Arrage
            SendCodeViewModel sendCode = new SendCodeViewModel();
            //Act
            var result = _accountController.SendCode(sendCode);

            //Assert


            Assert.IsInstanceOfType(result, typeof(System.Threading.Tasks.Task<ActionResult>));
        }

        ///<summary>
        /// Check what ExternalLoginCallback action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsExternalLoginCallbackViewResult()
        {
            //Arrage

            //Act
            var result = _accountController.ExternalLoginCallback("url");

            //Assert


            Assert.IsInstanceOfType(result, typeof(System.Threading.Tasks.Task<ActionResult>));
        }

        ///<summary>
        /// Check what ExternalLoginConfirmationPost action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsExternalLoginConfirmationPostViewResult()
        {
            //Arrage
            ExternalLoginConfirmationViewModel model = new ExternalLoginConfirmationViewModel();
            //Act
            var result = _accountController.ExternalLoginConfirmation(model, "url");

            //Assert


            Assert.IsInstanceOfType(result, typeof(System.Threading.Tasks.Task<ActionResult>));
        }

        ///<summary>
        /// Check what ExternalLoginConfirmationPost action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsLogOffViewResult()
        {
            //Arrage

            //var login = _accountController.Login("url");
            //Act
            var result = _accountController.LogOff();

            //Assert


            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        ///<summary>
        /// Check what ExternalLoginFailure action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsExternalLoginFailureViewResult()
        {
            //Arrage

            //Act
            var result = _accountController.ExternalLoginFailure();

            //Assert


            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        ///<summary>
        /// Check what Account Index action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsAccountIndexViewResult()
        {
            //Arrage

            //Act
            var result = _accountController.Index("succes");

            //Assert


            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        ///<summary>
        /// Check what ChangeUser action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsChangeUserViewResult()
        {
            //Arrage

            //Act
            var result = _accountController.ChangeUser("user");

            //Assert


            Assert.IsInstanceOfType(result, typeof(System.Threading.Tasks.Task<ActionResult>));
        }

        ///<summary>
        /// Check what ChangeUserPost action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsChangeUserPostViewResult()
        {
            //Arrage
            RegisterViewModel model = new RegisterViewModel();
            //Act
            var result = _accountController.ChangeUser(model);

            //Assert


            Assert.IsInstanceOfType(result, typeof(System.Threading.Tasks.Task<ActionResult>));
        }
        ///<summary>
        /// Check what Delete action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsDeleteViewResult()
        {
            //Arrage

            //Act
            var result = _accountController.Delete("user");

            //Assert


            Assert.IsInstanceOfType(result, typeof(System.Threading.Tasks.Task<ActionResult>));
        }

        ///<summary>
        /// Check what DeleteUser action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsDeleteUserViewResult()
        {
            //Arrage
            HttpContext.Current = new HttpContext(
            new HttpRequest("", "http://192.168.17.175:81/", ""),
            new HttpResponse(new StringWriter())
            );

            // User is logged in
            HttpContext.Current.User = new GenericPrincipal(
                new GenericIdentity("username"),
                new string[0]
                );

            // User is logged out
            HttpContext.Current.User = new GenericPrincipal(
                new GenericIdentity(String.Empty),
                new string[0]
                );
            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<ApplicationUserManager>(userStore.Object);
            var authenticationManager = new Mock<IAuthenticationManager>();
            var signInManager = new Mock<ApplicationSignInManager>(userManager.Object, authenticationManager.Object);

            var accountController = new AccountController(userManager.Object, signInManager.Object);
            //Act
            var result = accountController.DeleteUser("admin");

            //Assert


            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        ///<summary>
        /// Check what RolesIndex action returns.
        /// </summary>
        //[TestMethod]
        //public void ReturnsRolesIndexViewResult()
        //{
        //    //Arrage
        //    HttpContext.Current = new HttpContext(
        //    new HttpRequest("", "http://192.168.17.175:81/", ""),
        //    new HttpResponse(new StringWriter())
        //    );

        //    // User is logged in
        //    HttpContext.Current.User = new GenericPrincipal(
        //        new GenericIdentity("username"),
        //        new string[0]
        //        );
        //    // User is logged out
        //    HttpContext.Current.User = new GenericPrincipal(
        //        new GenericIdentity(String.Empty),
        //        new string[0]
        //        );
        //    var owinMock = new Mock<IOwinContext>();
        //    owinMock.Setup(o => o.Authentication.User).Returns(new ClaimsPrincipal());
        //    owinMock.Setup(o => o.Environment).Returns(new Dictionary<string, object> { { "key1", 123 } });
        //    var traceMock = new Mock<TextWriter>();
        //    owinMock.Setup(o => o.TraceOutput).Returns(traceMock.Object);

        //    var userStore = new Mock<IUserStore<ApplicationUser>>();
        //    var userManager = new Mock<ApplicationUserManager>(userStore.Object);
        //    var authenticationManager = new Mock<IAuthenticationManager>();
        //    var signInManager = new Mock<ApplicationSignInManager>(userManager.Object, authenticationManager.Object);

        //    var accountController = new AccountController(userManager.Object, signInManager.Object);
        //    accountController
        //    //Act
        //    var result = accountController.RolesIndex();

        //    //Assert


        //    Assert.IsInstanceOfType(result, typeof(ActionResult));
        //}
        ///<summary>
        /// Check what CreateRole action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsCreateRoleViewResult()
        {
            //Arrage

            //Act
            var result = _accountController.CreateRole();

            //Assert


            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        ///<summary>
        /// Check what Manage action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsManageViewResult()
        {

            //Arrage
            ChangePasswordViewModel password = new ChangePasswordViewModel();
            password.NewPassword = "password";
            password.ConfirmPassword = "password";
            //Act
            var result = _accountController.Manage(password);

            //Assert


            Assert.IsInstanceOfType(result, typeof(System.Threading.Tasks.Task<ActionResult>));
        }

        ///<summary>
        /// Check what JsonTableFill action returns.
        /// </summary>
        //[TestMethod]
        //public void ReturnsJsonTableFillViewResult()
        //{

        //    //Arrage

        //    //Act
        //    var result = _accountController.JsonTableFill(1,1,2);

        //    //Assert


        //    Assert.IsInstanceOfType(result, typeof(System.Threading.Tasks.Task<ActionResult>));
        //}


    }
}
