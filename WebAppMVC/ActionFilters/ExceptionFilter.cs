using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic;

namespace WebAppMVC.ActionFilters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        private ILoggerManager _logger;

        public ExceptionFilter(ILoggerManager logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var exceptionMessage = filterContext.Exception.Message;
                var stackTrace = filterContext.Exception.StackTrace;
                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var actionName = filterContext.RouteData.Values["action"].ToString();

                string Message = "Date :" + DateTime.Now.ToString() + ", Controller: " + controllerName + ", Action:" + actionName +
                                 "Error Message : " + exceptionMessage
                                + Environment.NewLine + "Stack Trace : " + stackTrace;

                _logger.LogInfo(Message);

                //saving the data in a text file called Log.txt
                //You can also save this in a dabase
                //File.AppendAllText(HttpContext.Current.Server.MapPath("~/Log/Log.txt"), Message);

                filterContext.ExceptionHandled = true;
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
            }

        }
    }

}

