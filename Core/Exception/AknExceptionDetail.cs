using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.Exception
{
    public class AknExceptionDetail
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public DateTime CreateDate { get; set; }= DateTime.Now;


        public AknExceptionType AknExceptionType { get; set; }

        public System.Exception Exception { get; set; }

        public AknExceptionDetail(System.Exception ex)
        {
            Exception= ex;
            Message = ex.Message;
            AknExceptionType = AknExceptionType.BUSINESS;
            Status =(int)HttpStatusCode.BadRequest;
        }

        public AknExceptionDetail(int code,string message)
        {
            Status= code;
            Message = message;
            AknExceptionType = AknExceptionType.BUSINESS;
        }
        public AknExceptionDetail( string message)
        {
            Message = message;
            AknExceptionType = AknExceptionType.BUSINESS;
            Status = (int)HttpStatusCode.BadRequest;
        }
        public AknExceptionDetail(int code, string message, AknExceptionType aknExceptionType)
        {
            Status = code;
            Message = message;
            AknExceptionType = aknExceptionType;
        }

    }
}
