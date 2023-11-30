using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LogAkn.Extantions
{
    public static class AddAknLogExtantions
    {
        public static IServiceCollection AddAknLogDependency(this IServiceCollection services)
        {


            var _configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var projectInfoConfiguration = _configuration.GetSection("ProjectInfoConfiguration");

            if (projectInfoConfiguration.Exists())
                Console.WriteLine("implement edilmeli"); //services.Configure<ProjectInfoConfiguration>(projectInfoConfiguration);
            else
                throw new System.Exception("ProjectInfoConfiguration not found");



            return services;
        }
    }
}
