using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NorthwindWeb.ViewModels.Dashboard
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