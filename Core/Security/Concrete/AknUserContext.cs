using Core.Security.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security.Basic
{
    public class AknUserContext : IAknUserContext
    {
        private readonly List<Type> _implementTypes;
        private readonly List<string> _ignoreEndpoindNames;
        public AknUserContext(List<Type> implementTypes, List<string> ignoreEndpoindNames)
        {
            _implementTypes = implementTypes;
            _ignoreEndpoindNames  = ignoreEndpoindNames;
        }
        public List<Type> ImplementTypes { get => _implementTypes; set => ImplementTypes = value; }

        public List<string> IgnoreEndpoindNames { get=> _ignoreEndpoindNames; set => IgnoreEndpoindNames = value; }
    }
}
