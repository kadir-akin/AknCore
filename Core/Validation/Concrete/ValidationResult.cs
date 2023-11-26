using Core.Exception;
using System.Collections.Generic;

namespace Core.Validation.Concrete
{
    public class ValidationResult
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public List<AknExceptionDetail> Errors { get; set; } = new List<AknExceptionDetail>();
    }
}
