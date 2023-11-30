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
        public System.Exception Exception { get; set; }
        public string ActionPath { get; set; }
        public string LogLevel { get; set; }
        public string Query { get; set; }
        public string Id { get; set; }

        public RequestContextLog(string message, string logLevel, HttpContext httpContext, System.Exception exception, IAknRequestContext requestContext, IAknUser user, ProjectInfoConfiguration projectInfo)
        {
            RequestContext = requestContext;
            User = user;
            ProjectName = projectInfo?.ProjectName;
            ApplicationName = projectInfo?.ApplicationName;
            StatusCode = httpContext.Response.StatusCode;
            ReturnCode = httpContext.Response.StatusCode;
            Message = message;
            CreateDate = DateTime.Now.ToString("yyyy'-'MM'-'dd''HH':'mm':'ss");
            Exception = exception;
            ActionPath = httpContext != null ? httpContext.Request.Path : null;
            LogLevel = logLevel;
            Query = httpContext != null ? httpContext.Request.QueryString.Value : null;
            Id = Guid.NewGuid().ToString();
        }


    }
}
