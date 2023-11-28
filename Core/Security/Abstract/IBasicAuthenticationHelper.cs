using Core.Security.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Abstract
{
    public interface IBasicAuthenticationHelper
    {
        public Task<AuthenticationResult> IsAutheticate(IHeaderDictionary header);
    }
}
