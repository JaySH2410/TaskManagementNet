using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;

namespace TaskManagement.Middlewares
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = string.Join(" | ", context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                context.Result = new BadRequestObjectResult(
                    ApiResponse.Error("Validation Failed", errors, 400)
                );
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
                
        }
    }
}
