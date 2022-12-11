using Diary.Core.Exceptions;
using Diary.Core.Models;
using Diary.Core.Utils;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Diary.Core.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Unhandled exception
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var errorKey = Constants.InternalServerError;
            var message = exception.Message;


            if (exception is ApiException apiException)
            {
                context.Response.StatusCode = apiException.StatusCode;
                errorKey = apiException.ErrorKey;
                message = apiException.Message;
            }

            await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorModel(errorKey, exception.GetType().Name, message, context.Response.StatusCode)));
        }
    }
}