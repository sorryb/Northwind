using System.Runtime.Serialization;

namespace NorthwindWeb.Core.ViewModels.Dashboard
{
    [DataContract]
    public class DashboardGraph1
    {
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public decimal Sales { get; set; }
        public DashboardGraph1() {}
    }
}