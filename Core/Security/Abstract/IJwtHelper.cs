using Core.Security.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Abstract
{
    public interface IJwtHelper
    {
        public JwtResult CreateToken(double expireMinute, ClaimsIdentity claimsIdentity);

        public Task<AuthenticationResult> IsAutheticate(IHeaderDictionary header);
    }
}
