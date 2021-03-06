﻿using Identity.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Identity.Infrastructure.Data
{
    public static class DataSeed
    {
        public static void AddData(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                List<User> user = new List<User>()
                {
                    new User { UserName = "admin"},
                    new User { UserName = "user"}
                };

                userManager.CreateAsync(user[0], "Admin123").Wait();
                userManager.AddClaimAsync(user[0], new Claim(ClaimTypes.Role, "admin")).Wait();

                userManager.CreateAsync(user[1], "User123").Wait();
                userManager.AddClaimAsync(user[1], new Claim(ClaimTypes.Role, "user")).Wait();
            }
        }
    }
}
