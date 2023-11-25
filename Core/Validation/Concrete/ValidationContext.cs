using Core.Validation.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Validation.Concrete
{
    public class ValidationContext : IValidationContext
    {
        private readonly List<Type> _validatorInterfaceImplements;
        public ValidationContext(List<Type> validatorInterfaceImplements)
        {
            _validatorInterfaceImplements = validatorInterfaceImplements;
        }
        public List<Type> ValidatorInterfaceImplements { get => _validatorInterfaceImplements; set =>ValidatorInterfaceImplements= value; }
    }
}
