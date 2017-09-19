using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using System.Linq;
using NorthwindWeb.Models;
using System.Web.Helpers;
using NorthwindWeb.Models.ServerClientCommunication;
using NorthwindWeb.ViewModels;
using NorthwindWeb.Context;

namespace UnitTestNorthwindWeb
{
    /// <summary>
    /// Test Product Controller
    /// </summary>
    [TestClass]
    public class ProductControllerTest
    {
        /// <summary>
        /// Unit test for index
        /// </summary>
        [TestMethod]
        public void ProductIndex()
        {
            //Arrange
            ProductController controller = new ProductController();
            var stringtest = "something";

            //Act
            controller.Index(stringtest);

            //Assert
            Assert.AreEqual(stringtest, controller.ViewBag.category);
            controller.Dispose();
        }

        /// <summary>
        /// unit test for product detail
        /// </summary>
        [TestMethod]
        public void ProductDetails()
        {
            //Arrange
            ProductController controller = new ProductController();
            int? productIdNull = null;
            int? productId = 1;
            int? productIdOver = int.MaxValue;

            //Act
            var resultProductNull = controller.Details(productIdNull);
            var resultProduct = controller.Details(productId);
            var resultProductOver = controller.Details(productIdOver);

            //Assert
            Assert.IsNotNull(resultProductNull);
            Assert.IsNotNull(resultProduct);
            Assert.IsNotNull(resultProductOver);
            controller.Dispose();
        }

        /// <summary>
        /// create product unit test
        /// </summary>
        [TestMethod]
        public void ProductCreate()
        {
            //Arrange
            var controller = new ProductController();

            //Act
            var create = controller.Create();

            //Assert
            Assert.IsNotNull(create);
            controller.Dispose();
        }

        /// <summary>
        /// Unit test for Create product in database
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task ProductCreateItemAsync()
        {
            //Arrange
            var controller = new ProductController();
            var db = new NorthwindDatabase();
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

            //Act
            await controller.Create(product, null);

            //Assert
            Assert.AreEqual(productCountBefore + 1, db.Products.Count());
            db.Entry(db.Products.Where(x => x.ProductName == product.ProductName).First()).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            controller.Dispose();
            db.Dispose();
        }

        /// <summary>
        /// Unit test for edit page
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task ProductEditAsync()
        {
            //Arrange
            var controller = new ProductController();

            //Act
            var view = await controller.Edit(1);
            var view1 = await controller.Edit(int.MaxValue);
            var view2 = await controller.Edit(-1);

            //Assert
            Assert.IsNotNull(view);
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);
            controller.Dispose();
        }

        /// <summary>
        /// Unit test method for edit item in database
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task ProductEditItemAsync()
        {
            //Arrange
            //init
            var controller = new ProductController();
            var db = new NorthwindDatabase();
            //create product
            var product = new Products() { ProductName = "test", CategoryID = 1, SupplierID = 1};
            db.Entry(product).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            //detach product from db
            db.Entry(product).State = System.Data.Entity.EntityState.Detached;
            db.SaveChanges();
            //edit name of product
            string name = product.ProductName;
            string nameExpected = "test12232";
            product.ProductName = nameExpected;

            //Act
            //run controller action
            await controller.Edit(product, null);
            controller.Dispose();
            string actual = db.Products.Where(x => x.ProductID == product.ProductID).First().ProductName;

            //Assert
            //check and delete product
            Assert.AreEqual(nameExpected, actual);
            product = db.Products.Where(x => x.ProductID == product.ProductID).First();
            product.ProductName = name;
            db.Entry(product).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            db.Dispose();
        }

        /// <summary>
        /// Unit test for Delete page of Product
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task ProductDeleteAsync()
        {
            //Arrange
            var controller = new ProductController();

            //Act
            var view = await controller.Edit(1);
            var view1 = await controller.Delete(int.MaxValue);
            var view2 = await controller.Edit(-1);

            //Assert
            Assert.IsNotNull(view);
            Assert.IsNotNull(view1);
            Assert.IsNotNull(view2);

            controller.Dispose();
        }

        /// <summary>
        /// Unit test for actually delete item in database
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task ProductDeleteItemAsync()
        {
            //Arrange
            var controller = new ProductController();
            var db = new NorthwindDatabase();
            //create product
            var product = new Products() { ProductName = "test", CategoryID = 1, SupplierID = 1 };
            db.Entry(product).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();

            //Act
            try
            {
                //run controller action
                await controller.DeleteConfirmed(product.ProductID);
                controller.Dispose();
            }
            catch (Exception ex)
            {
                //image not found
                if (!(ex is NullReferenceException))
                {
                    throw;
                }
            }
            //Assert
            //this will throw a InvalidOperationException
            if (db.Products.Any(x => x.ProductID == product.ProductID)) {
                Assert.Fail();
            }
            
        }

        /// <summary>
        /// Unit test for json response to fill dinamic datatable
        /// </summary>
        [TestMethod]
        public void ProductJsonTableFill()
        {
            //Arrange
            var controller = new ProductController();
            var db = new NorthwindDatabase();
            var productCount = db.Products.Count();
            int draw = 1;
            int row = 20;

            //Act
            var jsonData =   controller.JsonTableFill(draw, 0, row).Data as JsonDataTable;

            //Assert
            Assert.AreEqual(jsonData.draw, draw);
            Assert.AreEqual(jsonData.recordsTotal, productCount);
            Assert.IsTrue(jsonData.recordsFiltered <= productCount);
            db.Dispose();
        }

    }
}
