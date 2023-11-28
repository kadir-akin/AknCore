using Core.Security.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security.Concrete
{
    public class JwtResult
    {
        public string Token { get; set; }
        public DateTime Expire { get; set; }
    }
}
