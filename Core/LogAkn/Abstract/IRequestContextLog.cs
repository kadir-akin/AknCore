using Core.Security.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LogAkn.Abstract
{
    public interface IRequestContextLog
    {
        public IAknRequestContext RequestContext { get; set; }
       
        public IAknUser   User { get; set; }

        public string ProjectName { get; set; }

        public string ApplicationName { get; set; }

        public  int StatusCode { get; set; }

        public int ReturnCode { get; set; }

        public string Message { get; set; }

        public string CreateDate { get; set; }

        public string AknExceptionType { get; set; }

        public System.Exception Exception { get; set; }

        public string ActionPath { get; set; }

        public string LogLevel { get; set; }

      


    }
}
