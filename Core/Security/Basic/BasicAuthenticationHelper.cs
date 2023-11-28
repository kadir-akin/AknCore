using Core.Security.Abstract;
using Core.Security.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Basic
{
    public class BasicAuthenticationHelper : IBasicAuthenticationHelper
    {
        private readonly IOptions<SecurityConfiguration> _securityConfiguration;
        public BasicAuthenticationHelper(IOptions<SecurityConfiguration> securityConfiguration)
        {
            _securityConfiguration = securityConfiguration;
        }
        public Task<AuthenticationResult> IsAutheticate(IHeaderDictionary header)
        {
            var basicConfiguration = _securityConfiguration.Value.BasicConfiguration;
            string authorizationValue = header[HeaderConstants.Authorization];

            if (string.IsNullOrEmpty(authorizationValue) || authorizationValue.StartsWith(HeaderConstants.Basic))
                return Task.FromResult(new AuthenticationResult(false));

            if (basicConfiguration == null || string.IsNullOrEmpty(basicConfiguration.UserName) | string.IsNullOrEmpty(basicConfiguration.Password))
                return Task.FromResult(new AuthenticationResult(false));

            string encodedUsernamePassword = authorizationValue.Substring("Basic ".Length).Trim();

            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

            int seperatorIndex = usernamePassword.IndexOf(':');

            string username = usernamePassword.Substring(0, seperatorIndex);
            string password = usernamePassword.Substring(seperatorIndex + 1);

            if (username == basicConfiguration.UserName && password == basicConfiguration.Password)
            {
                return Task.FromResult(new AuthenticationResult(false));
            }

            return Task.FromResult(new AuthenticationResult(true));
        }
    }
}
