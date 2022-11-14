using LoggerService;
using System.Net;
using System.Net.Http;
//using System.Web.Http.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
//using ExceptionFilterAttribute = System.Web.Http.Filters.ExceptionFilterAttribute;


namespace WebApi.ActionFilters 
{
    public class ExceptionFilter : ExceptionFilterAttribute    //ExceptionFilterAttribute
    {
        private ILoggerManager _logger;

        public ExceptionFilter(ILoggerManager logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext filterContext) //ExceptionContext filterContext
        {

            _logger.LogInfo(filterContext.Exception.Message);

            var result = new ObjectResult(new
            {
                filterContext.Exception.Message, // Or a different generic message
                filterContext.Exception.Source,
                ExceptionType = filterContext.Exception.GetType().FullName,
            })
            {
                StatusCode = (int)HttpStatusCode.ServiceUnavailable
            };

            filterContext.Result = result;

            //For custom exceptions
            //if (filterContext.Exception is Exception)
            //{
            //    filterContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            //}

        }
    }

}

