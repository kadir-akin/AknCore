using Core.Exception;
using Core.Exception.Exceptions;
using Core.ResultModel;
using Core.Security.Abstract;
using Core.Security.Basic;
using Core.Security.Concrete;
using Core.Security.Jwt;
using Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Middleware
{
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthenticationMiddleware( RequestDelegate next)
        {
            _next = next;

        }
        public async Task InvokeAsync(HttpContext httpContext, IOptions<BasicAuthConfiguration> _basicAuthConfiguration,
            IBasicAuthenticationHelper _basicAuthenticationHelper,
            IAknUser _aknUser,
            IAknUserImplementType _aknUserImplementClasses
            )
        {
            
            var headers = httpContext.Request.Headers;
            AuthenticationResult authenticationResult = new AuthenticationResult(false);
            var path = httpContext.Request.Path.ToString();
            var isIgnoreEndpoind = path.Contains(HeaderConstants.IgnoreEndpoindName) || path.Contains("startup");

            if (_basicAuthConfiguration.Value != null && !isIgnoreEndpoind)
            {
                authenticationResult = await _basicAuthenticationHelper.IsAutheticate(headers);
                
                if (authenticationResult.IsSuccess) 
                {
                    var properries = _aknUserImplementClasses.ImplementTypes.FirstOrDefault().GetProperties();                    
                    var userInfo = JsonConvert.DeserializeObject<IAknUser>((string)authenticationResult.Data, AknUserJsonConvertor.GetJsonSerializerSettings(_aknUserImplementClasses.ImplementTypes.FirstOrDefault()));

                    foreach (var item in properries)
                    {
                        var value = item.GetValue(userInfo);

                        if (value != null)
                            item.SetValue(_aknUser, value);
                    }

                    httpContext.User = _aknUser?.SetCurrentUser();
                    _aknUser.IsAuthenticated = true;
                    _aknUser.AuthenticationType=Core.Security.Concrete.AuthotanticationType.BASIC.ToString();
                }
            }
            
            if (!authenticationResult.IsSuccess && !isIgnoreEndpoind) 
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
