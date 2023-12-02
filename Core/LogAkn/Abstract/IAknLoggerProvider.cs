using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LogAkn.Abstract
{
    public interface IAknLoggerProvider
    {
        public IAknLogger CreateLogger();
    }
}
