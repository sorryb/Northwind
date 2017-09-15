using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Linq;
using NorthwindWeb.Context;

namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class ProductsControllerTests
    {
        [TestMethod]
        public void ProductsProducts()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.ProductsController();
            var db = new NorthwindDatabase();
            int productCount = db.Products.Count();
            int noProductOnPage = 10;

            //Act
            var resultView = controller.Products("") as ViewResult;
            var modelResult = resultView.Model as PagedList.IPagedList<NorthwindWeb.ViewModels.ViewProductCategoryS>;

            //Assert
            Assert.IsNotNull(resultView);
            Assert.IsTrue(modelResult.TotalItemCount == productCount);
            Assert.IsTrue(modelResult.PageCount * noProductOnPage >= productCount);
            Assert.IsTrue(modelResult.PageNumber == 1);

            db.Dispose();
            controller.Dispose();
        }
    }
}
