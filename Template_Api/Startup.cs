using Core.Exception.Extantions;
using Core.Localization.Extantions;
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
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITesResolver, TestResolver>();

            services.AknSecurityDependency(Configuration);
            services.AddAknValidationFilter();
            services.AddControllers();
            // var serviceProvider = services.BuildServiceProvider();

            services.AddLocalizationService();
           // var securityConfiguration = Configuration.GetSection("SecurityConfiguration");

           // services.Configure<SecurityConfiguration>(Configuration.GetSection("SecurityConfiguration"));
           //;

           // var c = Configuration.GetSection("SecurityConfiguration");
           // var a = Configuration.GetValue<string>("abcd");
           // var b = Configuration.GetValue<SecurityConfiguration>("SecurityConfiguration");



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
            app.UseAknAuhenticationExtantion();
            app.UseAknExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
