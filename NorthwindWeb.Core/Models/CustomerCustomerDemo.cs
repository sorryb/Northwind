using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindWeb.Core.Models
{
    public class CustomerCustomerDemo
    {
        public CustomerCustomerDemo()
        {
        }

        /// <summary>
        /// The id of the customer.
        /// </summary>
        [Key]
        [StringLength(10)]
        public string CustomerTypeID { get; set; }

        /// <summary>
        /// The id of the customer.
        /// </summary>
        [Key]
        [Required(ErrorMessage = "ID Client este obligatoriu.")]
        [StringLength(5)]
        public string CustomerID { get; set; }

        /// <summary>
        /// The customers with demographics.
        /// </summary>
        public virtual Customers Customers { get; set; }

        /// <summary>
        /// The demography of the customer.
        /// </summary>
        public virtual CustomerDemographics CustomerDemographics { get; set; }

    }
}
