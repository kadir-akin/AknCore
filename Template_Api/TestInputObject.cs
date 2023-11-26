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
        private readonly ITesResolver _tesResolver;
        public TestOutputObjectValidator(ITesResolver tesResolver)
        {
             _tesResolver = tesResolver;;
        }
        public override void ValidateHandle(TestInputObject validate)
        {
            //_tesResolver.GetError();
            RuleFor(x => x.Name != null,"isim boş geçilemez");
            RuleFor(x => x.Id == 3," 3 olmalı");
            RuleFor(x => x.Name.Length<1, "isim uzunluğu birden küçük olmalı");


        }


        public override Task ValidateHandleAsync(TestInputObject validate)
        {
            throw new System.NotImplementedException();
        }
    }

    
    public interface ITesResolver 
    {
        public void GetError();
    }

    public class TestResolver : ITesResolver
    {
        public void GetError()
        {
            throw new System.NotImplementedException();
        }
    }

    public class TestInputV2 
    {
        public int Id { get; set; }
    }
}
