using Core.RequestContext.Concrate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Core.Project.Extantions
{
    public static class AddProjectInformationExtantions
    {
        public static IServiceCollection AddProjectInformation(this IServiceCollection services)
        {
            var _configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var projectInfoConfiguration = _configuration.GetSection("ProjectInfoConfiguration");

            if (projectInfoConfiguration.Exists())
                services.Configure<ProjectInfoConfiguration>(projectInfoConfiguration);
            else
                throw new System.Exception("ProjectInfoConfiguration not found");


            var projectInfo = services.BuildServiceProvider().GetService<IOptions<ProjectInfoConfiguration>>();

            if (projectInfo.Value != null && (projectInfo.Value.MinWorkerThreadsCount > 0 || projectInfo.Value.MinCompletionPortThreadsCount > 0))
            {
                ThreadPool.SetMinThreads(projectInfo.Value.MinWorkerThreadsCount, projectInfo.Value.MinCompletionPortThreadsCount);
            }
            if (projectInfo.Value != null && (projectInfo.Value.MaxWorkerThreadsCount > 0 || projectInfo.Value.MaxCompletionPortThreadsCount > 0))
            {
                ThreadPool.SetMaxThreads(projectInfo.Value.MaxWorkerThreadsCount, projectInfo.Value.MaxCompletionPortThreadsCount);
            }

            return services;
        }


    }

}
