using Core.Exception;
using Core.Localization;
using Core.Security.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.Security.Extantions
{
    public static class UseBasicAuthExtantions
    {
        public static void UseBasicAuth(this IApplicationBuilder app)
        {
            var configuration =(IConfiguration)app.ApplicationServices.GetService(typeof(IConfiguration));
            var basic = configuration.GetSection("BasicAuthConfiguration");


            
            if(basic.Exists())
               app.UseMiddleware<BasicAuthenticationMiddleware>();
            else
              throw new System.Exception("Basic Authentication Configuration Not Found");
        }
    }
}
