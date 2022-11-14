using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace WebApi.ActionFilters
{
    public class ValidationFilter
    {
        public class ValidationFilterAttribute : IActionFilter
        {
            public void OnActionExecuting(ActionExecutingContext context)
            {
                var param = context.ActionArguments.SingleOrDefault();
                if (param.Value == null)
                {
                    context.Result = new BadRequestObjectResult("Object is null");
                    return;
                }
                if (!context.ModelState.IsValid)
                {
                    context.Result = new UnprocessableEntityObjectResult(context.ModelState);
                }
            }
            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        }
    }
}
