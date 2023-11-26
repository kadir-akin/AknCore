using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ResultModel
{
    public interface IResult
    {
    }
    public interface IResult<TData> : IResult
    {
    }
}
