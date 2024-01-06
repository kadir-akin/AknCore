using Core.Exception;
using Core.Localization;
using Core.Localization.Abstract;
using Core.Validation.Abstract;
using Core.Validation.Concrete;
using Core.Validation.Extansions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Template_Api
{
    public class TestInputObject : IValidateObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public int From { get; set; }

    }

    public class TestOutputObjectValidator : AbstractValidator<TestInputObject>
    {
        private readonly ITesResolver _tesResolver;
        private readonly ILocalizationProvider _localizationProvider;
        public TestOutputObjectValidator(ITesResolver tesResolver, ILocalizationProvider localizationProvider)
        {
             _tesResolver = tesResolver;
            _localizationProvider = localizationProvider;
        }
        public override void ValidateHandle(TestInputObject validate)
        {

            //_tesResolver.GetError();
            RuleFor(x => x.Name.Length > 5, _localizationProvider.GetTranslate(LocalizationConstants.TEST_LOCALIZE,"en-US","Kadir"),2020);
            RuleFor(x => x.Id == 3," 3 olmalı");
           


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
            throw new AknException("GetErrora implemet edilmedi");
        }
    }

    public class TestInputV2 
    {
        public Dictionary<string,string> DictionaryList { get; set; }
        public string Culture { get; set; }
    }
}
