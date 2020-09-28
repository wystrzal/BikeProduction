using CompanyManagementMVC.Interfaces;
using CompanyManagementMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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

        public async Task SignIn(TokenModel tokenModel)
        {
            AuthenticationProperties options = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTime.Now.AddDays(1)
            };

            var claims = new List<Claim>
            {
                new Claim("AccessToken", tokenModel.Token),
                new Claim(ClaimTypes.NameIdentifier, tokenModel.NameIdentifier)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                options);
        }
    }
}
