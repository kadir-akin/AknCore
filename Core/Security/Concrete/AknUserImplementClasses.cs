using Core.Security.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security.Basic
{
    public class AknUserImplementClasses : IAknUserImplementClasses
    {
        private readonly List<Type> _implementTypes;
        public AknUserImplementClasses(List<Type> implementTypes)
        {
            _implementTypes = implementTypes;
        }
        public List<Type> ImplementTypes { get => _implementTypes; set => ImplementTypes = value; }
    }
}
