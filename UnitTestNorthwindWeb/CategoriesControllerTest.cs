using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Linq;


namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class CategoriesControllerTest
    {
        //Arrange
        CategoriesController _CategoriesControllerUnderTest = new CategoriesController();
        NorthwindModel db = new NorthwindModel();

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
        /// Tests if create returns view.
        /// </summary>
        [TestMethod]
        public void CreateReturnsViewCategory()
        {
            //Arrange

            //Act
            var result = _CategoriesControllerUnderTest.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Tests if create inserts into database.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task CategoryCreate()
        {
            //Arrange
            Categories CategoriesTest = new Categories() {CategoryID=14,CategoryName = "foto", Description = "foto, video" };
            //Act
            var expected = db.Categories.Count() + 1;
            await _CategoriesControllerUnderTest.Create(CategoriesTest);
            var actual = db.Categories.Count();
            var category = db.Categories.Where(c => c.CategoryName == CategoriesTest.CategoryName && c.Description == CategoriesTest.Description);
            
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
        public async System.Threading.Tasks.Task CategoryDeleteReturnsViewAsync()
        {
            //Arrange
            Categories categoriesTest = new Categories() { CategoryName = "foto", Description = "foto, video" };
            await _CategoriesControllerUnderTest.Create(categoriesTest);

            //Act
            var result = _CategoriesControllerUnderTest.Delete(categoriesTest.CategoryID);

            //Assert
            Assert.IsNotNull(result);





            var category = db.Categories.Where(c => c.CategoryName == categoriesTest.CategoryName && c.Description == categoriesTest.Description);
            db.Categories.RemoveRange(category);
            db.SaveChanges();
        }

        /// <summary>
        /// Tests if delete deletes
        /// </summary>
        [TestMethod]
        public async System.Threading.Tasks.Task CategoryDeleteDeletesAsync()
        {
            //Arrange
            Categories categoriesTest = new Categories() { CategoryName = "foto", Description = "foto, video" };
            await _CategoriesControllerUnderTest.Create(categoriesTest);
            int expected = db.Categories.Count() - 1;

            //Act
            await _CategoriesControllerUnderTest.DeleteConfirmed(categoriesTest.CategoryID);
            int actual = db.Categories.Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests if edit works
        /// </summary>
        [TestMethod]
        public async System.Threading.Tasks.Task EditEditsAsync()
        {
            //Arrange
            Categories categoriesTest = new Categories() { CategoryName = "video", Description = "camere video" };
            await _CategoriesControllerUnderTest.Create(categoriesTest);
            db.Entry(categoriesTest).State = System.Data.Entity.EntityState.Added;

            var expectedCategory = db.Categories.Find(categoriesTest.CategoryID);

            db.Dispose();
            categoriesTest.CategoryName = "foto";
            categoriesTest.Description = "aparat foto";
            db = new NorthwindModel();

            //Act
            await _CategoriesControllerUnderTest.Edit(categoriesTest);
            db.Entry(categoriesTest).State = System.Data.Entity.EntityState.Modified;
            var actualCategory = db.Categories.Find(categoriesTest.CategoryID);
        
            //Assert
            Assert.AreEqual(expectedCategory, actualCategory);


            var category = db.Categories.Where(c => (c.CategoryName == "video" && c.Description == "camere video") || (c.CategoryName == "foto" && c.Description == "aparat foto"));
            db.Categories.RemoveRange(category);
            db.SaveChanges();
        }
    }
}
