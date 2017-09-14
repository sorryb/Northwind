using NorthwindWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindWeb.Models.ServerClientCommunication
{
    /// <summary>
    /// Contain data that datatable need to draw corect table from server
    /// </summary>
    public class JsonDataTable
    {
        /// <summary>
        /// The draw counter that this object is a response to - from the draw parameter 
        /// sent as part of the data request.
        /// </summary>
        public int Draw { get; set; }
        /// <summary>
        /// Total records, before filtering (i.e. the total number of records in the database)
        /// </summary>
        public int RecordsTotal { get; set; }
        /// <summary>
        /// Total records, after filtering
        /// </summary>
        public int RecordsFiltered { get; set; }
        /// <summary>
        /// The data to be displayed in the table. 
        /// This is an array of data source objects, one for each row, which will be used by DataTables.
        /// </summary>
        public IQueryable Data { get; set; }
        /// <summary>
        /// Optional: If an error occurs during the running of the server-side processing script, 
        /// you can inform the user of this error by passing back the error message to be displayed using this parameter.
        /// </summary>
        public string Error { get; set; }

    }


    public class JsonDataTableUserList
    {
        /// <summary>
        /// The draw counter that this object is a response to - from the draw parameter 
        /// sent as part of the data request.
        /// </summary>
        public int Draw { get; set; }
        /// <summary>
        /// Total records, before filtering (i.e. the total number of records in the database)
        /// </summary>
        public int RecordsTotal { get; set; }
        /// <summary>
        /// Total records, after filtering
        /// </summary>
        public int RecordsFiltered { get; set; }
        /// <summary>
        /// The data to be displayed in the table. 
        /// This is an array of data source objects, one for each row, which will be used by DataTables.
        /// </summary>
        public List<UserInfoViewModel> Data { get; set; }
        /// <summary>
        /// Optional: If an error occurs during the running of the server-side processing script, 
        /// you can inform the user of this error by passing back the error message to be displayed using this parameter.
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// Optional:Only used if want users assigned to a role
        /// </summary>
        public string RoleName { get; set; }
    }

    public class JsonDataTableRoleList
    {
        /// <summary>
        /// The draw counter that this object is a response to - from the draw parameter 
        /// sent as part of the data request.
        /// </summary>
        public int Draw { get; set; }
        /// <summary>
        /// Total records, before filtering (i.e. the total number of records in the database)
        /// </summary>
        public int RecordsTotal { get; set; }
        /// <summary>
        /// Total records, after filtering
        /// </summary>
        public int RecordsFiltered { get; set; }
        /// <summary>
        /// The data to be displayed in the table. 
        /// This is an array of data source objects, one for each row, which will be used by DataTables.
        /// </summary>
        public List<RoleInfoViewModel> Data { get; set; }
        /// <summary>
        /// Optional: If an error occurs during the running of the server-side processing script, 
        /// you can inform the user of this error by passing back the error message to be displayed using this parameter.
        /// </summary>
        public string Error { get; set; }

    }

    //public class 
}