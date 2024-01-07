using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Elastic.Concrate
{
    public class ElasticSearchRepsonse<T>
    {
        public bool IsValid { get; set; }

        public System.Exception Exception { get; set; }

        public List<T> Documents { get; set; }

        public List<ElasticSearchSuggest> SearchSuggests { get; set; }

    }
}
