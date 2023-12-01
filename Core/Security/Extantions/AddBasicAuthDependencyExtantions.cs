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
            var aknUserTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IAknUser).IsAssignableFrom(p) && !p.IsInterface)?.ToList(); ;

            if (aknUserTypes == null || !aknUserTypes.Any())
                throw new System.Exception("aknUserTypes implement user type not found");


            if (AknUserType.GetInterfaces().Contains(typeof(IAknUser)))
                services.AddScoped(typeof(IAknUser), AknUserType);
            else
                throw new System.Exception("Type is not IAknUser implements");
                                                             
            services.AddSingleton(typeof(IAknUserImplementType),new AknUserImplementClasses(aknUserTypes?.ToList()));
          
            var _configuration= services.BuildServiceProvider().GetService<IConfiguration>();
            var basicAuthConfiguration = _configuration.GetSection("BasicAuthConfiguration");
            
            if (basicAuthConfiguration.Exists())
                Console.WriteLine("BasicAuthConfiguration implement Edilmeli"); //services.Configure<BasicAuthConfiguration>(basicAuthConfiguration);
            else
                throw new System.Exception("BasicAuthConfiguration not found");


            services.AddScoped<IBasicAuthenticationHelper, BasicAuthenticationHelper>();

            return services;
        }
    }
}
