using Core.RequestContext.Abstract;
using Core.Security.Abstract;
using Core.Security.Concrete;
using Core.Security.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Middleware
{
    public class AknRequestContextMiddleware
    {
        private readonly RequestDelegate _next;
      
        public AknRequestContextMiddleware( RequestDelegate next )
        {
            _next = next;       
        }
        public async Task InvokeAsync(HttpContext httpContext, IAknRequestContext _requestContext,IAknRequestContextImplementTypes _implementTypes)
        {           
            var headers = httpContext.Request.Headers;
            var CountryId= headers[HeaderConstants.CountryId];
            var RegionId = headers[HeaderConstants.RegionId];
            var CultureCode = headers[HeaderConstants.CultureCode];
            var TrueClientIp = headers[HeaderConstants.TrueClientIp];
            var SessionId = headers[HeaderConstants.SessionId];
            var TransactionId = headers[HeaderConstants.TransactionId];
            var SpanId = headers[HeaderConstants.SpanId];

            if (!string.IsNullOrEmpty(CountryId)) 
            {
                int.TryParse(CountryId, out int countryId);
                _requestContext.CountryId = countryId;
            }

            if (!string.IsNullOrEmpty(RegionId))
            {
                int.TryParse(RegionId, out int regionId);
                _requestContext.RegionId = regionId;
            }

            if(!string.IsNullOrEmpty(CultureCode))
                _requestContext.CultureCode=CultureCode;
            
            if (!string.IsNullOrEmpty(TrueClientIp))
                _requestContext.TrueClientIp = TrueClientIp;

            if (!string.IsNullOrEmpty(SessionId))
                _requestContext.SessionId = SessionId;
            else
                _requestContext.SessionId = Guid.NewGuid().ToString();

            if (!string.IsNullOrEmpty(TransactionId))
                _requestContext.TransactionId = TransactionId;
            else
                _requestContext.TransactionId = Guid.NewGuid().ToString();

            if (!string.IsNullOrEmpty(SpanId))
                _requestContext.SpanId = SpanId;
            else
                _requestContext.SpanId = Guid.NewGuid().ToString();

            var otherProperties = _implementTypes.ImplementTypes.FirstOrDefault().GetProperties();

            foreach (var item in otherProperties)
            {
                if (true)
                {

                }
            }

            await _next(httpContext);
        }
    }
}
