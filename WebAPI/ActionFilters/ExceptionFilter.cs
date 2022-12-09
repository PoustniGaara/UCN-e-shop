using LoggerService;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Exceptions;


namespace WebApi.ActionFilters 
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

            _logger.LogInfo(filterContext.Exception.Message);

            ObjectResult result = new ObjectResult(new
            {
                filterContext.Exception.Message, // Or a different generic message
                filterContext.Exception.Source,
                ExceptionType = filterContext.Exception.GetType().FullName,
            })
            {
                //Generic exception
                StatusCode = (int)HttpStatusCode.ServiceUnavailable
            };

            filterContext.Result = result;

            //Custom exceptions
            if (filterContext.Exception is WrongLoginException)
            {
                result.StatusCode = (int)HttpStatusCode.Forbidden;
                filterContext.Result = result;
            }
            if (filterContext.Exception is ProductOutOfStockException)
            {
                result.StatusCode = (int)HttpStatusCode.Conflict;
                filterContext.Result = result;
            }
        }
    }

}

