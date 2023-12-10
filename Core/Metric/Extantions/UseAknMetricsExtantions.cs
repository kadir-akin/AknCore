using Core.Metric.Middleware;
using Microsoft.AspNetCore.Builder;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Metric.Extantions
{
    public static class UseAknMetricsExtantions
    {
        public static void UseAknMetrics(this IApplicationBuilder app)
        {

           
            app.UseHttpMetrics();
            app.UseMiddleware<AknMetricsMiddleware>();
        }
    }
}
