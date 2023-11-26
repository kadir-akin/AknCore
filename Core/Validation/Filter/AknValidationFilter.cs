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
        private readonly IServiceProvider _servicesProvider;

        public AknValidationFilter(IValidationContext validationContext, IServiceProvider services)
        {
            _validationContext = validationContext;
            _servicesProvider = services;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

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
                var constractorInfo = validatorType.GetConstructors()?.FirstOrDefault();
                var parameters = new List<object>();
               
                foreach (var param in constractorInfo.GetParameters())
                {
                    var service = _servicesProvider.GetService(param.ParameterType);//get instance of the class
                    parameters.Add(service);
                }

                validator = (IValidator)Activator.CreateInstance(validatorType, parameters?.ToArray());

            }

            if (arguman is IValidateObject validate &&  validator != null)
            {
                validatorResult = validator.Validate(validate);

                if (validatorResult != null && !validatorResult.IsSucces)
                {
                    context.Result = new ObjectResult(validatorResult);
                    return;
                }
            }

        }
    }
}
