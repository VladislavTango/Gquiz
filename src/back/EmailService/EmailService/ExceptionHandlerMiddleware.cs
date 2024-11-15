using EmailDomain.Models;
using System.Net;

namespace EmailService
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

        public async Task InvokeAsync(HttpContext httpContent)
        {
            try
            {
                await _next(httpContent);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync
                    (httpContent, ex.Message,
                    HttpStatusCode.InternalServerError,
                    "An unexpected error occurred.");
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, string Ex, HttpStatusCode code, string message)
        {
            _logger.LogError(Ex);
            HttpResponse responce = httpContext.Response;
            responce.ContentType = "application/json";
            responce.StatusCode = (int)code;
            ErrorModel model = new()
            {
                ErrorStr = message,
                StausCode = (int)code
            };

            string result = model.ToString();
            await responce.WriteAsJsonAsync(result);
        }
    }
}
