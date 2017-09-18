using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using NorthwindWeb.Context;

namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class ContactControllerTest
    {
        [TestMethod]
        public void ContactIndex()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.ContactController();

            //Act
            var result = controller.Index("succes");

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ContactCreate()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.ContactController();

            //Act
            var result = controller.Index("succes");

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ContactCreateItem()
        {
            //Arrange
            var controller = new NorthwindWeb.Controllers.ContactController();
            var db = new NorthwindDatabase();
            NorthwindWeb.Models.Persons person = new NorthwindWeb.Models.Persons()
            {
            ID = db.Persons.Count() + 1,
            FirstName = "test1223",
                LastName = "123331111",
                Email="tast@ceva.com",
                Comment="Buna!"
            };

            //Act
            var result = controller.Index(person);

            //Assert
            Assert.IsNotNull(result);

            db.Entry(person).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            db.Dispose();
        }
    }
}
