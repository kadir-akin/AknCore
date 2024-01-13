using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Core.Exception
{
    public class AknException : System.Exception
    {
        public List<AknExceptionDetail> ExceptionDetailList { get; set; } = new List<AknExceptionDetail>();

        public AknException(List<AknExceptionDetail> exceptionDetailList) : base(exceptionDetailList?.FirstOrDefault()?.Message, exceptionDetailList?.FirstOrDefault()?.Exception)
        {
            ExceptionDetailList = exceptionDetailList;
        }
        public AknException(AknExceptionDetail exceptionDetail) : base(exceptionDetail.Message, exceptionDetail.Exception)
        {
            ExceptionDetailList.Add(exceptionDetail);
        }
        public AknException(string message) : base(message)
        {
            var exceptionDetail = new AknExceptionDetail(message);
            ExceptionDetailList.Add(exceptionDetail);
        }

        public AknException(int code,string message) : base(message)
        {
            var exceptionDetail = new AknExceptionDetail(code,message);
            ExceptionDetailList.Add(exceptionDetail);
        }

        public AknException(System.Exception ex) : base(ex.Message,ex)
        {
           var exceptionDetail = new AknExceptionDetail(ex);
           ExceptionDetailList.Add(exceptionDetail);
        }

        public AknException(System.Exception ex,AknExceptionType exceptionType) : base(ex.Message, ex)
        {
            var exceptionDetail = new AknExceptionDetail(ex, exceptionType);
            ExceptionDetailList.Add(exceptionDetail);
        }
    }
}
