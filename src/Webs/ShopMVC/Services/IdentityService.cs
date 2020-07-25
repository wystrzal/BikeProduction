using BikeHttpClient;
using Microsoft.Win32;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopMVC.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly string baseUrl;
        private readonly ICustomHttpClient customHttpClient;

        public IdentityService(ICustomHttpClient customHttpClient)
        {
            baseUrl = "http://host.docker.internal:5000/api/identity/";
            this.customHttpClient = customHttpClient;
        }

        public async Task<HttpResponseMessage> Login(LoginDto loginDto)
        {
            string loginUrl = baseUrl + "login";

            return await customHttpClient.PostAsync(loginUrl, loginDto);
        }

        public async Task<HttpResponseMessage> Register(RegisterDto registerDto)
        {
            string registerUrl = baseUrl + "register";

            return await customHttpClient.PostAsync(registerUrl, registerDto);
        }
    }
}
