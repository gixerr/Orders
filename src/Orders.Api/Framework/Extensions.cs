using Microsoft.AspNetCore.Builder;
using Orders.Api.Middleware;

namespace Orders.Api.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UseOrdersExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ExceptionHandlerMiddleware));
    }
}
