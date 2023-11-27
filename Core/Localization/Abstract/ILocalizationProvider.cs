using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Localization.Abstract
{
    public interface ILocalizationProvider
    {
        public string GetTranslate(string key, string culture,params object[] args );
        public Task<string> GetTranslateAsync(string key, string culture,params object[] args);
    }
}
