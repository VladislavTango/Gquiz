using CommonShared.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace CommonShared.Middlewares
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
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(httpContext, ex, ex.StatusCode, ex.Message);
            }
            catch (ApplicationException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, HttpStatusCode statusCode, string message)
        {
            _logger.LogError(message);
            var response = httpContext.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            var errorResponse = new ResponseApi<object>(
                response: new
                {
                    StatusCode = (int)statusCode,
                    ErrorStr = message
                },
                result: false
            );

            var json = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(json);
        }

    }
}
