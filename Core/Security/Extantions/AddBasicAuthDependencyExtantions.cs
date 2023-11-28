using Core.Security.Abstract;
using Core.Security.Basic;
using Core.Security.Concrete;
using Core.Security.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Security.Extantions
{
    public static class AddBasicAuthDependencyExtantions
    {
        public static IServiceCollection AddBasicAuthDependency(this IServiceCollection services,Type AknUserType)
        {
            services.AddScoped(typeof(IAknUser), AknUserType);
           var aknUserTypes = System.Reflection.Assembly.GetExecutingAssembly()
                              .GetTypes()
                              .Where(type => AknUserType.IsAssignableFrom(type) && !type.IsInterface);

            if (aknUserTypes==null || !aknUserTypes.Any())
            {
                throw new System.Exception("implement user type bulunamadı");
            }

            services.AddSingleton(typeof(IAknUserImplementClasses),new AknUserImplementClasses(aknUserTypes?.ToList()));
            var _configuration= services.BuildServiceProvider().GetService<IConfiguration>();
            var basicAuthConfiguration = _configuration.GetSection("BasicAuthConfiguration");
            if (basicAuthConfiguration.Exists())
            {
                //services.Configure<BasicAuthConfiguration>(basicAuthConfiguration);
            }
            
            services.AddScoped<IBasicAuthenticationHelper, BasicAuthenticationHelper>();

            return services;
        }
    }
}
