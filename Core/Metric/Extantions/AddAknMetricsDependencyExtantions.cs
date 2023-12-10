using Core.Metric.Abstract;
using Core.Metric.Concrate;
using Core.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Metric.Extantions
{
    public static class AddAknMetricsDependencyExtantions
    {
        public static IServiceCollection AddAknMetricsDependency(this IServiceCollection services)
        {

            services.AddSingleton(typeof(IAknMetricContext),new AknMetricContext());
            services.AddScoped(typeof(AknMetricsUtilities));
            return services;
        }
    }
}
