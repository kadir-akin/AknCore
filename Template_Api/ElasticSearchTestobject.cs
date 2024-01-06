using Core.Elastic.Abstract;
using Nest;
using System;

namespace Template_Api
{
    public class ElasticSearchTestobject :IElasticIndex
    {      
        public string Message { get; set; }
        public string Code { get; set; }
        public string Id { get; set; }

        public int Quantity { get; set; } = 5;
        public DateTime CreateDate { get; set; }
    }
}
