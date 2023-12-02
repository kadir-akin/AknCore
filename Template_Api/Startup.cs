using Core.Elastic.Abstract;
using Core.Elastic.Concrate;
using Core.Elastic.Extantions;
using Core.Exception.Extantions;
using Core.Infrastructure.Extantions;
using Core.Localization.Extantions;
using Core.LogAkn.Abstract;
using Core.LogAkn.Concrate;
using Core.LogAkn.Extantions;
using Core.RequestContext.Concrate;
using Core.Security.Abstract;
using Core.Security.Basic;
using Core.Security.Concrete;
using Core.Security.Extantions;
using Core.Security.Filter;
using Core.Utilities;
using Core.Validation.Extansions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Template_Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var projectInfoConfiguration = _configuration.GetSection("ProjectInfoConfiguration");
            if (projectInfoConfiguration.Exists())
                services.Configure<ProjectInfoConfiguration>(projectInfoConfiguration);


            var logConfigurationsection = _configuration.GetSection("LogConfiguration");
            if (logConfigurationsection.Exists())
                services.Configure<LogConfiguration>(logConfigurationsection);


            var basicAuthConfiguration = _configuration.GetSection("BasicAuthConfiguration");
            if (basicAuthConfiguration.Exists())
            {
                services.Configure<BasicAuthConfiguration>(basicAuthConfiguration);
            }

            var elasticConfig = _configuration.GetSection("ElasticSearchConfiguration");
            if (elasticConfig.Exists())
            {
                services.Configure<ElasticSearchConfiguration>(elasticConfig);

            }
            services.AddAknValidationFilter();
            services.AddReqeustContextDependency(typeof(TestRequestContext));
            services.AddBasicAuthDependency(typeof(AknUser));                      
            services.AddElasticSearch();
           
            
            services.AddControllers();          
           
            services.AddLocalizationService();                                
            services.AddAknLogDependency();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
          
            app.UseRouting();
            app.UseAuthorization();

            app.UseAknExceptionMiddleware();
            app.UseAknRequestContextExtantion();
            app.UseBasicAuth();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
