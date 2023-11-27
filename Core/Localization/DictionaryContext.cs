using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Localization
{
    public class DictionaryContext : IDictionaryContext
    {
        private readonly List<Dictionary> _dictionaries;
        public DictionaryContext(List<Dictionary> dictionaries)
        {
            _dictionaries = dictionaries;
        }
        public List<Dictionary> Dictionaries { get => _dictionaries; set => Dictionaries = value; }
    }
}
