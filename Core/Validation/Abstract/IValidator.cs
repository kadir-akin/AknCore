using Core.Validation.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validation.Abstract
{
    public interface IValidator
    {
        public ValidationResult Validate(IValidateObject validate);

        public Task<ValidationResult> ValidateAsync(IValidateObject validate);
    }

    public interface IValidator<TValidate> : IValidator where TValidate : class, IValidateObject
    {
       
    }
}
