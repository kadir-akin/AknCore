using Core.Exception;
using Core.LogAkn.Abstract;
using Core.RequestContext.Concrate;
using Core.Security.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Core.LogAkn.Concrate
{
    public class RequestContextLog : IRequestContextLog
    {
        public IAknRequestContext RequestContext { get; set; }
        public IAknUser User { get; set; }
        public string ProjectName { get; set; }
        public string ApplicationName { get; set; }
        public int StatusCode { get; set; }
        public int ReturnCode { get; set; }
        public string Message { get; set; }
        public string CreateDate { get; set; }
        public string AknExceptionType { get; set; }
        public System.Exception Exception { get; set; }
        public string ActionPath { get; set; }
        public string LogLevel { get; set; }

        public string Query { get; set; }

        public RequestContextLog( HttpContext httpContext, AknException aknException, IAknRequestContext requestContext, IAknUser user, string logLevel, ProjectInfoConfiguration projectInfo)
        {
            RequestContext = requestContext;
            User = user;
            ProjectName = projectInfo?.ProjectName;
            ApplicationName = projectInfo?.ApplicationName;
            StatusCode = aknException==null ? ((int)HttpStatusCode.OK) : (aknException.ExceptionDetailList.FirstOrDefault()?.Status ?? ((int)HttpStatusCode.OK) );
            ReturnCode = aknException == null ? ((int)HttpStatusCode.OK) : (aknException.ExceptionDetailList.FirstOrDefault()?.Status ?? ((int)HttpStatusCode.OK));
            Message = aknException.ExceptionDetailList.FirstOrDefault()?.Message;
            CreateDate = DateTime.Now.ToString("yyyy'-'MM'-'dd''HH':'mm':'ss");
            AknExceptionType = aknException.ExceptionDetailList.FirstOrDefault()?.AknExceptionType.ToString();
            Exception = aknException.ExceptionDetailList.FirstOrDefault()?.Exception;
            ActionPath = httpContext !=null ? httpContext.Request.Path : null;
            LogLevel = logLevel;
            Query = httpContext != null ? httpContext.Request.QueryString.Value : null;
        }
    }
}
