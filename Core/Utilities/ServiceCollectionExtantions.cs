using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities
{
    public static class ServiceCollectionExtantions
    {
        public static IServiceCollection Configure(this IServiceCollection services, Type typeToRegister, IConfiguration service)
        {
            var myMethod = typeof(OptionsConfigurationServiceCollectionExtensions)
              .GetMethods(BindingFlags.Static | BindingFlags.Public)
              .Where(x => x.Name == nameof(OptionsConfigurationServiceCollectionExtensions.Configure) && x.IsGenericMethodDefinition)
              .Where(x => x.GetGenericArguments().Length == 1)
              .Where(x => x.GetParameters().Length == 2)
              .Single();

            MethodInfo generic = myMethod.MakeGenericMethod(typeToRegister);
            generic.Invoke(null, new object[] { services, service });
            return services;
        }

        public static IServiceCollection AddHttpClient(this IServiceCollection services, Type typeToRegister,Type typeImplemantion)
        {
            var myMethod = typeof(HttpClientFactoryServiceCollectionExtensions)
              .GetMethods(BindingFlags.Static | BindingFlags.Public)
              .Where(x => x.Name == nameof(HttpClientFactoryServiceCollectionExtensions.AddHttpClient) && x.IsGenericMethodDefinition)
              .Where(x => x.GetGenericArguments().Length == 2)
              .Where(x => x.GetParameters().Length == 1)
              .Single();

            MethodInfo generic = myMethod.MakeGenericMethod(typeToRegister, typeImplemantion);
            generic.Invoke(null, new object[] { services });
            return services;
        }
    }
}
