using Core.Exception;
using Core.Exception.Exceptions;
using Core.ResultModel;
using Core.Security.Abstract;
using Core.Security.Basic;
using Core.Security.Concrete;
using Core.Security.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Middleware
{
    public class AknAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AknAuthenticationMiddleware( RequestDelegate next)
        {
            _next = next;

        }
        public async Task InvokeAsync(HttpContext httpContext, IOptions<SecurityConfiguration> _securityConfiguration,
            IBasicAuthenticationHelper _basicAuthenticationHelper,
            IAknRequestContext _requestContext,
            IJwtHelper _jwtHelper)
        {
            var jwtConfiguration = _securityConfiguration.Value.JwtConfiguration;
            var basicConfiguration = _securityConfiguration.Value.BasicConfiguration;
            var headers = httpContext.Request.Headers;
            AuthenticationResult authenticationResult = new AuthenticationResult(false);

            if (basicConfiguration != null)
            {
                authenticationResult = await _basicAuthenticationHelper.IsAutheticate(headers);
            }
            else if (jwtConfiguration != null)
            {
                authenticationResult = await _jwtHelper.IsAutheticate(headers);
            }
            else
            {
                authenticationResult.IsSuccess = false;
            }


            if (authenticationResult == null || !authenticationResult.IsSuccess) 
            { 
                var aknException = new UnAuthenticationException();
                var resulModel = new ErrorResult<List<AknExceptionDetail>>(aknException.ExceptionDetailList, aknException.ExceptionDetailList.FirstOrDefault().Status, aknException.ExceptionDetailList.FirstOrDefault().Message);

                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(resulModel));
            
            }


            await _next(httpContext);
        }
    }
}
