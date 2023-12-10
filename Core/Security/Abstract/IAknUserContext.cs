using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security.Abstract
{
    public interface IAknUserContext
    {
        public List<Type> ImplementTypes { get; set; }

        public List<string> IgnoreEndpoindNames{ get; set; }
    }
}
