using AuthenticationDomain.Models;
using System.Net;

namespace AuthenticationService.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (AuthenticationDomain.ValidationException ex)
            {
                await HandleExceptionAsync
                    (httpContext, ex, ex.StatusCode, ex.Message);
            }
            catch (ApplicationException ex)
            {
                await HandleExceptionAsync
                    (httpContext, ex, HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync
                    (httpContext, ex, HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, HttpStatusCode statusCode, string message)
        {
            _logger.LogError(message);
            HttpResponse responce = httpContext.Response;
            responce.ContentType = "application/json";
            responce.StatusCode = (int)statusCode;

            var errorResponse = new ResponseApi<object>(
                response: new
            {
                StatusCode = (int)statusCode,
                ErrorStr = message
            },
            result: false);

            await responce.WriteAsJsonAsync(errorResponse);
        }
    }
}
