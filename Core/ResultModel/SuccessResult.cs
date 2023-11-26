using Core.Localization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.ResultModel
{
    public class SuccessResult :Result
    {
        public SuccessResult() :base((int)HttpStatusCode.OK,LocalizationConstants.OPERATION_SUCCESS)
        {

        }
    }

    public class SuccessResult<TData> : Result<TData> 
    {
        public SuccessResult(TData data) :base(data, (int)HttpStatusCode.OK, LocalizationConstants.OPERATION_SUCCESS)
        {

        }
    }
}
