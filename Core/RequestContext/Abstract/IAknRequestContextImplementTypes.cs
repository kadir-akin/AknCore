using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.RequestContext.Abstract
{
    public interface IAknRequestContextImplementTypes
    {
        public List<Type> ImplementTypes { get; set; }

        public List<PropertyInfo> InterfacePropertys { get; set; }
    }
}
