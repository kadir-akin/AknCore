using Core.Elastic.Abstract;
using Core.Elastic.Concrate;
using Core.Exception.Extantions;
using Core.Infrastructure.Extantions;
using Core.Localization.Extantions;
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


            services.AddReqeustContextDependency();
            services.AddBasicAuthDependency(typeof(AknUser));
           
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
            services.AddScoped(typeof(IElasticSearchProvider<>),typeof(ElasticSearchProvider<>));
            services.AddAknValidationFilter();
            services.AddControllers();
            // var serviceProvider = services.BuildServiceProvider();

            services.AddLocalizationService();
         
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
            app.UseAknRequestContextExtantion();
            app.UseBasicAuth();
            app.UseAknExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
