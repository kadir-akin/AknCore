using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Elastic.Concrate
{
    public class ElasticSearchSuggest
    {
        public string Text { get; set; }
        public double Score { get; set; }
        public string Id { get; set; }
        public IEnumerable<string> Input { get; set; }
        public object SuggestOutput { get; set; }
    }
}
