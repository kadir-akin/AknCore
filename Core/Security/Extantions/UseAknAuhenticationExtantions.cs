using Core.Security.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security.Extantions
{
    public static class UseAknAuhenticationExtantions
    {
        public static void UseAknAuhenticationExtantion(this IApplicationBuilder app)
        {
            app.UseMiddleware<AknAuthenticationMiddleware>();
        }
    }
}
