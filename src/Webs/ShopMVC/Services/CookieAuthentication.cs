using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopMVC.Services
{
    public class CookieAuthentication : ICookieAuthentication
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CookieAuthentication(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task SignIn(TokenModel tokenModel)
        {
            AuthenticationProperties options = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTime.Now.AddDays(1)
            };

            var jwt = tokenModel.Token;
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
  
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Authentication, tokenModel.Token),
                new Claim(ClaimTypes.NameIdentifier, tokenModel.NameIdentifier)
            };

            claims.AddRange(token.Claims);

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                options);
        }
    }
}
