namespace NorthwindWeb.Core.ViewModels.Order
{   /// <summary>
    /// The class used to store, join between the Products and Categories
    /// </summary>
    public class ProductCategory
    {   
        /// <summary>
        /// Name Product
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Name Category
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// Units In Stock
        /// </summary>
        public short? UnitsInStock { get; set; }
    }
}