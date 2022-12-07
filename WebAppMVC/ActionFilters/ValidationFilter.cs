using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using WebAppMVC.ViewModels;

namespace WebAppMVC.ActionFilters
{
        public class ValidationFilter : IActionFilter
        {
            public void OnActionExecuting(ActionExecutingContext context)
            {

                var param = context.ActionArguments.SingleOrDefault();   /*Last();*/
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
