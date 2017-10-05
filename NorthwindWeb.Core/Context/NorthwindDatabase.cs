using Microsoft.EntityFrameworkCore;
using NorthwindWeb.Core.Models;
using System;

namespace NorthwindWeb.Core.Context
{


    /// <summary>
    /// Context for northwind database.
    /// </summary>
    public partial class NorthwindDatabase : DbContext
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public NorthwindDatabase(DbContextOptions<NorthwindDatabase> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source=.\\SQLExpress;initial catalog=NorthwindEF;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
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
        /// Context for CustomerCustomerDemo table in northwind database
        /// </summary>
        public virtual DbSet<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
        /// <summary>
        /// Context for EmployeeTerritories table in northwind database
        /// </summary>
        public virtual DbSet<EmployeeTerritories> EmployeeTerritories { get; set; }

        /// <summary>
        /// Build Information of Northwind DataBase
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CustomerDemographics>()
                .Property(e => e.CustomerTypeID)
                .HasColumnType("NCHAR(10)");

            modelBuilder.Entity<CustomerCustomerDemo>()
                .Property(e => e.CustomerTypeID)
                .HasColumnType("NCHAR(10)");

            modelBuilder.Entity<Customers>()
                .Property(e => e.CustomerID)
                .HasColumnType("NCHAR(5)");

            modelBuilder.Entity<CustomerCustomerDemo>()
                .Property(e => e.CustomerID)
                .HasColumnType("NCHAR(5)");

            modelBuilder.Entity<CustomerCustomerDemo>()
                .HasKey(e => new { e.CustomerTypeID, e.CustomerID });

            modelBuilder.Entity<CustomerCustomerDemo>()
                .HasOne(e => e.Customers)
                .WithMany(c => c.CustomerCustomerDemo)
                .HasForeignKey(d => d.CustomerID);

            modelBuilder.Entity<CustomerCustomerDemo>()
                .HasOne(bc => bc.CustomerDemographics)
                .WithMany(b => b.CustomerCustomerDemo)
                .HasForeignKey(bc => bc.CustomerTypeID);

            modelBuilder.Entity<EmployeeTerritories>()
                .HasKey(e => new { e.TerritoryID, e.EmployeeID });

            modelBuilder.Entity<Employees>()
                .HasMany(e => e.Employees1)
                .WithOne(e => e.Employee1)
                .HasForeignKey(e => e.ReportsTo);

            modelBuilder.Entity<EmployeeTerritories>()
                .HasOne(e => e.Employees)
                .WithMany(c => c.EmployeeTerritories)
                .HasForeignKey(d => d.EmployeeID);

            modelBuilder.Entity<EmployeeTerritories>()
                .HasOne(bc => bc.Territories)
                .WithMany(b => b.EmployeeTerritories)
                .HasForeignKey(bc => bc.TerritoryID);

            modelBuilder.Entity<Order_Details>()
                .Property(e => e.UnitPrice)
                .HasColumnType("decimal(19, 4)");

            modelBuilder.Entity<Order_Details>()
                .Property(o => o.OrderID)
                .IsRequired();

            modelBuilder.Entity<Order_Details>()
                .Property(e => e.ProductID)
                .IsRequired();

            modelBuilder.Entity<Order_Details>()
                .HasKey(o => new { o.OrderID, o.ProductID });

            modelBuilder.Entity<Orders>()
                .Property(e => e.CustomerID)
                .HasColumnType("NCHAR(5)");

            modelBuilder.Entity<Orders>()
                .Property(e => e.Freight)
                .HasColumnType("decimal(19, 4)");

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.Order_Details);

            modelBuilder.Entity<Products>()
                .Property(e => e.UnitPrice)
                .HasColumnType("decimal(19, 4)");

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Order_Details);

            modelBuilder.Entity<Region>()
                .Property(e => e.RegionDescription)
                .HasColumnType("NCHAR(50)");

            modelBuilder.Entity<Region>()
                .HasMany(e => e.Territories);

            modelBuilder.Entity<Shippers>()
                .HasMany(e => e.Orders);

            modelBuilder.Entity<Territories>()
                .Property(e => e.TerritoryDescription)
                .HasColumnType("NCHAR(50)");
            

            modelBuilder.Entity<Persons>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Persons>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Persons>()
                .HasKey(p => new { p.ID, p.LastName });

            modelBuilder.Entity<ShopCarts>()
                .Property(e => e.Quantity)
                .IsRequired();

            modelBuilder.Entity<ShopCarts>()
                .HasKey(s => new { s.UserName, s.ProductID });
        }
    }
}