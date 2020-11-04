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
        private readonly string sessionId;
        private readonly ICustomHttpClient customHttpClient;
        private readonly ICookieAuthentication cookieAuthentication;

        public IdentityService(ICustomHttpClient customHttpClient, ICookieAuthentication cookieAuthentication,
            IHttpContextAccessor httpContextAccessor)
        {
            baseUrl = "http://host.docker.internal:5000/api/identity/";
            sessionId = httpContextAccessor.HttpContext.Session.Id;
            this.customHttpClient = customHttpClient;
            this.cookieAuthentication = cookieAuthentication;        
        }

        public async Task<HttpResponseMessage> Login(LoginDto loginDto)
        {
            loginDto.SessionId = sessionId;

            string loginUrl = $"{baseUrl}login";

            var response = await customHttpClient.PostAsync(loginUrl, loginDto);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var tokenModel = JsonConvert.DeserializeObject<TokenModel>(await response.Content.ReadAsStringAsync());
                await cookieAuthentication.SignIn(tokenModel);
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
