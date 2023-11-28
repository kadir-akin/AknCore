using Core.Exception.Exceptions;
using Core.Security.Concrete;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http.Filters;


namespace Core.Security.Filter
{
    public class AknAuthorizationFilter : FilterAttribute, Microsoft.AspNetCore.Mvc.Filters.IActionFilter
    {
        private readonly AuthotanticationType _authotanticationType;
        private readonly string _roles;
        public AknAuthorizationFilter(AuthotanticationType authotanticationType,string roles=null)
        {
            _authotanticationType = authotanticationType;
            _roles = roles;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var headers =context.HttpContext.Request.Headers;

            if (headers == null)
                throw new UnAuthenticationException();

            if (_authotanticationType==AuthotanticationType.JWT)
            {

            }
            else
            {

            }

        }
    }
}
