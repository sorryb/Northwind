using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Web.Mvc;
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
        /// Tests if create returns view.
        /// </summary>
        [TestMethod]
        public void CreateReturnsView()
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
        public async System.Threading.Tasks.Task CreateCategory()
        {
            //Arrange
            Categories CategoriesTest = new Categories() {CategoryName = "foto", Description = "foto, video" };
            //Act
            var expected = db.Categories.Count() + 1;
            await _CategoriesControllerUnderTest.Create(CategoriesTest);
            var actual = db.Categories.Count();
            var category = db.Categories.Where(e => e.CategoryName == CategoriesTest.CategoryName && e.Description == CategoriesTest.Description);
            
            //Assert
            Assert.AreEqual(expected, actual);

            db.Categories.RemoveRange(category);
            db.SaveChanges();

        }

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
    }
}
