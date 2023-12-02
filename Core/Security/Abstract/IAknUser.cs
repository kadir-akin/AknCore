using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Core.Security.Abstract
{
    public interface IAknUser :IIdentity
    {
        public string Roles { get; set; }
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
    }
}
