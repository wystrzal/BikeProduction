using BikeHttpClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopMVC.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly string baseUrl;
        private readonly ICustomHttpClient customHttpClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        public IdentityService(ICustomHttpClient customHttpClient, IHttpContextAccessor httpContextAccessor)
        {
            baseUrl = "http://host.docker.internal:5000/api/identity/";
            this.customHttpClient = customHttpClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<HttpResponseMessage> Login(LoginDto loginDto)
        {
            string loginUrl = $"{baseUrl}login";

            var response = await customHttpClient.PostAsync(loginUrl, loginDto);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var tokenModel = JsonConvert
                    .DeserializeObject<TokenModel>(await response.Content.ReadAsStringAsync());

                AuthenticationProperties options = new AuthenticationProperties();

                options.AllowRefresh = true;
                options.IsPersistent = true;
                options.ExpiresUtc = DateTime.Now.AddDays(1);

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

            return response;
        }

        public async Task<HttpResponseMessage> Register(RegisterDto registerDto)
        {
            string registerUrl = $"{baseUrl}register";

            return await customHttpClient.PostAsync(registerUrl, registerDto);
        }
    }
}
