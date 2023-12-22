using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exception.Concrate
{
    public class AknExceptionModel
    {

        public AknExceptionModel(AknExceptionDetail x)
        {
            Status = x.Status;
            Message = x.Message;
            CreateDate = x.CreateDate.ToString("G");
            AknExceptionType = x.AknExceptionType.ToString();
        }

        public int Status { get; set; }

        public string Message { get; set; }

        public string CreateDate { get; set; } = DateTime.Now.ToString("G");

        public string AknExceptionType { get; set; }
    }
}
