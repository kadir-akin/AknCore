using Core.Localization.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Localization.Provider
{
    public class LocalizationProvider : ILocalizationProvider
    {
        private readonly IDictionaryContext _context;
        public LocalizationProvider(IDictionaryContext context)
        {
            _context = context;
        }
        public string GetTranslate(string key, string culture, params object[] args)
        {
            return Translate(key, culture, args);
        }

        public Task<string> GetTranslateAsync(string key, string culture, params object[] args)
        {
            return Task.FromResult(Translate(key, culture, args));
        }

        private  string Translate(string key, string culture, params object[] args) 
        {
            var dictionary = _context?.Dictionaries?.Where(x => x.Culture == culture)?.FirstOrDefault();

            if (dictionary == null)
                return key;

            if (!dictionary.DictionaryList.ContainsKey(key))
                return key;

            var translate = dictionary.DictionaryList[key];

            if (args != null)
            translate = String.Format(translate, args);

            return translate;
        }
    }
}
