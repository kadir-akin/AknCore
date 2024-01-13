using Core.HttpClient.Abstract;
using Core.HttpClient.Concrate;
using Core.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.HttpClient.Extantions
{
    public static class AknHttpClientExtantions
    {
        public static IServiceCollection AddAknHttpClient(this IServiceCollection services)
        {

            var aknHttpConfigurationTypes = TypeUtilities.GetAllAssembysTypeFromAssignableInterface(typeof(IAknHttpConfiguration), true);
            var _configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            if (aknHttpConfigurationTypes != null && aknHttpConfigurationTypes.Any())
            {

                foreach (var item in aknHttpConfigurationTypes)
                {
                    var httpConfiguration = _configuration.GetSection(item.Name);

                    if (httpConfiguration.Exists())
                        services.Configure(item, httpConfiguration);
                    else
                        throw new System.Exception($"{item.Name} not found");

                    var serviceType = typeof(IAknHttpClient<>).MakeGenericType(item);
                    var implementaionType = typeof(AknHttpClient<>).MakeGenericType(item);
                    services.AddHttpClient(serviceType, implementaionType);
                }                            
            }

            return services;
        }

        



    }
}
