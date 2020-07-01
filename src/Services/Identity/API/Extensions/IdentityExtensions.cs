﻿using Identity.Core.Models;
using Identity.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Extensions
{
    public static class IdentityExtensions

    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {
            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
            });

            services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<DataContext>();
        }
    }
}
