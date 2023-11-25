using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Web.Http.Filters;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Core.Validation.Abstract;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Core.Validation.Filter;
using Core.Validation.Concrete;
using System.Reflection;

namespace Core.Validation.Extansions
{
    public static class MiddlewareExtansions
    {
        public static IServiceCollection AddAknValidationFilter(this IServiceCollection services)
        {
            //List<Assembly> listOfAssemblies = new List<Assembly>();
            //var mainAsm = Assembly.GetEntryAssembly();
            //listOfAssemblies.Add(mainAsm);
            //foreach (var refAsmName in mainAsm.GetReferencedAssemblies())
            //{
            //    listOfAssemblies.Add(Assembly.Load(refAsmName));
            //}
           


            var interfaceValidatorType = typeof(IValidator);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => interfaceValidatorType.IsAssignableFrom(p))?.ToList();
            services.AddSingleton(typeof(IValidationContext),new ValidationContext(types));

            services.AddMvc(x => x.Filters.Add(typeof(AknValidationFilter)));
          

            return services;
        }
    }
}
