﻿using Core.Elastic.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Elastic.Concrate
{
    public class ElasticSearchProvider<Tindex> : IElasticSearchProvider<Tindex> where Tindex : class, IElasticIndex
    {
        private readonly IConfiguration _configuration;
        private readonly IElasticClient _client;
        private readonly IOptions<ElasticSearchConfiguration> _elasticConfiguration;
        private readonly string _indexName;
        public ElasticSearchProvider(IConfiguration configuration, IOptions<ElasticSearchConfiguration> elasticConfiguration)
        {
            _configuration = configuration;
            _elasticConfiguration = elasticConfiguration;
            _indexName = typeof(Tindex).Name.ToLower();
            _client = CreateInstance();
        }

        private ElasticClient CreateInstance()
        {
            string host = _elasticConfiguration.Value.Host;
            string port = _elasticConfiguration.Value.Port;
            string username = _elasticConfiguration.Value.Username;
            string password = _elasticConfiguration.Value.Password;

            var settings = new ConnectionSettings(new Uri(host + ":" + port));

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                settings.BasicAuthentication(username, password);

            return new ElasticClient(settings);
        }


        public async Task ChekIndex()
        {
            var anyy = await _client.Indices.ExistsAsync(_indexName);
            if (anyy.Exists)
                return;

            var response = await _client.Indices.CreateAsync(_indexName,
                ci => ci
                    .Index(_indexName)
                    .Settings(s => s.NumberOfShards(3).NumberOfReplicas(1))
                    );

            return;

        }

        public async Task InsertDocument(Tindex document)
        {

            var response = await _client.CreateAsync(document, q => q.Index(_indexName));
            if (response.ApiCall?.HttpStatusCode == 409)
            {
                await _client.UpdateAsync<Tindex>(response.Id, a => a.Index(_indexName).Doc(document));
            }

        }

        public async Task InsertDocuments(List<Tindex> products)
        {
            await _client.IndexManyAsync(products, index: _indexName);
        }


        public async Task<Tindex> GetDocument(string id)
        {
            var response = await _client.GetAsync<Tindex>(id, q => q.Index(_indexName));

            return response.Source;

        }

        public async Task<List<Tindex>> GetDocuments()
        {
            var response = await _client.SearchAsync<Tindex>(q => q.Index(_indexName).Scroll("5m"));
            return response.Documents.ToList();
        }

        public async Task<ElasticSearchRepsonse<Tindex>> SearchAsync(ElasticSearchBuilder searchBuilder)
        {
            var response = await _client.SearchAsync<Tindex>(new SearchRequest(_indexName)
            {
                Size = searchBuilder.Size,
                From = searchBuilder.From,
                Query = searchBuilder.BuildBoolQuery(),

            });

            return new ElasticSearchRepsonse<Tindex>
            {

                Documents = response.Documents?.ToList(),
                Exception = response.OriginalException,
                IsValid = response.IsValid,

            };


        }
    }
}
