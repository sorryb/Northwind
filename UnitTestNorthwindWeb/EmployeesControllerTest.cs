using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Linq;

namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class EmployeesTest
    {
        //Arrange
        EmployeesController _EmployeesControllerUnderTest = new EmployeesController();
        NorthwindModel db = new NorthwindModel();

        /// <summary>
        /// Tests if create returns view.
        /// </summary>
        [TestMethod]
        public void CreateReturnsView()
        {
            //Arrange

            //Act
            var result = _EmployeesControllerUnderTest.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Tests if create inserts into database.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task CreateCreates()
        {
            //Arrange
            Employees EmployeeTest = new Employees() { LastName = "test", FirstName = "test" };
            //Act
            var expected = db.Employees.Count()+1;
            await _EmployeesControllerUnderTest.Create(EmployeeTest);
            var actual = db.Employees.Count();
            var employees = db.Employees.Where(e => e.LastName == EmployeeTest.LastName && e.FirstName == EmployeeTest.FirstName);
            //Assert
            Assert.AreEqual(expected,actual);
            


            db.Employees.RemoveRange(employees);
            db.SaveChanges();
        }
    }
}
