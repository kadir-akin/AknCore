using Core.Security.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security.Abstract
{
    public interface IAknRequestContext
    {              
        public int CountryId { get; set; }

        public int RegionId { get; set; }

        public string CultureCode { get; set; }

        public string TrueClientIp { get; set; }

        public string SessionId { get; set; }

        public string TransactionId { get; set; }

        public string SpanId { get; set; }


    }
}
