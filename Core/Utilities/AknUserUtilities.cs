using Core.Security.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities
{
    public static class AknUserUtilities
    {
       
        public static ClaimsPrincipal SetCurrentUser(this IAknUser aknUser) 
        {
             var claimsIdentity= aknUser.IdentityToClaimsIdentity();

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);           
            System.Threading.Thread.CurrentPrincipal = claimsPrincipal;

            return claimsPrincipal;
            
        }

        public static ClaimsPrincipal GetCurrentUser()
        {
           
           return (ClaimsPrincipal)System.Threading.Thread.CurrentPrincipal; 
        }

        public static ClaimsIdentity IdentityToClaimsIdentity(this IAknUser aknUser)
        {
            var claimIdentity = new ClaimsIdentity();

            var type = aknUser.GetType();
            

            foreach (var item in type.GetProperties())
            {
                var value = item.GetValue(aknUser);
                if (value !=null)
                {
                    claimIdentity.AddClaim(new Claim(item.Name, value.ToString()));
                }
               
            }
                     
            return claimIdentity;
        }

        public static List<string> GetCurrentUserRoles() 
        {
           
            var currentUser = GetCurrentUser();

            var rolesClaim = currentUser.Claims.Where(x => x.Type == "Roles")?.FirstOrDefault()?.Value;

            if (string.IsNullOrEmpty(rolesClaim))
              return new List<string>(); ;

            var rolesArray = rolesClaim.Split(",");

            return rolesArray?.ToList();

        }
    }
}
