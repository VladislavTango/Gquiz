using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace AuthenticationService.Middlewares
{
    public class ResponseFilter : ActionFilterAttribute
    {
        public void OnActionExecuted(ActionExecutedContext _)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult
                && context.HttpContext.Response.StatusCode == StatusCodes.Status200OK)
            {
                var value = objectResult.Value;
                objectResult.Value = ResponseApi<object>.Success(value);
            }
        }
    }
}
