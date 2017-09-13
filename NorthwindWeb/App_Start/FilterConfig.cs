using System.Web;
using System.Web.Mvc;

namespace NorthwindWeb
{
    /// <summary>
    /// Filters.
    /// </summary>
    public class FilterConfig
    {

        /// <summary>
        /// Register filters.
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
