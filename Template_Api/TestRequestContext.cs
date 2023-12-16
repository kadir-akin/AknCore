using Core.Bus.RabbitMq;
using Core.Security.Abstract;

namespace Template_Api
{
    public class TestRequestContext : IAknRequestContext
    {
        public int TestValue { get; set; }
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public string CultureCode { get; set; }
        public string TrueClientIp { get; set; }
        public string SessionId { get; set; }
        public string TransactionId { get; set; }
        public string SpanId { get; set; }
    }
}
