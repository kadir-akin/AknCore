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
            if (aknRequestContext.GetInterfaces().Contains(aknRequestContext))
                services.AddScoped(typeof(IAknRequestContext), aknRequestContext);
            else
                throw new System.Exception("Type is not IAknRequestContext implements");


            
            var aknRequestContextTypes = System.Reflection.Assembly.GetExecutingAssembly()
                               .GetTypes()
                               .Where(type => aknRequestContext.IsAssignableFrom(type) && !type.IsInterface);

            if (aknRequestContextTypes == null || !aknRequestContextTypes.Any())
            {
                throw new System.Exception("implement user type not found");
            }

            var aknInterfacePropertyList = typeof(IAknRequestContext).GetProperties()?.ToList();

            services.AddSingleton(typeof(IAknRequestContextImplementTypes), new AknRequestContextImplementTypes(aknRequestContextTypes?.ToList(), aknInterfacePropertyList));

            

          
            return services;
        }
    }
}
