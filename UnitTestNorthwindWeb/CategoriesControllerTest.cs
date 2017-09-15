using NorthwindWeb.Models.ServerClientCommunication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using System.Threading.Tasks;
using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Linq;
using System;
using NorthwindWeb.Context;

namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class CategoriesControllerTest
    {
        //Arrange
        CategoriesController _categoriesControllerTest = new CategoriesController();
        NorthwindDatabase db = new NorthwindDatabase();

        /// <summary>
        /// Sample test method.
        /// </summary>
        [TestMethod]
        public void SampleTestCategory()
        {
            //Arrage

            //Act

            //Assert
            Assert.AreEqual("CategoriesController", "CategoriesController");
        }

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void CategoryReturnsIndexView()
        {
            //Arrage

            //Act
            var result = _categoriesControllerTest.Index("");

            //Assert
            Assert.IsNotNull(result);

        }

        /// <summary>
        /// Check what Index action returns.
        /// </summary>
        [TestMethod]
        public void CategoryReturnsIndexViewResult()
        {
            //Arrage

            //Act
            var result = _categoriesControllerTest.Index("");

            //Assert
            Assert.IsNotNull(result);


        }

        

        /// <summary>
        /// Check Details items from Index action .
        /// </summary>
        [TestMethod]
        public async Task CategoryReturnsDetails()
        {
            //Arrage
            Categories categoriesTest = new Categories() { CategoryName = "Accesorii" };
            //Act
            var result = await _categoriesControllerTest.Details(categoriesTest.CategoryID) ;
            

            //Assert
            Assert.IsNotNull(result);
        }


        /// <summary>
        /// Tests if create returns view.
        /// </summary>
        [TestMethod]
        public void CreateReturnsViewCategory()
        {
            //Arrange

            //Act
            var result = _categoriesControllerTest.Create() ;

            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests if create inserts into database.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CategoryCreate()
        {
            //Arrange
            Categories CategoriesTest = new Categories() {CategoryID=14, CategoryName = "foto"};
            //Act
            var expected = db.Categories.Count() + 1;
            await _categoriesControllerTest.Create(CategoriesTest);
            var actual = db.Categories.Count();
            var category = db.Categories.Where(c => c.CategoryName == CategoriesTest.CategoryName);
            
            //Assert
            Assert.AreEqual(expected, actual);

            db.Categories.RemoveRange(category);
            db.SaveChanges();
        }

        

        /// <summary>
        /// Tests if delete returns view
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CategoryDeleteReturnsView()
        {
            //Arrange
            Categories categoriesTest = new Categories() { CategoryName = "foto"};
            await _categoriesControllerTest.Create(categoriesTest);

            //Act
            var result = _categoriesControllerTest.Delete(categoriesTest.CategoryID);

            //Assert
            Assert.IsNotNull(result);

            var category = db.Categories.Where(c => c.CategoryName == categoriesTest.CategoryName);
            db.Categories.RemoveRange(category);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if delete deletes
        /// </summary>
        [TestMethod]
        public async System.Threading.Tasks.Task CategoryDeleteDeletes()
        {
            //Arrange
            Categories categoriesTest = new Categories() { CategoryName = "foto", Description = "foto, video" };
            await _categoriesControllerTest.Create(categoriesTest);
            int expected = db.Categories.Count() - 1;

            //Act
            await _categoriesControllerTest.DeleteConfirmed(categoriesTest.CategoryID);
            int actual = db.Categories.Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        
        /// <summary>
        /// Tests if edit works
        /// </summary>
        [TestMethod]
        public async Task CategoryEditEdits()
        {
            //Arrange
            Categories categoriesTest = new Categories() { CategoryName = "foto" };
            await _categoriesControllerTest.Create(categoriesTest);
            db.Entry(categoriesTest).State = System.Data.Entity.EntityState.Added;

            var expectedCategory = db.Categories.Find(categoriesTest.CategoryID);

            db.Dispose();
            categoriesTest.CategoryName = "video";
            db = new NorthwindDatabase();

            //Act
            await _categoriesControllerTest.Edit(categoriesTest);
            db.Entry(categoriesTest).State = System.Data.Entity.EntityState.Modified;
            var actualCategory = db.Categories.Find(categoriesTest.CategoryID);
        
            //Assert
            Assert.AreEqual(expectedCategory, actualCategory);


            var category = db.Categories.Where(c => (c.CategoryName == "video") || (c.CategoryName == "foto"));
            db.Categories.RemoveRange(category);
            db.SaveChanges();
        }


        /// <summary>
        /// Unit test for json response to fill dinamic datatable
        /// </summary>
        [TestMethod]
        public void CategoryJsonTableFill()
        {
            //Arrange
            var controller = new CategoriesController();
            var db = new NorthwindDatabase();
            var categoryCount = db.Categories.Count();


            //Act
            var jsonData = controller.JsonTableFill("").Data as IQueryable<CategoriesData>;

            //Assert
            Assert.AreEqual(db.Categories.Count(), jsonData.Count());

            db.Dispose();
        }
    }
}
