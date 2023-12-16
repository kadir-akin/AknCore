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
using Core.Utilities;

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
           
            var types= TypeUtilities.GetAllAssembysTypeFromAssignableInterface(typeof(IValidator), false);
            
            services.AddSingleton(typeof(IValidationContext),new ValidationContext(types));

            services.AddMvc(x => x.Filters.Add(typeof(AknValidationFilter)));
          

            return services;
        }
    }
}
