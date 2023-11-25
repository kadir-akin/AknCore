using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exception
{
    public enum AknExceptionType
    {
        NOTAUTENTICATION,
        UNAUTHORIZED,               
        VALIDATION,
        BUSINESS,
        INTERNALSERVICE,
        SERVER
    }
}
