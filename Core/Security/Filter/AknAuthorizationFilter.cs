using Core.Exception.Exceptions;
using Core.Security.Concrete;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http.Filters;


namespace Core.Security.Filter
{
    public class AknAuthorizationFilter : FilterAttribute, Microsoft.AspNetCore.Mvc.Filters.IActionFilter
    {
        
        private readonly string _roles;
        public AknAuthorizationFilter(string roles=null)
        {           
            _roles = roles;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            

            

        }
    }
}
