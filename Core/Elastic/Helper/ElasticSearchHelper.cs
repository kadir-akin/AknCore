using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Elastic.Helper
{
    public class ElasticSearchHelper
    {
        public static Nest.CompletionField ConvertCompletionField(List<string> suggestList)
        {
            if (suggestList==null || !suggestList.Any())
                return null;    

            return new Nest.CompletionField()
            {
                Input = suggestList?.ToArray(),
            };
        }
    }
}
