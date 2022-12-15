using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WebApiClient.Exceptions;
using WebAppMVC.ViewModels;


namespace WebAppMVC.ActionFilters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private ILoggerManager _logger;

        public ExceptionFilter(ILoggerManager logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext filterContext)
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

                //Generic exception
                //...
                filterContext.ExceptionHandled = true;
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };

                //Custom exceptions
                //...
                if(filterContext.Exception is WrongLoginException)
                {
                    var viewData = new ViewDataDictionary<LoginVM>(new EmptyModelMetadataProvider(), filterContext.ModelState);
                    viewData = new ViewDataDictionary<LoginVM>(viewData, new LoginVM { ErrorMessage = "Wrong email or password!" });
                    filterContext.Result = new ViewResult()
                    {
                        ViewName = "Login",
                        ViewData = viewData
                    };
                }
                if (filterContext.Exception is ProductOutOfStockException)
                {
                    filterContext.Result = new ViewResult()
                    {
                        ViewName = "ErrorOutOfStock",
                    };
                }
            }

        }
    }

}

