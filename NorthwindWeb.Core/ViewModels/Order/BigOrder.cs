namespace NorthwindWeb.Core.ViewModels.Order
{   /// <summary>
    /// Order with most products
    /// </summary>
    public class BigOrder
    {
        /// <summary>
        /// Order id
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// Number Of Product
        /// </summary>
        public int NumberOfProduct { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public BigOrder() { }
    }
}