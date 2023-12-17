using Core.Bus.Abstract;
using Core.Bus.Concrate;
using Core.Bus.RabbitMq;
using Core.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Bus.Extantions
{
    public static class AddBusDependencyExtantions
    {
        public static IServiceCollection AddBusDependency(this IServiceCollection services)
        {
            var busMessageListType = TypeUtilities.GetAllAssembysTypeFromAssignableInterface(typeof(IBusMessage), true);
            var consumeHandlerListType = TypeUtilities.GetAllAssembysTypeFromAssignableInterface(typeof(IConsumeHandler), true);
            var rabbitMqContextList = GetRabbitMqContexts(busMessageListType, consumeHandlerListType, services.BuildServiceProvider());
            
            services.AddSingleton(typeof(IBusContext), new BusContext(busMessageListType, rabbitMqContextList));
            services.AddScoped<IRabbitMqProvider, RabbitMqProvider>();
            return services;
        }

        private static List<RabbitMqContext> GetRabbitMqContexts(List<Type> BusMessageTypes, List<Type> consumeHandler,IServiceProvider serviceProvider)
        {
            var results = new List<RabbitMqContext>();

            foreach (var item in BusMessageTypes)
            {
                var rabbitMqAttribute = TypeUtilities.GetAttributeValueByType<RabbitMqAttribute>(item);
                var consumeType= consumeHandler.Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericArguments().Any(z => z == item)))?.FirstOrDefault();
                IConsumeHandler handler= null;
                if (consumeType != null)
                    handler = TypeUtilities.GetInstance<IConsumeHandler>(consumeType, serviceProvider);

                if (rabbitMqAttribute != null)
                {
                    results.Add(new RabbitMqContext(item, rabbitMqAttribute, handler));
                }
            }


            return results;
        }
    }
}
