namespace NorthwindWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// The entity that contains information about territories, located in database.
    /// </summary>
    public partial class Territories
    {
        /// <summary>
        /// Default constructor. Initialises new empty instances for Employees.
        /// </summary>
        public Territories()
        {
            Employees = new HashSet<Employees>();
        }

        /// <summary>
        /// The ID through which we find the territory.
        /// </summary>
        [Key]
        [StringLength(20)]
        public string TerritoryID { get; set; }

        /// <summary>
        /// The Description through which we find the territory.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string TerritoryDescription { get; set; }

        /// <summary>
        /// The RegionID through which we find the territory.
        /// </summary>
        public int RegionID { get; set; }

        /// <summary>
        /// The region which contains more territories.
        /// </summary>
        public virtual Region Region { get; set; }

        /// <summary>
        /// The employees where work in a territory.
        /// </summary>
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
