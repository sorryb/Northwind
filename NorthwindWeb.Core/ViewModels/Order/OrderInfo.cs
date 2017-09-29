namespace NorthwindWeb.Core.ViewModels.Order
{   /// <summary>
    /// The class used to store, join between the Orders, Customers and Shippers
    /// </summary>
    public class OrderInfo
    {   
        /// <summary>
        /// Order id
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// Order date
        /// </summary>
        public string OrderDate { get; set; }
        /// <summary>
        /// Name Company
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Name Shipper
        /// </summary>
        public string ShipperName { get; set; }
    }
}