using Core.Validation.Abstract;
using Core.Validation.Concrete;
using System.Threading.Tasks;

namespace Template_Api
{
    public class TestInputObject : IValidateObject
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class TestOutputObjectValidator : AbstractValidator<TestInputObject>
    {
        public override ValidationResult ValidateHandle(TestInputObject validate)
        {
            return new ValidationResult()
            {
                IsSucces = false,
                Message = "Validasyon hatası",
                Errors = new System.Collections.Generic.List<Core.Exception.AknException>
                {
                    new Core.Exception.AknException()
                    {
                        Message = "Validasyon Hatası oldu",
                        AknExceptionType= Core.Exception.AknExceptionType.VALIDATION,
                        CreateDate=System.DateTime.Now,
                        Status=400
                    }

                }
            };

        }


        public override Task<ValidationResult> ValidateHandleAsync(TestInputObject validate)
        {
            throw new System.NotImplementedException();
        }
    }
}
