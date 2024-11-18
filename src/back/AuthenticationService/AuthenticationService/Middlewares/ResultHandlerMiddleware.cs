using AuthenticationService.Base;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthenticationService.Middlewares
{
    public class ResultHandlerMiddleware : ResultFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }
    }
}
