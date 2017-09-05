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
        NorthwindModel _db = new NorthwindModel();

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
        public async System.Threading.Tasks.Task CreateCreatesAsync()
        {
            //Arrange
            Employees employeeTest = new Employees() { EmployeeID = 1000, LastName = "test", FirstName = "test" };

            //Act
            var expected = _db.Employees.Count() + 1;
            await _EmployeesControllerUnderTest.Create(employeeTest);
            var actual = _db.Employees.Count();
            var employees = _db.Employees.Where(e => e.LastName == employeeTest.LastName && e.FirstName == employeeTest.FirstName);

            //Assert
            Assert.AreEqual(expected, actual);

            _db.Employees.RemoveRange(employees);
            _db.SaveChanges();
        }
        /// <summary>
        /// Tests if delete returns view
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async System.Threading.Tasks.Task DeleteReturnsViewAsync()
        {
            //Arrange
            Employees employeeTest = new Employees() { LastName = "test", FirstName = "test" };
            await _EmployeesControllerUnderTest.Create(employeeTest);

            //Act
            var result = _EmployeesControllerUnderTest.Delete(employeeTest.EmployeeID);

            //Assert
            Assert.IsNotNull(result);





            var employees = _db.Employees.Where(e => e.LastName == employeeTest.LastName && e.FirstName == employeeTest.FirstName);
            _db.Employees.RemoveRange(employees);
            _db.SaveChanges();
        }
        /// <summary>
        /// Tests if delete deletes
        /// </summary>
        [TestMethod]
        public async System.Threading.Tasks.Task DeleteDeletesAsync()
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
        [TestMethod]
        public async System.Threading.Tasks.Task EditEditsAsync()
        {
            //Arrange
            Employees employeeTest = new Employees() { LastName = "test1", FirstName = "test1" };
            await _EmployeesControllerUnderTest.Create(employeeTest);
            _db.Entry(employeeTest).State = System.Data.Entity.EntityState.Added;

            var expectedEmployee = _db.Employees.Find(employeeTest.EmployeeID);

            _db.Dispose();
            employeeTest.LastName = "test2";
            employeeTest.FirstName = "test2";
            _db = new NorthwindModel();

            //Act
            await _EmployeesControllerUnderTest.Edit(employeeTest);
            _db.Entry(employeeTest).State = System.Data.Entity.EntityState.Modified;
            var actualEmployee = _db.Employees.Find(employeeTest.EmployeeID);

            //Assert
            Assert.AreEqual(expectedEmployee, actualEmployee);


            var employees = _db.Employees.Where(e => (e.LastName == "test1" && e.FirstName == "test1") || (e.LastName == "test2" && e.LastName == "test2"));
            _db.Employees.RemoveRange(employees);
            _db.SaveChanges();
        }
    }
}
