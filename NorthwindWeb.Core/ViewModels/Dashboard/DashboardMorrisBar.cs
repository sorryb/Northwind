namespace NorthwindWeb.Core.ViewModels.Dashboard
{/// <summary>
/// The data model for the MorrisBar graph
/// </summary>
    public class DashboardMorrisBar
    {   /// <summary>
        /// year on which sales are calculated
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// first-quarter sales
        /// </summary>
        public decimal dashboardMorrisBarColumA { get; set; }
        /// <summary>
        /// sales for the second quarter
        /// </summary>
        public decimal dashboardMorrisBarColumB { get; set; }
        /// <summary>
        /// sales for the third quarter
        /// </summary>
        public decimal dashboardMorrisBarColumC { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public DashboardMorrisBar() {}
    }
}