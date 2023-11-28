using Core.Security.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security.Concrete
{
    public class AuthenticationResult 
    {
        public bool IsSuccess { get; set; }
        public object Data { get; set; }

        public AuthenticationResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public AuthenticationResult(bool isSuccess,object data)
        {
            IsSuccess=isSuccess;
            Data = data;
        }
    }
}
