using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindWeb.Controllers;
using NorthwindWeb.Models;
using System.Web.Mvc;
using System.Linq;
using NorthwindWeb.Context;

namespace UnitTestNorthwindWeb
{
    [TestClass]
    public class EmployeesTest
    {
        //Arrange
        EmployeesController _EmployeesControllerUnderTest = new EmployeesController();
        NorthwindDatabase _db = new NorthwindDatabase();


        /// <summary>
        /// Tests if Create returns View.
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
        /// Tests if Create inserts into database.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task EmployeesCreateCreatesAsync()
        {
            //Arrange
            Employees employeeTest = new Employees() { EmployeeID = 1000, LastName = "test", FirstName = "test" };

            //Act
            var expected = _db.Employees.Count() + 1;
            await _EmployeesControllerUnderTest.Create(employeeTest);
            var actual = _db.Employees.Count();

            //Assert
            Assert.AreEqual(expected, actual);

            DeleteTestFromDb();
        }

        /// <summary>
        /// Tests if Delete returns View
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task EmployeesDeleteReturnsViewAsync()
        {
            //Arrange
            Employees employeeTest = new Employees() { LastName = "test", FirstName = "test" };
            await _EmployeesControllerUnderTest.Create(employeeTest);

            //Act
            var result = _EmployeesControllerUnderTest.Delete(employeeTest.EmployeeID);

            //Assert
            Assert.IsNotNull(result);





            DeleteTestFromDb();
        }

        /// <summary>
        /// Tests if Delete deletes.
        /// </summary>
        [TestMethod]
        public async System.Threading.Tasks.Task EmployeesDeleteDeletesAsync()
        {
            //Arrange
            Employees employeeTest = new Employees() { LastName = "test", FirstName = "test" };
            await _EmployeesControllerUnderTest.Create(employeeTest);
            int expected = _db.Employees.Count() - 1;

            //Act
            await _EmployeesControllerUnderTest.DeleteConfirmed(employeeTest.EmployeeID);
            int actual = _db.Employees.Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests if Details returns View
        /// </summary>
        [TestMethod]
        public async System.Threading.Tasks.Task EmployeesDetailsReturnsViewAsync()
        {
            //Arrange
            Employees employeeTest = new Employees() { LastName = "test", FirstName = "test" };
            await _EmployeesControllerUnderTest.Create(employeeTest);
            //Act
            var result = _EmployeesControllerUnderTest.Details(employeeTest.EmployeeID);
            //Assert
            Assert.IsNotNull(result);

            DeleteTestFromDb();
        }

        /// <summary>
        /// Tests if Edit returns View
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task EmployeesEditReturnsViewAsync()
        {
            //Arrange
            Employees employeeTest = new Employees() { FirstName = "test", LastName = "test" };
            await _EmployeesControllerUnderTest.Create(employeeTest);

            //Act
            var result = _EmployeesControllerUnderTest.Edit(employeeTest.EmployeeID);

            //Assert
            Assert.IsNotNull(result);



            DeleteTestFromDb();

        }

        /// <summary>
        /// Tests if Edit edits.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task EmployeesEditEditsAsync()
        {
            //Arrange
            Employees employeeTest = new Employees() { LastName = "test1", FirstName = "test1" };
            await _EmployeesControllerUnderTest.Create(employeeTest);
            _db.Entry(employeeTest).State = System.Data.Entity.EntityState.Added;

            var expectedEmployee = _db.Employees.Find(employeeTest.EmployeeID);

            _db.Dispose();
            employeeTest.LastName = "test2";
            employeeTest.FirstName = "test2";
            _db = new NorthwindDatabase();

            //Act
            await _EmployeesControllerUnderTest.Edit(employeeTest);
            _db.Entry(employeeTest).State = System.Data.Entity.EntityState.Modified;
            var actualEmployee = _db.Employees.Find(employeeTest.EmployeeID);

            //Assert
            Assert.AreEqual(expectedEmployee, actualEmployee);

            DeleteTestFromDb();
        }

        /// <summary>
        /// Tests if index returns view
        /// </summary>
        [TestMethod]
        public void EmployeesIndexReturnsView()
        {
            //Arrange

            //Act
            var result = _EmployeesControllerUnderTest.Index();

            //Assert
            Assert.IsNotNull(result);
        }












        private void DeleteTestFromDb()
        {
            var employees = _db.Employees.Where(e => e.LastName.Contains("test"));
            _db.Employees.RemoveRange(employees);
            _db.SaveChanges();
        }
    }
}
