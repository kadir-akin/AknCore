using Core.Elastic.Abstract;
using Nest;

namespace Template_Api
{
    public class ElasticSearchTestobject :IElasticIndex
    {      
        public string Message { get; set; }

        public string Code { get; set; }
        public string Id { get; set; }
    }
}
