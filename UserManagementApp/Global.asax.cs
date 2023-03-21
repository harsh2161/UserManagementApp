using System.Web.Mvc;
using System.Web.Routing;
using UserManagementApp.App_Start;
using UserManagementApp.ExceptionHandler;

namespace UserManagementApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalFilters.Filters.Add(new CustomExceptionHandler());
        }
    }
}
