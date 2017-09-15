using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using NorthwindWeb.Models;
using System.Threading.Tasks;
using System.Linq;
using NorthwindWeb.Models.ServerClientCommunication;
using NorthwindWeb.Context;

namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class SupplierControllerTests
    {
        /// <summary>
        /// create Suppliers unit test
        /// </summary>
        [TestMethod]
        public void SuppliersCreate()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.SuppliersController();

            //Act
            var create = controller.Create();

            //Assert
            Assert.IsNotNull(create);
            controller.Dispose();
        }

        /// <summary>
        /// Unit test for Create Suppliers in database
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task SuppliersCreateItemAsync()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.SuppliersController();
            var db = new NorthwindDatabase();
            int SuppliersCountBefore = db.Suppliers.Count();
            var Suppliers = new Suppliers()
            {
                CompanyName = "TestSuppliersCreate",
                ContactName = "contact",
            };

            //Act
            await controller.Create(Suppliers);

            //Assert
            Assert.AreEqual(SuppliersCountBefore + 1, db.Suppliers.Count());
            db.Entry(Suppliers).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            controller.Dispose();
            db.Dispose();
        }

        /// <summary>
        /// Unit test for edit page
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task SuppliersEditAsync()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.SuppliersController();

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
        public async System.Threading.Tasks.Task SuppliersEditItemAsync()
        {
            //Arrange
            //init
            var controller = new NorthwindWeb.Controllers.SuppliersController();
            var db = new NorthwindDatabase();
            //create Suppliers
            var Suppliers = new Suppliers()
            {
                CompanyName = "TestSuppliersCreate",
                ContactName = "contact",
            };
            db.Entry(Suppliers).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            //detach Suppliers from db
            db.Entry(Suppliers).State = System.Data.Entity.EntityState.Detached;
            db.SaveChanges();
            //edit name of Suppliers
            string name = Suppliers.CompanyName;
            string nameExpected = "test1223";
            Suppliers.CompanyName = nameExpected;

            //Act
            //run controller action
            await controller.Edit(Suppliers);
            controller.Dispose();
            string actual = db.Suppliers.Where(x => x.SupplierID == Suppliers.SupplierID).First().CompanyName;

            //Assert
            //check and delete Suppliers
            Assert.AreEqual(nameExpected, actual);
            Suppliers = db.Suppliers.Where(x => x.SupplierID == Suppliers.SupplierID).First();
            Suppliers.CompanyName = name;
            db.Entry(Suppliers).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            db.Dispose();
        }

        /// <summary>
        /// Unit test for Delete page of Suppliers
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task SuppliersDeleteAsync()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.SuppliersController();

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
        [ExpectedException(typeof(System.InvalidOperationException))]
        public async System.Threading.Tasks.Task SuppliersDeleteItemAsync()
        {
            //Arrange
            //init
            var controller = new NorthwindWeb.Controllers.SuppliersController();
            var db = new NorthwindDatabase();
            //create Suppliers
            var Suppliers = new Suppliers()
            {
                CompanyName = "TestSuppliersCreate",
                ContactName = "contact",
            };
            db.Entry(Suppliers).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            //detach Suppliers from db
            db.Entry(Suppliers).State = System.Data.Entity.EntityState.Detached;
            db.SaveChanges();

            //Act
            //run controller action
            await controller.DeleteConfirmed(Suppliers.SupplierID);
            controller.Dispose();

            //Assert
            //this will throw a InvalidOperationException
            var actualSuppliers = db.Suppliers.Where(x => x.SupplierID == Suppliers.SupplierID).First();

        }

        /// <summary>
        /// Unit test for json response to fill dinamic datatable
        /// </summary>
        [TestMethod]
        public void SuppliersJsonTableFill()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.SuppliersController();
            var db = new NorthwindDatabase();
            var SuppliersCount = db.Suppliers.Count();
            int draw = 1;
            int row = 20;

            //Act
            var jsonData = controller.JsonTableFill(draw, 0, row).Data as JsonDataTable;

            //Assert
            Assert.AreEqual(jsonData.draw, draw);
            Assert.AreEqual(jsonData.recordsTotal, SuppliersCount);
            Assert.IsTrue(jsonData.recordsFiltered <= SuppliersCount);
            db.Dispose();
        }
    }
}
