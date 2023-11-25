using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Filters;
using Core.Exception;
using Core.Validation.Abstract;
using Core.Validation.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Validation.Filter
{
    public class AknValidationFilter : FilterAttribute, Microsoft.AspNetCore.Mvc.Filters.IActionFilter
    {
        private readonly IValidationContext _validationContext;

        public AknValidationFilter(IValidationContext validationContext)
        {
            _validationContext = validationContext;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var typeInput = context.ActionDescriptor?.Parameters?.FirstOrDefault()?.ParameterType;
            object arguman = context.ActionArguments.FirstOrDefault().Value;
            ValidationResult validatorResult = null;
            Type validatorType = null;
            IValidator validator = null;
            if (typeInput != null && (typeInput?.GetInterfaces()?.Contains(typeof(IValidateObject)) ?? false))
            {
                validatorType = _validationContext.ValidatorInterfaceImplements?.Where(x => x.BaseType?.GenericTypeArguments?.FirstOrDefault() == typeInput).FirstOrDefault();
            }

            if (validatorType != null)
            {
                validator = (IValidator)Activator.CreateInstance(validatorType);
            }

            if (arguman is IValidateObject validate)
            {
                validatorResult = validator.Validate(validate);

                if (validatorResult !=null && !validatorResult.IsSucces )
                {

                    context.Result = new ObjectResult(validatorResult.Errors.FirstOrDefault());
                    return;
                }
            }

        }
    }
}
