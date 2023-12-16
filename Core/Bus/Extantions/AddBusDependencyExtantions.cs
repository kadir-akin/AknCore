using Core.Bus.Abstract;
using Core.Bus.Concrate;
using Core.Bus.RabbitMq;
using Core.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Bus.Extantions
{
    public static class AddBusDependencyExtantions
    {
        public static IServiceCollection AddBusDependency(this IServiceCollection services)
        {
            var busMessageListType= TypeUtilities.GetAllAssembysTypeFromAssignableInterface(typeof(IBusMessage), true);
            Dictionary<Type, RabbitMqAttribute> rabbitMqValues = new Dictionary<Type, RabbitMqAttribute>();
            foreach (var item in busMessageListType)
            {
                var rabbitMqAttribute = TypeUtilities.GetAttributeValueByType<RabbitMqAttribute>(item);
                if (rabbitMqAttribute != null)
                {
                    rabbitMqValues.Add(item, rabbitMqAttribute);
                }
            }

            services.AddSingleton(typeof(IBusContext), new BusContext(busMessageListType, rabbitMqValues));
            services.AddScoped<IRabbitMqProvider, RabbitMqProvider>();
            return services;
        }
    }
}
