using Core.HttpClient.Concrate;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.HttpClient.Abstract
{
    public interface IAknHttpClient<TConfiguration>  where TConfiguration : class, IAknHttpConfiguration 
    {
        public Task<AknHttpResponse<TSuccess,TError>> SendAsync<TSuccess, TError>(string prefixUrl, HttpMethodType httpMethodType, HttpContent httpContent=null , Dictionary<string, string> headers = null) where TSuccess : class where TError :class;
        public Task<AknHttpResponse<TError>> SendAsync<TError>(string prefixUrl, HttpMethodType httpMethodType, HttpContent httpContent=null,  Dictionary<string, string> headers = null) where TError : class;
    }

}
