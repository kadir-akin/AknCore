using Core.Validation.Abstract;
using Core.Validation.Concrete;
using Core.Validation.Extansions;
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
        public override void ValidateHandle(TestInputObject validate)
        {
            RuleFor(x => x.Name != null,"isim boş geçilemez");
            RuleFor(x => x.Id == 3," 3 olmalı");
            RuleFor(x => x.Name.Length<1, "isim uzunluğu birden küçük olmalı");


        }


        public override Task ValidateHandleAsync(TestInputObject validate)
        {
            throw new System.NotImplementedException();
        }
    }
}
