using Core.Security.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security.Extantions
{
    public static class UseAknRequestContextExtantions
    {
        public static void UseAknRequestContextExtantion(this IApplicationBuilder app)
        {
            app.UseMiddleware<AknRequestContextMiddleware>();
        }
    }
}
