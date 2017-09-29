namespace NorthwindWeb.Core.ViewModels.Dashboard
{   /// <summary>
    /// MatchesFound format in dashboard search model
    /// </summary>
    public class LocateSearch
    {   /// <summary>
        /// number match
        /// </summary>
        public int Position { get; set; }
        /// <summary>
        /// locating matching matches
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// name of the column in which it was found
        /// </summary>
        public string WhereFound { get; set; }
        /// <summary>
        /// controller that generates it
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public LocateSearch() { }
    }
}