using System.Web.Mvc;
using UserManagementApp.ExceptionHandler.CustomExceptions;

namespace UserManagementApp.ExceptionHandler
{
    public class CustomExceptionHandler : FilterAttribute, IExceptionFilter
    {
        private readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(CustomExceptionHandler));
        public void OnException(ExceptionContext filterContext)
        {
            if(filterContext.Exception is AuthorizationException)
            {
                Log.Error(" Catching Error of AuthorizationException");
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
            }
            else if(filterContext.Exception is ValidationException)
            {
                Log.Error(" Catching Error of ValidationException");
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
            }
            else if(filterContext.Exception is BaseUrlInvalid)
            {
                Log.Error(" Catching Error of BaseUrlInvalid");
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
            }
            else
            {
                Log.Error(" Catching Error of Unknow Error");
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
            }

            filterContext.ExceptionHandled = true;
        }
    }
}
