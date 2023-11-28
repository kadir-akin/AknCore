using Core.Security.Abstract;
using Core.Security.Basic;
using Core.Security.Concrete;
using Core.Security.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security.Extantions
{
    public static class AknSecurityDependencyExtantions
    {
        public static IServiceCollection AknSecurityDependency(this IServiceCollection services,IConfiguration _configuration)
        {
            services.AddScoped<IAknUser, AknUser>();
            services.AddScoped<IAknRequestContext, AknRequestContext>();

            var securityConfiguration = _configuration.GetSection("SecurityConfiguration");
            if (securityConfiguration != null)
            {
                services.Configure<SecurityConfiguration>(x => _configuration.GetSection("SecurityConfiguration"));
            }

            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<IBasicAuthenticationHelper, BasicAuthenticationHelper>();

            return services;
        }
    }
}
