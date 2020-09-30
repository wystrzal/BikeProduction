using CompanyManagementMVC.Interfaces;
using CompanyManagementMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CompanyManagementMVC.Services
{
    public class CookieAuthentication : ICookieAuthentication
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CookieAuthentication(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> SignIn(TokenModel tokenModel)
        {
            AuthenticationProperties options = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTime.Now.AddDays(1)
            };

            var claims = SetClaims(tokenModel);

            if (!claims.Any(x => x.Value == "admin"))
                return false;

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            options);

            return true;
        }

        private List<Claim> SetClaims(TokenModel tokenModel)
        {
            var jwt = tokenModel.Token;
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Authentication, tokenModel.Token),
                new Claim(ClaimTypes.NameIdentifier, tokenModel.NameIdentifier)
            };

            claims.AddRange(token.Claims);

            return claims;
        }
    }
}
