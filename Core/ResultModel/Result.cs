using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ResultModel
{
    public class Result :IResult
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public Result(int code,string message)
        {
            Code = code;
            Message = message;
        }
    }

    public class Result<TData> : Result ,IResult<TData>
    {
        public Result(TData data,int code,string message):base(code,message)
        {
            Data=data;
        }
        public TData Data { get; set; }
    }
}
