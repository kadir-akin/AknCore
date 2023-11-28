using Core.Localization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.Exception.Exceptions
{
    public class UnAuthenticationException : AknException
    {
        public UnAuthenticationException() : base(new AknExceptionDetail((int)HttpStatusCode.Unauthorized, LocalizationConstants.UNAUTHENTICATION, AknExceptionType.NOTAUTENTICATION))
        {

        }
    }
}
