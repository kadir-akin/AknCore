using Core.Validation.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validation.Abstract
{
    public abstract class AbstractValidator<TValidate> : IValidator, IValidator<TValidate> where TValidate : class, IValidateObject
    {
        public ValidationResult Validate(IValidateObject validate)
        {
            return ValidateHandle((TValidate)validate);
        }

        public Task<ValidationResult> ValidateAsync(IValidateObject validate)
        {
            return ValidateHandleAsync((TValidate)validate);
        }

        public abstract ValidationResult ValidateHandle(TValidate validate);
        public abstract Task<ValidationResult> ValidateHandleAsync(TValidate validate);
    }

    
}
