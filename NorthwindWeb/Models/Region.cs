namespace NorthwindWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// The entity that contains information about regions, located in database.
    /// </summary>
    [Table("Region")]
    public partial class Region
    {

        /// <summary>
        /// Default constructor. Initialises new empty instances for Territories.
        /// </summary>
        public Region()
        {
            Territories = new HashSet<Territories>();
        }

        /// <summary>
        /// The ID through which we find the Region.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RegionID { get; set; }

        /// <summary>
        /// The Description through which we find the Region.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string RegionDescription { get; set; }

        /// <summary>
        /// The region contains more territories.
        /// </summary>
        public virtual ICollection<Territories> Territories { get; set; }
    }
}
