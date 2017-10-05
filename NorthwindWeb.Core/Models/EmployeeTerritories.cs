namespace NorthwindWeb.Core.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Table that hold n to n relation between Employee and Territories
    /// </summary>
    public partial class EmployeeTerritories
    {
        public EmployeeTerritories()
        {
        }

        /// <summary>
        /// The ID through which we find the territory.
        /// </summary>
        [Key]
        [StringLength(20)]
        public string TerritoryID { get; set; }

        /// <summary>
        /// The id of the employee.
        /// </summary>
        [Key]
        public int EmployeeID { get; set; }

        /// <summary>
        /// The employees where work in a territory.
        /// </summary>
        public virtual Employees Employees { get; set; }


        /// <summary>
        /// The territories where the employee works.
        /// </summary>
        public virtual Territories Territories { get; set; }
    }
}
