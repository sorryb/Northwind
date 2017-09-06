using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Linq;

namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class ProductsController
    {
        [TestMethod]
        public void ProductsProducts()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.ProductsController();
            var db = new NorthwindWeb.Models.NorthwindModel();
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
