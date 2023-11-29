using Core.Exception.Exceptions;
using Core.Security.Concrete;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Web.Http.Filters;


namespace Core.Security.Filter
{
    public class AknAuthorizationFilter : FilterAttribute, Microsoft.AspNetCore.Mvc.Filters.IActionFilter
    {

        private readonly string[] _roles;
        public AknAuthorizationFilter(params string[] roles)
        {
            _roles = roles;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {


            var userRoles = AknUserUtilities.GetCurrentUserRoles();
            bool isAuthorized = false;

            foreach (var role in _roles)
            {
                if (userRoles.Contains(role))
                {
                    isAuthorized = true;
                    break;
                }

            }

            if (!isAuthorized)
                throw new AuthorizationException();

        }
    }
}
