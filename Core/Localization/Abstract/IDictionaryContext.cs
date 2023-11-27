using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Localization
{
    public interface IDictionaryContext
    {
        public List<Dictionary> Dictionaries { get; set ; }
    }
}
