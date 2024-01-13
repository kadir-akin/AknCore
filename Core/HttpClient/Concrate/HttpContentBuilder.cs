using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Core.HttpClient.Concrate
{
    public class HttpContentBuilder
    {
        public static StringContent CreateStringContent(string content, Encoding encoding , string mediaType="application/json") 
        {
            return new StringContent(content,encoding,mediaType);
        }
        public static StringContent CreateStringContent(string content, string mediaType = "application/json")
        {
            return new StringContent(content, Encoding.UTF8, mediaType);
        }
    }
}
