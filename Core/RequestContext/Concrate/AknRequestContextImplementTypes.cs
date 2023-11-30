using Core.RequestContext.Abstract;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.RequestContext.Concrate
{
    public class AknRequestContextImplementTypes : IAknRequestContextImplementTypes
    {
        private readonly List<Type> _implementTypes;
        private readonly List<PropertyInfo> _interfacePropertys; 
        public AknRequestContextImplementTypes(List<Type> implementTypes, List<PropertyInfo> interfacePropertys)
        {
            _implementTypes = implementTypes;
            _interfacePropertys = interfacePropertys;
        }
        public List<Type> ImplementTypes { get => _implementTypes; set => ImplementTypes = value; }
        public List<PropertyInfo> InterfacePropertys { get => _interfacePropertys; set => InterfacePropertys = value; }
    }
}
