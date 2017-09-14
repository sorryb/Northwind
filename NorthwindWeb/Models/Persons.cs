namespace NorthwindWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    /// <summary>
    /// The entity that contains information about persons, located in database.
    /// </summary>
    [Table("Persons")]
    public partial class Persons
    {

        /// <summary>
        /// The ID through which we find the person.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        /// <summary>
        /// The LastName through which we find the person.
        /// </summary>
        [Key]
        [Required(ErrorMessage = "Introduceti-va numele")]
        [Column(Order = 1)]
        [StringLength(255)]
        public string LastName { get; set; }

        /// <summary>
        /// The FirstName through which we find the person.
        /// </summary>
        [StringLength(255)]
        public string FirstName { get; set; }

        /// <summary>
        /// The Email through which we find the person.
        /// </summary>
        [Required(ErrorMessage = "Email-ul nu a fost introdus corect")]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// The Comment what belongs to the person.
        /// </summary>
        [Required(ErrorMessage = "Va rugam sa va spuneti parerea")]
        [StringLength(255)]
        public string Comment { get; set; }

    }
}
