using Core.HttpClient.Concrate;
using System.Collections.Generic;

namespace Template_Api
{
    public class TestInternalServiceConfiguration : IAknHttpConfiguration
    {
        public string BaseUrl { get; set; }
        public int Timeout { get; set; }
        public Dictionary<string, string> DefaulHeaders { get; set; }
        public bool IgnoreNullValues { get; set; }
        public bool PropertyNameCaseInsensitive { get; set; }
    }
}
