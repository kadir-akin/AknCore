using Core.Security.Basic;
using Core.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security.Concrete
{
    public class SecurityConfiguration
    {
        public JwtConfiguration  JwtConfiguration  { get; set; }

        public BasicConfiguration BasicConfiguration { get; set; }

    }
}
