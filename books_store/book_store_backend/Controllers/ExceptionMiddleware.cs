using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Internal
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
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await httpContext.Response.WriteAsync(
                     new ErrorDetails(
                         httpContext.Response.StatusCode,
                         "Something went wrong.\r\n\r\n" + ex.ToString()
                     ).ToString()
                 );
            }
        }
    }
}
