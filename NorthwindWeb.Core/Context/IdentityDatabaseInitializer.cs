using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NorthwindWeb.Core.Models;
using NorthwindWeb.Core.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using System;

namespace NorthwindWeb.Context.IdentityDatabaseInitializer
{

    public static class RolesData
    {

        private static readonly string[] roles = new[] {
        "Admins",
        "Guest",
        "Customers",
        "Managers",
        "Employees"
        };

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var create = await roleManager.CreateAsync(new IdentityRole(role));

                    if (!create.Succeeded)
                    {
                        throw new Exception("Failed to create role");
                    }
                }
            }
            var result = await userManager.CreateAsync(new ApplicationUser { UserName = "admin"}, "123+Asd");
            //await AddUsersInRole(context,  northwindContext);
            context.Dispose();
        }
        //todo adauga useri in baza de date.

        //public static async Task AddUsersInRole(ApplicationDbContext context, NorthwindDatabase northwindContext)
        //{
        //    var userManager = new UserManager<ApplicationUser>(new UserStore(context));
        //    //add admins
        //    var user = new ApplicationUser();
        //    user.UserName = "admin";
        //    user.Email = "admin@gmail.com";
        //    AddUser(userManager, user, "123456");

        //    //create user for tests
        //    var testUser = new ApplicationUser();
        //    testUser.UserName = "tester";
        //    testUser.Email = "Tester_1@gmail.com";
        //    AddUser(userManager, testUser, "Tester_1");


        //    var employees = northwindContext.Employees;

        //    foreach (var itemEmployee in employees)
        //    {
        //        ApplicationUser employee = new ApplicationUser();
        //        employee.UserName = itemEmployee.FirstName + itemEmployee.LastName;
        //        employee.Email = itemEmployee.FirstName + "@gmail.com";
        //        await userManager.CreateAsync(employee, itemEmployee.FirstName + itemEmployee.LastName);

        //        var currentUser = await userManager.FindByNameAsync(itemEmployee.FirstName + itemEmployee.LastName);
        //        if (itemEmployee.ReportsTo == null)
        //        {
        //            var rolManagers = await userManager.AddToRoleAsync(currentUser, "Managers");
        //        }

        //        var rol = await userManager.AddToRoleAsync(currentUser, "Employees");

        //    }
        //}

        private static async Task AddUserAsync(UserManager<ApplicationUser> userManager, ApplicationUser user, string userPWD)
        {
            var chkUser = await userManager.CreateAsync(user, userPWD);

            //Add default User to Role Admin   
            if (chkUser.Succeeded)
            {
                var resultForAdd = userManager.AddToRoleAsync(user, "Admins");

            }
        }

    }
}