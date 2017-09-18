using System.Data.Entity;
using NorthwindWeb.Models;

namespace NorthwindWeb.Context
{


    /// <summary>
    /// Context for northwind database.
    /// </summary>
    public partial class NorthwindDatabase : DbContext
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public NorthwindDatabase()
            : base("name=NorthwindDatabaseConnection") // name in web.config of Connection string.
        {
            Database.SetInitializer(new NorthwindDatabaseInitializer());
        }


        /// <summary>
        /// Context for Categories table in northwind database
        /// </summary>
        public virtual DbSet<Categories> Categories { get; set; }
        /// <summary>
        /// Context for CustomerDemographics table in northwind database
        /// </summary>
        public virtual DbSet<CustomerDemographics> CustomerDemographics { get; set; }
        /// <summary>
        /// Context for Customers table in northwind database
        /// </summary>
        public virtual DbSet<Customers> Customers { get; set; }
        /// <summary>
        /// Context for Employees table in northwind database
        /// </summary>
        public virtual DbSet<Employees> Employees { get; set; }
        /// <summary>
        /// Context for Order_Details table in northwind database
        /// </summary>
        public virtual DbSet<Order_Details> Order_Details { get; set; }
        /// <summary>
        /// Context for Orders table in northwind database
        /// </summary>
        public virtual DbSet<Orders> Orders { get; set; }
        /// <summary>
        /// Context for Persons table in northwind database
        /// </summary>
        public virtual DbSet<Persons> Persons { get; set; }
        /// <summary>
        /// Context for Products table in northwind database
        /// </summary>
        public virtual DbSet<Products> Products { get; set; }
        /// <summary>
        /// Context for Regions table in northwind database
        /// </summary>
        public virtual DbSet<Region> Regions { get; set; }
        /// <summary>
        /// Context for Shippers table in northwind database
        /// </summary>
        public virtual DbSet<Shippers> Shippers { get; set; }
        /// <summary>
        /// Context for Suppliers table in northwind database
        /// </summary>
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        /// <summary>
        /// Context for Territories table in northwind database
        /// </summary>
        public virtual DbSet<Territories> Territories { get; set; }
        /// <summary>
        /// Context for ShopCart table in northwind database
        /// </summary>
        public virtual DbSet<ShopCarts> ShopCart { get; set; }


        /// <summary>
        /// Build Information of Northwind DataBase
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CustomerDemographics>()
                .Property(e => e.CustomerTypeID)
                .IsFixedLength();

            modelBuilder.Entity<CustomerDemographics>()
                .HasMany(e => e.Customers)
                .WithMany(e => e.CustomerDemographics)
                .Map(m => m.ToTable("CustomerCustomerDemo").MapLeftKey("CustomerTypeID").MapRightKey("CustomerID"));

            modelBuilder.Entity<Customers>()
                .Property(e => e.CustomerID)
                .IsFixedLength();

            modelBuilder.Entity<Employees>()
                .HasMany(e => e.Employees1)
                .WithOptional(e => e.Employee1)
                .HasForeignKey(e => e.ReportsTo);

            modelBuilder.Entity<Employees>()
                .HasMany(e => e.Territories)
                .WithMany(e => e.Employees)
                .Map(m => m.ToTable("EmployeeTerritories").MapLeftKey("EmployeeID").MapRightKey("TerritoryID"));

            modelBuilder.Entity<Order_Details>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .Property(e => e.CustomerID)
                .IsFixedLength();

            modelBuilder.Entity<Orders>()
                .Property(e => e.Freight)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.Order_Details)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Order_Details)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Region>()
                .Property(e => e.RegionDescription)
                .IsFixedLength();

            modelBuilder.Entity<Region>()
                .HasMany(e => e.Territories)
                .WithRequired(e => e.Region)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shippers>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Shipper)
                .HasForeignKey(e => e.ShipVia);

            modelBuilder.Entity<Territories>()
                .Property(e => e.TerritoryDescription)
                .IsFixedLength();

            modelBuilder.Entity<Persons>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Persons>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<ShopCarts>()
                .HasRequired(a => a.Products)
                .WithMany()
                .HasForeignKey(a => a.ProductID);

            modelBuilder.Entity<ShopCarts>()
                .Property(e => e.UserName)
                .IsRequired();

            modelBuilder.Entity<ShopCarts>()
                .Property(e => e.Quantity)
                .IsRequired();
        }
    }
}
