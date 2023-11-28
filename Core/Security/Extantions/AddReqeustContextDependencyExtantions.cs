using Core.Security.Abstract;
using Core.Security.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.Extantions
{
    public static class AddReqeustContextDependencyExtantions
    {
        public static IServiceCollection AddReqeustContextDependency(this IServiceCollection services)
        {
           
            services.AddScoped<IAknRequestContext, AknRequestContext>();
            return services;
        }
    }
}
