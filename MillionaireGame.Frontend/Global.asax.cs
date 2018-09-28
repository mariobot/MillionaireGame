using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MillionaireGame.Frontend
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            App_Start.BundleConfig.RegisterBundles(BundleTable.Bundles);           
        }
    }
}
