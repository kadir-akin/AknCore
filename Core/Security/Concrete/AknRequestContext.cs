using Core.Security.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security.Concrete
{
    public class AknRequestContext :IAknRequestContext
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
