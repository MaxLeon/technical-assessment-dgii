using DGII.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DGII.API.Filters;

public class ResponseFormatterFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is not null) return;

        if (context.Result is ObjectResult objectResult)
        {
            context.Result = new ObjectResult(new ApiResponse<object>(
                Success: true,
                Data: objectResult.Value,
                Error: null
            ))
            {
                StatusCode = objectResult.StatusCode ?? 200
            };
        }
    }
}
