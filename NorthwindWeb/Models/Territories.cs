namespace NorthwindWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 
    /// </summary>
    public partial class Territories
    {
        /// <summary>
        /// 
        /// </summary>
        public Territories()
        {
            Employees = new HashSet<Employees>();
        }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        [StringLength(20)]
        public string TerritoryID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(50)]
        public string TerritoryDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RegionID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Region Region { get; set; }

   
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
