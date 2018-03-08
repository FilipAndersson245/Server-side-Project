using System.Web.Mvc;
using System.Web.Routing;

namespace ServerSide_Project
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Service.Configuration.AutoMapperConfig.Configure();
        }
    }
}