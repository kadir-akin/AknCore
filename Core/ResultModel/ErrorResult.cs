using Core.Localization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.ResultModel
{
    public class ErrorResult : Result
    {
        public ErrorResult(int code,string message) : base(code, message)
        {

        }

        public ErrorResult() : base((int)HttpStatusCode.BadRequest, LocalizationConstants.GENERAL_ERROR)
        {

        }
    }

    public class ErrorResult<TData> : Result<TData>
    {
        public ErrorResult(TData data) : base(data, (int)HttpStatusCode.BadRequest, LocalizationConstants.GENERAL_ERROR)
        {

        }

        public ErrorResult(TData data,int code,string message) : base(data, code, message)
        {

        }
    }
}
