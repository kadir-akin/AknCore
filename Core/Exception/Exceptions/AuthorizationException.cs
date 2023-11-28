using Core.Localization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.Exception.Exceptions
{
    public class AuthorizationException :AknException
    {
        public AuthorizationException() :base( new AknExceptionDetail((int)HttpStatusCode.Unauthorized,LocalizationConstants.UNAUTHORIZATION,AknExceptionType.UNAUTHORIZED))
        {

        }
    }
}
