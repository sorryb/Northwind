using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;

using System.Web.Mvc;
using NorthwindWeb.ViewModels;

namespace UnitTestNorthwindWeb
{
    /// <summary>
    /// Summary description for OrderControllerTest
    /// </summary>
    [TestClass]
    public class OrderControllerTest
    {
        OrderController _orderControllerUnderTest = new OrderController();

        /// <summary>
        /// Check what HomeAdmin action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsHomeAdminView()
        {
            //Arrage

            //Act
            var result = _orderControllerUnderTest.HomeAdmin(null, null, null, "102", "102");

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));

        }

        /// <summary>
        /// Check model return from HomeAdmin action.
        /// </summary>
        [TestMethod]
        public void ReturnsHomeAdminModel()
        {
            //Arrage

            //Act
            var result = _orderControllerUnderTest.HomeAdmin(null, null, null, "102", "102") as ViewResult;
            var model = result.Model;

            //Assert
            Assert.IsNotNull(model);
        }

        /// <summary>
        /// Check what Home action returns.
        /// </summary>
        [TestMethod]
        public void ReturnsHomeView()
        {
            //Arrage
            LoginViewModel user = new LoginViewModel();
            user.UserName = "admin";
            user.Password = "123456";
            AccountController login = new AccountController();
            var c = login.Login(user, "/Order/Home");
            //Act
            var result = _orderControllerUnderTest.HomeAdmin(null, null, null, "102", "102");

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));

        }

        /// <summary>
        /// Check model return from Home action.
        /// </summary>
        [TestMethod]
        public void ReturnsHomenModel()
        {
            //Arrage
            LoginViewModel user = new LoginViewModel();
            user.UserName = "admin";
            user.Password = "123456";
            AccountController login = new AccountController();
            var c = login.Login(user, "url");
            //Act
            var result = _orderControllerUnderTest.HomeAdmin(null, null, null, "102", "102") as ViewResult;
            var model = result.Model;

            //Assert
            Assert.IsNotNull(model);
        }
    }
}
