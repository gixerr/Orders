using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Orders.Infrastructure.Exceptions;

namespace Orders.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorCode = "error";
            var statusCode = HttpStatusCode.BadRequest;

            switch (exception)
            {
                case ServiceException ex when exception is ServiceException:
                    errorCode = ex.ErrorCode;
                    break;
                case Exception ex when exception is UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case Exception ex when exception.GetType() == typeof(Exception):
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var resposne = new { code = errorCode, message = exception.Message };
            var payload = JsonConvert.SerializeObject(resposne);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);


        }
    }
}