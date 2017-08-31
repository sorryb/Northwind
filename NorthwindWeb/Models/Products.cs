namespace NorthwindWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {

        public Products()
        {
            Order_Details = new HashSet<Order_Details>();
        }
        [Key]
        public int ProductID { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; }
        
        public int SupplierID { get; set; }

        public int CategoryID { get; set; }

        [StringLength(20)]
        public string QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }


        public virtual Categories Category { get; set; }

        public virtual ICollection<Order_Details> Order_Details { get; set; }

        public virtual Suppliers Supplier { get; set; }
    }
}
