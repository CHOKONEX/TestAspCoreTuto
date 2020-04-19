using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestAspCoreTuto.Extensions;

namespace TestAspCoreTuto.Bootstrapping.ActionFilters
{
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState.GetErrorMessages());
            }
        }
    }
}
