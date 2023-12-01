using Core.RequestContext.Abstract;
using Core.RequestContext.Concrate;
using Core.Security.Abstract;
using Core.Security.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Infrastructure.Extantions
{
    public static class AddReqeustContextDependencyExtantions
    {
        public static IServiceCollection AddReqeustContextDependency(this IServiceCollection services,Type aknRequestContext)
        {
            if (aknRequestContext.GetInterfaces().Contains(typeof(IAknRequestContext)))
                services.AddScoped(typeof(IAknRequestContext), aknRequestContext);
            else
                throw new System.Exception("Type is not IAknRequestContext implements");
           
            var aknRequestContextTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IAknRequestContext).IsAssignableFrom(p) && !p.IsInterface)?.ToList(); ;

            if (aknRequestContextTypes == null || !aknRequestContextTypes.Any())
                throw new System.Exception("implement user type not found");

            var aknInterfacePropertyList = typeof(IAknRequestContext).GetProperties()?.ToList();

            var _configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var projectInfoConfiguration = _configuration.GetSection("ProjectInfoConfiguration");
            
            if (projectInfoConfiguration.Exists())
                Console.WriteLine("implement edilmeli"); //services.Configure<ProjectInfoConfiguration>(projectInfoConfiguration);
            else
                throw new System.Exception("ProjectInfoConfiguration not found");

            services.AddSingleton(typeof(IAknRequestContextImplementTypes), new AknRequestContextImplementTypes(aknRequestContextTypes?.ToList(), aknInterfacePropertyList));
         
            return services;
        }
    }
}
