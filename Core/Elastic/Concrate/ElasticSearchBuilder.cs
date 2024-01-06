using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Elastic.Concrate
{
    public class ElasticSearchBuilder
    {
        public int Size { get; set; }
        public int From { get; set; }

        public List<QueryContainer> MustClauses { get; set; }
        public List<QueryContainer> FilterClauses { get; set; }
        public List<QueryContainer> ShouldClauses { get; set; }
        public ElasticSearchBuilder()
        {
            MustClauses = new List<QueryContainer>();
            FilterClauses= new List<QueryContainer>();
            ShouldClauses= new List<QueryContainer>();
        }

        public ElasticSearchBuilder SetSize(int size)
        {
            Size = size;
            return this;
        }

        public ElasticSearchBuilder SetFrom(int from)
        {
            From = from;
            return this;
        }

        public ElasticSearchBuilder AddTermQuery(string term, string field)
        {
            FilterClauses.Add(new TermQuery
            {
                Field = new Field(field),
                Value = term.ToLower()
            });

            return this;
        }

        public ElasticSearchBuilder AddRangeFilter(double gte, double lte, string field)
        {
            FilterClauses.Add( new NumericRangeQuery
            {
                Field = new Field(field),
                LessThanOrEqualTo = lte,
                GreaterThanOrEqualTo = gte
            });

            return this;
        }

        public ElasticSearchBuilder AddExistQuery(string name, string field)
        {
            FilterClauses.Add(new ExistsQuery
            {
                Name = name,
                Field = field
            });

            return this;
        }

        public QueryContainer BuildBoolQuery() 
        {
            return new BoolQuery()
            {
             Filter = FilterClauses,
             Must=MustClauses,
             Should=ShouldClauses,
            };
        }

    }
}
