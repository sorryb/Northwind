namespace NorthwindWeb.Core.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The entity that holds all the information from the CustomerDemographics table in the database.
    /// </summary>
    public partial class CustomerDemographics
    {

        /// <summary>
        /// Default constructor. Initialises new empty instances for Customers.
        /// </summary>
        public CustomerDemographics()
        {
            CustomerCustomerDemo = new HashSet<CustomerCustomerDemo>();
        }

        /// <summary>
        /// The id of the customer.
        /// </summary>
        [Key]
        [StringLength(10)]
        public string CustomerTypeID { get; set; }


        /// <summary>
        /// The description of the customer.
        /// </summary>
        [Column(TypeName = "ntext")]
        public string CustomerDesc { get; set; }

        /// <summary>
        /// The customers with demographics.
        /// </summary>
        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
    }
}
