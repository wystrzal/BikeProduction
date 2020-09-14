﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace AdminMVC.Extensions
{
    public static class AuthorizationExtensions
    {
        public static void AddCustomAuth(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

            services.AddAuthorization(options =>
            {

            });
        }
    }
}
