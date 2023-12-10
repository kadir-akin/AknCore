using Core.LogAkn.Abstract;
using Core.LogAkn.Extantions;
using Core.ResultModel;
using Core.Utilities;
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
        public async Task InvokeAsync(HttpContext httpContext,ILogService logService)
        {

            try
            {                
                await _next(httpContext);
                
            }           
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, logService);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, System.Exception exception, ILogService logService)
        {
            AknException aknException= null;
            context.Response.ContentType = "application/json";
            if (exception is SecurityException) 
            {
                aknException = new AknException(exception);
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (exception is AknException) 
            {
                aknException = (AknException)exception;
                context.Response.StatusCode = aknException.ExceptionDetailList.FirstOrDefault().Status;
            }
            else
            {
                aknException = new AknException(exception);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            var resulModel= new ErrorResult<List<AknExceptionDetail>>(aknException.ExceptionDetailList, aknException.ExceptionDetailList.FirstOrDefault().Status, aknException.ExceptionDetailList.FirstOrDefault().Message);
            logService.LogErrorAsync(aknException, aknException?.Message);                                  
            return context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(resulModel));
        }
    }
}
