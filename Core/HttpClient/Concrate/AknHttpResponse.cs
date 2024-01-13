using System;
using System.Collections.Generic;
using System.Text;

namespace Core.HttpClient.Concrate
{
    public class AknHttpResponse<T> : AknHttpResponse where T:class
    {
        public AknHttpResponse(System.Net.Http.HttpResponseMessage httpResponse,T responseModel,System.Exception ex=null) :base(httpResponse,ex)
        {
            ResponseModel = responseModel;
        }

        public AknHttpResponse(System.Exception ex) :base(ex)
        {
            ResponseModel = default(T);
        }
        public T ResponseModel { get; set; }

    }
    public class AknHttpResponse
    {       
        public AknHttpResponse(System.Net.Http.HttpResponseMessage httpResponse, System.Exception exception=null)
        {
            IsSuccess = httpResponse.IsSuccessStatusCode;
            HttpStatusCode = (int)httpResponse.StatusCode;
            Ex = exception;
        }

        public AknHttpResponse(System.Exception exception)
        {
            IsSuccess = false;
            HttpStatusCode = 500;
            Ex = exception;
        }
        public bool IsSuccess{ get; set; }
        public int HttpStatusCode { get; set; }
        public System.Exception Ex { get; set; }

    }
}
