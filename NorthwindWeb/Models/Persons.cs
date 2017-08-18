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

        public string Nume { get; set; }

        public string Email { get; set; }

        public string Comentariu { get; set; }
    }
}
