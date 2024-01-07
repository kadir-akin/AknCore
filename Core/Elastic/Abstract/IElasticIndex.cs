using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Elastic.Abstract
{
    public interface IElasticIndex
    {
        [Keyword]
        public string Id { get; set; }
        public string SuggestOutput { get; set; }
        public CompletionField Suggest { get; set; }
    }
}
