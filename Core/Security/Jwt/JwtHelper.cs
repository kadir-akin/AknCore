using Core.Security.Abstract;
using Core.Security.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Jwt
{
    public class JwtHelper : IJwtHelper
    {
        public const string SecretKey = "15088D48-F1B4-41D0-9BB9-4A07A0ED3FD9-8DAC1C9E-E13C-4C14-B1AB-69152A800C8D-07E36FBE-41E3-41A7-8632-1E5634883FA5";
        public const string Audience = "tazi.tech";
        public const string Issuer = "tazi.tech";

        private readonly IOptions<SecurityConfiguration> _securityConfiguration;
        public JwtHelper(IOptions<SecurityConfiguration> securityConfiguration)
        {
            _securityConfiguration = securityConfiguration;
        }
        public JwtResult CreateToken(double expireMinute, ClaimsIdentity claimsIdentity)
        {


            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(SecretKey);
            DateTime expire = DateTime.UtcNow.AddMinutes(expireMinute);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = Audience,
                Issuer = Issuer,
                Subject = claimsIdentity,
                Expires = expire,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtResult { Token = tokenHandler.WriteToken(securityToken), Expire = expire };
        }

        public Task<AuthenticationResult> IsAutheticate(IHeaderDictionary header) 
        {
            return null;
        }
        //private ClaimsIdentity IdentityToClaimsIdentity(Identity identity)
        //{
        //    var claimIdentity = new ClaimsIdentity();
        //    claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, identity.Guid.ToString()));
        //    claimIdentity.AddClaim(new Claim(ClaimTypes.Name, identity.FirstName.ToString()));
        //    claimIdentity.AddClaim(new Claim(ClaimTypes.Surname, identity.LastName));
        //    claimIdentity.AddClaim(new Claim(ClaimTypes.Email, identity.Email));


        //    foreach (var role in identity.Roles)
        //    {
        //        claimIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
        //    }

        //    return claimIdentity;
        //}



    }
}
