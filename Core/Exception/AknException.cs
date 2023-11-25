using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exception
{
    public class AknException
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public DateTime CreateDate { get; set; }


        public AknExceptionType AknExceptionType { get; set; }

        public System.Exception Exception { get; set; }

        public AknException()
        {
            
        }

    }
}
