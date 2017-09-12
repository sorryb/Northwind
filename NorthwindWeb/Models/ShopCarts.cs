namespace NorthwindWeb.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Shop Carts")]
    public partial class ShopCarts
    {
        [Key]
        [StringLength(256)]
        [Column(Order = 1)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ProductID { get; set; }
        
        public int Quantity { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}