namespace NorthwindWeb.Core.Models.ServerClientCommunication
{
    /// <summary>
    /// Contain data of region that datatable need to draw a table.
    /// </summary>
    public class RegionData
    {
        /// <summary>
        /// The ID of region.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The description of region.
        /// </summary>
        public string RegionDescription { get; set; }
    }
}