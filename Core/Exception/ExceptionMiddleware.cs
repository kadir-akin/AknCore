using Core.ResultModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exception
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {                
                await _next(httpContext);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            AknException aknException= null;
            context.Response.ContentType = "application/json";
            if (exception.GetType() == typeof(SecurityException)) 
            {
                aknException = new AknException(exception);
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (exception.GetType() == typeof(AknException)) 
            {
                aknException = (AknException)exception;
            }
            else
            {
                aknException = new AknException(exception);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            var resulModel= new ErrorResult<List<AknExceptionDetail>>(aknException.ExceptionDetailList, aknException.ExceptionDetailList.FirstOrDefault().Status, aknException.ExceptionDetailList.FirstOrDefault().Message);


            return context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(resulModel));
        }
    }
}
