using Core.Elastic.Concrate;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Elastic.Abstract
{
    public interface IElasticSearchProvider<Tindex> where Tindex : class, IElasticIndex
    {
        public Task ChekIndex();
        public Task InsertDocument(Tindex document);
        public Task InsertDocuments(List<Tindex> products);
        public Task<Tindex> GetDocument(string id);
        public Task<List<Tindex>> GetDocuments();

        public Task<ElasticSearchRepsonse<Tindex>> SearchAsync(ElasticSearchBuilder searchBuilder);

    }
}
