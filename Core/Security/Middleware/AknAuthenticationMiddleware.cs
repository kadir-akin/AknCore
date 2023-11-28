using Core.Exception.Exceptions;
using Core.Security.Abstract;
using Core.Security.Basic;
using Core.Security.Concrete;
using Core.Security.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Middleware
{
    public class AknAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<SecurityConfiguration> _securityConfiguration;
        private readonly IBasicAuthenticationHelper _basicAuthenticationHelper;
        private readonly IJwtHelper _jwtHelper;
        private readonly IAknRequestContext  _requestContext;
        public AknAuthenticationMiddleware
            (
            RequestDelegate next,
            IOptions<SecurityConfiguration> securityConfiguration,
            IBasicAuthenticationHelper basicAuthenticationHelper,
            IAknRequestContext requestContext,
            IJwtHelper jwtHelper
            )
        {
            _next = next;
            _securityConfiguration = securityConfiguration;
            _basicAuthenticationHelper = basicAuthenticationHelper;
            _requestContext = requestContext;
            _jwtHelper = jwtHelper;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var jwtConfiguration = _securityConfiguration.Value.JwtConfiguration;
            var basicConfiguration =_securityConfiguration.Value.BasicConfiguration;
            var headers = httpContext.Request.Headers;
            AuthenticationResult authenticationResult = null;

            if (basicConfiguration !=null)
            {
                authenticationResult =await _basicAuthenticationHelper.IsAutheticate( headers);
            }
            else if (jwtConfiguration != null)
            {
                authenticationResult = await _jwtHelper.IsAutheticate(headers);
            }
            else
            {
                throw new UnAuthenticationException();
            }


            if (authenticationResult ==null || !authenticationResult.IsSuccess)
                throw new UnAuthenticationException();


            await _next(httpContext);
        }
    }
}
