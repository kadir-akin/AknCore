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
        public Task<AknHttpResponse<T>> SendAsync<T>(string prefixUrl, HttpMethodType httpMethodType, HttpContent httpContent=null , Dictionary<string, string> headers = null) where T : class;
        public Task<AknHttpResponse> SendAsync(string prefixUrl, HttpMethodType httpMethodType, HttpContent httpContent=null,  Dictionary<string, string> headers = null);
    }

}
