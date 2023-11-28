using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security.Jwt
{
    public class JwtAuthConfiguration
    {
        public string SecretKey { get; set; }

        public string Audience { get; set; }

        public string Issuer { get; set; }
    }
}
