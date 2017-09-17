using System.Data.Entity;
using NorthwindWeb.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NorthwindWeb.Context
{
    /// <summary>
    /// Context class used for Microsoft identity users database.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Default constructor. Initialises the datababase based on the IdentityDatabaseInitializer class.
        /// </summary>
        public ApplicationDbContext()
            : base("NorthwindDatabaseConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new IdentityDatabaseInitializer());
        }

        /// <summary>
        /// Returns a new instance of this class.
        /// </summary>
        /// <returns>new ApplicationDbContext</returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /// <summary>
        /// When model start to be build.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");


        }
    }
}