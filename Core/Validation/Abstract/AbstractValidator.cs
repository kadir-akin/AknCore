using Core.Validation.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validation.Abstract
{
    public abstract class AbstractValidator<TValidate> : IValidator, IValidator<TValidate> where TValidate : class, IValidateObject
    {
        private readonly List<(Func<TValidate, bool> expression, string message, int returnCode)> validationFuncList = new List<(Func<TValidate, bool> expression, string message, int returnCode)>();


        public ValidationResult Validate(IValidateObject validate)
        {

            ValidateHandle((TValidate)validate);
            return Execute((TValidate)validate);
        }

        public Task<ValidationResult> ValidateAsync(IValidateObject validate)
        {
            ValidateHandleAsync((TValidate)validate);
            return Task.FromResult(Execute((TValidate)validate));
        }

        public abstract void ValidateHandle(TValidate validate);
        public abstract Task ValidateHandleAsync(TValidate validate);

        private ValidationResult Execute(TValidate validate)
        {
            if (!validationFuncList.Any())
                return null;

            var validationResult = new ValidationResult();
            validationResult.IsSucces = true;

            foreach (var item in validationFuncList)
            {
                
                if (item.Item1.Invoke(validate))
                    continue;

                validationResult.IsSucces = false;
                validationResult.Message = item.message;
                validationResult.Errors.Add(new Core.Exception.AknException()
                {
                    Message = item.message,
                    AknExceptionType = Core.Exception.AknExceptionType.VALIDATION,
                    CreateDate = System.DateTime.Now,
                    Status = item.returnCode
                });
                           
            }

            return validationResult;
        }

        public Func<TValidate, bool> RuleFor(Expression<Func<TValidate, bool>> expression, string message = "Validasyon Hatası", int returnCode = 400)
        {

            var result = expression.Compile();

            validationFuncList.Add((result, message, returnCode));

            return result;


        }

        public Task<Func<TValidate, bool>> RuleForAsync(Expression<Func<TValidate, bool>> expression, string message = "Validasyon Hatası", int returnCode = 400)
        {

            var result= RuleFor(expression, message, returnCode);

            return Task.FromResult(result);


        }

    }


}
