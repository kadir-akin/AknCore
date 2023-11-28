﻿using Core.Security.Abstract;
using Core.Security.Concrete;
using Core.Security.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Middleware
{
    public class AknRequestContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAknRequestContext _requestContext;
        public AknRequestContextMiddleware
            (
            RequestDelegate next,
            IAknRequestContext requestContext
            )
        {
            _next = next;
            _requestContext = requestContext;
        }
        public async Task InvokeAsync(HttpContext httpContext)
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

            if (!string.IsNullOrEmpty(TransactionId))
                _requestContext.TransactionId = TransactionId;
            else
                _requestContext.TransactionId = Guid.NewGuid().ToString();

            if (!string.IsNullOrEmpty(SpanId))
                _requestContext.SpanId = SpanId;
            else
                _requestContext.SpanId = Guid.NewGuid().ToString();



            await _next(httpContext);
        }
    }
}
