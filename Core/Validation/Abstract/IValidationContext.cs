using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Validation.Abstract
{
    public interface IValidationContext
    {
        public List<Type> ValidatorInterfaceImplements { get; set; }
    }
}
