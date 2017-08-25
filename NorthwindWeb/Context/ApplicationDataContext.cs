using System.Data.Entity;
using NorthwindWeb.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NorthwindWeb.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new IdentityDatabaseInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<User>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
        //    modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        //    modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
        //    modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
        //    modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");


        //}
    }
}