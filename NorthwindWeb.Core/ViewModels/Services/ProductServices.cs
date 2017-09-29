namespace NorthwindWeb.Core.ViewModels
{
    /// <summary>
    /// Keeps all products from Services.
    /// </summary>
    public class ProductServices
    {
        /// <summary>
        /// The name of the product.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// The id of the product.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// The category of the product.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// The name of the shipper.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The quantity per unit of the product.
        /// </summary>
        public string QuantityPerUnit { get; set; }

        /// <summary>
        /// The unit price of the product.
        /// </summary>
        public decimal? UnitPrice { get; set; }

        /// <summary>
        /// The number of units available.
        /// </summary>
        public int? UnitsInStock { get; set; }

        /// <summary>
        /// The number of units asked on the order.
        /// </summary>
        public int? UnitsOnOrder { get; set; }

        /// <summary>
        /// Default constructor. Initialises new empty instance.
        /// </summary>
        public ProductServices(){}

    }
}