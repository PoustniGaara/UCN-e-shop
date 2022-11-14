using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace WebApi.FilterAttributes
{
        //public class ValidateModelExists<T> : IActionFilter
        //{
            
        //    public void OnActionExecuting(ActionExecutingContext context)
        //    {
        //        Guid id = Guid.Empty;
        //        if (context.ActionArguments.ContainsKey("id"))
        //        {
        //            id = (Guid)context.ActionArguments["id"];
        //        }
        //        else
        //        {
        //            context.Result = new BadRequestObjectResult("Bad id parameter");
        //            return;
        //        }
        //        var entity = _context.Set<T>().SingleOrDefault(x => x.Id.Equals(id));
        //        if (entity == null)
        //        {
        //            context.Result = new NotFoundResult();
        //        }
        //        else
        //        {
        //            context.HttpContext.Items.Add("entity", entity);
        //        }
        //    }
        //    public void OnActionExecuted(ActionExecutedContext context)
        //    {
        //    }
        //}
}
