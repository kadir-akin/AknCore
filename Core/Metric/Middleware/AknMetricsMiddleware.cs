using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Metric.Middleware
{
    internal class AknMetricsMiddleware
    {
        private readonly RequestDelegate _next;

        public AknMetricsMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public async Task InvokeAsync(HttpContext httpContext)
        {

           


            await _next(httpContext);
        }
    }
}
