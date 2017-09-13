using System.Web.Mvc;
using System.Web.Routing;
using log4net;
using System.Reflection;
using System.Diagnostics;

namespace NorthwindWeb
{
    /// <summary>
    /// Mvc application.
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.
        private static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Start IIS application.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //start configurator for Log4Net from web.config
            log4net.Config.XmlConfigurator.Configure();

            // Create the source, if it does not already exist.
            if (!EventLog.SourceExists("Northwind"))
            {
                //An event log source should not be created and immediately used.
                //There is a latency time to enable the source, it should be created
                //prior to executing the application that uses the source.
                //Execute this sample a second time to use the new source.
                EventLog.CreateEventSource("Northwind", "Application");
 

                // The source is created.  Exit the application to allow it to be registered.
                return;
            }
        }

        /// <summary>
        /// Write global errors.
        /// </summary>
        protected void Application_Error()
        {
            Log.Fatal("An exception occurred in NorthwindWeb site! ", this.Server.GetLastError());
        }
    }
}
