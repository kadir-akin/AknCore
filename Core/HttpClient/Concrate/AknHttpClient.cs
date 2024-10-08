﻿using Core.Exception;
using Core.HttpClient.Abstract;
using Core.LogAkn.Abstract;
using Core.Security.Abstract;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.HttpClient.Concrate
{
    public class AknHttpClient<TConfiguration> : IAknHttpClient<TConfiguration> where TConfiguration :class, IAknHttpConfiguration
    {
        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly IOptions<TConfiguration> _clientConfig;
        private readonly JsonSerializerOptions _jsonSerliliazeOptions;
        private readonly IAknRequestContext _requestContext;
        public AknHttpClient(System.Net.Http.HttpClient httpClient, IOptions<TConfiguration> clientConfig, IAknRequestContext requestContext)
        {
            _requestContext = requestContext;
            _httpClient = httpClient;
            _clientConfig = clientConfig;
            _httpClient = BuildClientConfig(_httpClient, _clientConfig);
            _jsonSerliliazeOptions = BuildJsonSerializerOptions(_clientConfig.Value);

        }

        public async Task<AknHttpResponse<TSuccess, TError>> SendAsync<TSuccess, TError>(string prefixUrl, HttpMethodType httpMethodType, HttpContent httpContent = null, Dictionary<string, string> headers = null) where TSuccess : class where TError : class
        {
            var requestUrl = _clientConfig.Value.BaseUrl + prefixUrl;
            try
            {
                using (HttpRequestMessage requestMessage = new HttpRequestMessage())
                {
                    requestMessage.Method = BuildHttpMethod(httpMethodType);
                    requestMessage.RequestUri = new Uri(requestUrl);
                    requestMessage.Content = httpContent;

                    if (headers != null && headers.Any())
                    {
                        foreach (var item in headers)
                        {
                            requestMessage.Headers.Add(item.Key, item.Value);
                        }
                    }

                    using (var resultHttpResponse = await _httpClient.SendAsync(requestMessage))
                    {
                        if (resultHttpResponse.IsSuccessStatusCode)
                        {
                            using (var streamSuccess = await resultHttpResponse.Content.ReadAsStreamAsync())
                            {
                                var result = await JsonSerializer.DeserializeAsync<TSuccess>(streamSuccess, _jsonSerliliazeOptions);
                                return new AknHttpResponse<TSuccess,TError>(resultHttpResponse, result);
                            }
                        }
                        else
                        {
                            using (var streamError = await resultHttpResponse.Content.ReadAsStreamAsync())
                            {
                                var result = await JsonSerializer.DeserializeAsync<TError>(streamError, _jsonSerliliazeOptions);
                                return new AknHttpResponse<TSuccess, TError>(resultHttpResponse, result);
                            }
                        }
                       
                    }

                }
            }
            catch (System.Exception ex)
            {
                throw new AknException(new System.Exception($"{requestUrl} internal service Error, Exception Message : {ex.Message}"), AknExceptionType.INTERNALSERVICE);
            }



        }

        public async Task<AknHttpResponse<TError>> SendAsync<TError>(string prefixUrl, HttpMethodType httpMethodType, HttpContent httpContent = null, Dictionary<string, string> headers = null) where TError : class
        {
            var requestUrl = _clientConfig.Value.BaseUrl + prefixUrl;
            try
            {
                using (HttpRequestMessage requestMessage = new HttpRequestMessage())
                {
                    requestMessage.Method = BuildHttpMethod(httpMethodType);
                    requestMessage.RequestUri = new Uri(requestUrl);
                    requestMessage.Content = httpContent;

                    if (headers != null && headers.Any())
                    {
                        foreach (var item in headers)
                        {
                            requestMessage.Headers.Add(item.Key, item.Value);
                        }
                    }

                    using (var resultHttpResponse = await _httpClient.SendAsync(requestMessage))
                    {
                        if (resultHttpResponse.IsSuccessStatusCode)
                        {
                            return new AknHttpResponse<TError>(resultHttpResponse);
                        }
                        else
                        {
                            using (var streamError = await resultHttpResponse.Content.ReadAsStreamAsync())
                            {
                                var result = await JsonSerializer.DeserializeAsync<TError>(streamError, _jsonSerliliazeOptions);
                                return new AknHttpResponse<TError>(resultHttpResponse, result);
                            }
                        }
                       
                    }

                }
            }
            catch (System.Exception ex)
            {
                throw new AknException(new System.Exception($"{requestUrl} internal service Error, Exception Message : {ex.Message}"), AknExceptionType.INTERNALSERVICE);
            }
        }


        private System.Net.Http.HttpClient BuildClientConfig(System.Net.Http.HttpClient httpClient, IOptions<TConfiguration> _clientConfig)
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(_clientConfig.Value.Timeout);
            _httpClient.DefaultRequestHeaders.Add("TransactionId", _requestContext.TransactionId);
            _httpClient.DefaultRequestHeaders.Add("SpanId", _requestContext.SpanId);
            if (_clientConfig.Value.DefaulHeaders != null && _clientConfig.Value.DefaulHeaders.Any())
            {
                foreach (var item in _clientConfig.Value.DefaulHeaders)
                {
                    _httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }

            }
            return httpClient;
        }

        private HttpMethod BuildHttpMethod(HttpMethodType httpMethodType)
        {
            switch (httpMethodType)
            {
                case HttpMethodType.GET: return HttpMethod.Get;
                case HttpMethodType.POST: return HttpMethod.Post;
                case HttpMethodType.PUT: return HttpMethod.Put;
                case HttpMethodType.DELETE: return HttpMethod.Delete;
                default: return HttpMethod.Get;
            }

        }

        private JsonSerializerOptions BuildJsonSerializerOptions(IAknHttpConfiguration configuration) 
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IgnoreNullValues = configuration.IgnoreNullValues;
            options.PropertyNameCaseInsensitive = configuration.PropertyNameCaseInsensitive;
            return options; ;

        }
    }
}
