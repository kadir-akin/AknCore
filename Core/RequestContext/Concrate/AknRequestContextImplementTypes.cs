using Core.RequestContext.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.RequestContext.Concrate
{
    public class AknRequestContextImplementTypes : IAknRequestContextImplementTypes
    {
        private readonly List<Type> _implementTypes;
        public AknRequestContextImplementTypes(List<Type> implementTypes)
        {
            _implementTypes = implementTypes;
        }
        public List<Type> ImplementTypes { get => _implementTypes; set => ImplementTypes = value; }
    }
}
