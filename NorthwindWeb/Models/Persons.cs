namespace NorthwindWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Persons")]
    public partial class Persons
    {
        [Key]
        public int ID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public int Age { get; set; }
    }
}
