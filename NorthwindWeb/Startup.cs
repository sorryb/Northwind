using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NorthwindWeb.Startup))]
namespace NorthwindWeb
{
    /// <summary>
    /// Start the application.
    /// </summary>
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
