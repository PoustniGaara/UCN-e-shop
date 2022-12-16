using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;

namespace WebApi.ActionFilters
{
    public class ValidationFilter : IActionFilter
        {
            public void OnActionExecuting(ActionExecutingContext context)
            {

                var param = context.ActionArguments.SingleOrDefault(p => p.Value is IEntity);  
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
