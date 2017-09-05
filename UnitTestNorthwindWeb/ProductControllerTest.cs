using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using System.Linq;
using NorthwindWeb.Models;

namespace UnitTestNorthwindWeb
{
    /// <summary>
    /// Test Product Controller
    /// </summary>
    [TestClass]
    public class ProductControllerTest
    {
        //index result test
        [TestMethod]
        public void ProductIndex()
        {
            ProductController controller = new ProductController();
            var stringtest = "something";

            controller.Index(stringtest);

            Assert.AreEqual(stringtest, controller.ViewBag.category);
            controller.Dispose();
        }

        //product details test
        [TestMethod]
        public void ProductDetails()
        {
            ProductController controller = new ProductController();
            int? productIdNull = null;
            int? productId = 1;
            int? productIdOver = int.MaxValue;

            var resultProductNull = controller.Details(productIdNull);
            var resultProduct = controller.Details(productId);
            var resultProductOver = controller.Details(productIdOver);

            Assert.IsNotNull(resultProductNull);
            Assert.IsNotNull(resultProduct);
            Assert.IsNotNull(resultProductOver);
            controller.Dispose();
        }

        [TestMethod]
        public void ProductCreate()
        {
            var controller = new ProductController();

            var create = controller.Create();

            Assert.IsNotNull(create);
            controller.Dispose();
        }

        [TestMethod]
        public async System.Threading.Tasks.Task ProductCreateItemAsync()
        {
            var controller = new ProductController();
            var db = new NorthwindWeb.Models.NorthwindModel();
            int productCountBefore = db.Products.Count();
            var product = new Products()
            {
                CategoryID = 4,
                ProductName = "TestProductCreate",
                Discontinued = false,
                QuantityPerUnit = "1",
                UnitPrice = 1,
                UnitsInStock = 1,
                ReorderLevel = 0,
                SupplierID = 1,
                UnitsOnOrder = 0
            };

            await controller.Create(product);

            Assert.AreEqual(productCountBefore + 1, db.Products.Count());
            db.Entry(product).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            controller.Dispose();
            db.Dispose();
        }

        [TestMethod]
        public async System.Threading.Tasks.Task ProductEditAsync()
        {
            var controller = new ProductController();
            var db = new NorthwindWeb.Models.NorthwindModel();
            var product = db.Products.First();
            db.Dispose();
            string name = product.ProductName;
            product.ProductName = "asd";
            await controller.Edit(product);

            db = new NorthwindWeb.Models.NorthwindModel();
            Assert.Equals("asd", db.Products.Where(x => x.ProductID == 1).First().ProductName);
           
        }

    }
}
