
using AuthenticationDomain.Models;
using AuthenticationService.Base;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace AuthenticationService.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private const string UnknowExceptionMessage = "Возникла неизвестная ошибка";

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
            catch (Exception ex)
              when (ex.Message == "db error")
            {
                await HandleExceptionAsync
                    (httpContext, ex.Message, HttpStatusCode.InternalServerError, "Database error occurred.");
            }
            catch (Exception ex)
            when (ex.Message == "this user already exists")
            {
                await HandleExceptionAsync
                    (httpContext, ex.Message, HttpStatusCode.Conflict, "This user already exists.");
            }
            catch (Exception ex)
            when (ex.Message == "incorrect password")
            {
                await HandleExceptionAsync
                    (httpContext, ex.Message, HttpStatusCode.Unauthorized, "Incorrect password.");
            }
            catch (Exception ex)
            when (ex.Message == "Creater not found")
            {
                await HandleExceptionAsync
                    (httpContext, ex.Message, HttpStatusCode.NotFound, "Creater not found.");
            }
            catch (Exception ex)
            when (ex.Message == "creater already exist")
            {
                await HandleExceptionAsync
                    (httpContext, ex.Message, HttpStatusCode.Conflict, "Creater already exists.");
            }
            catch (Exception ex)
            when (ex.Message == "Redis hash delete error")
            {
                await HandleExceptionAsync
                    (httpContext, ex.Message, HttpStatusCode.Conflict, "Redis hash delete error");
            }
            catch (Exception ex)
            when (ex.Message == "Redis key delete error")
            {
                await HandleExceptionAsync
                    (httpContext, ex.Message, HttpStatusCode.Conflict, "Redis key delete error");
            }

            catch (Exception ex)
            {
                await HandleExceptionAsync
                    (httpContext, ex.Message, HttpStatusCode.InternalServerError, "An unexpected error occurred.");
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
