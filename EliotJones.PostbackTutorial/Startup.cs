using EliotJones.PostbackTutorial;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace EliotJones.PostbackTutorial
{
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}