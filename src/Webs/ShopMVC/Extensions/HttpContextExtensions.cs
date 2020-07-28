using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopMVC.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetToken(this HttpContext httpContext)
        {
            return httpContext.User.Claims
                .Where(x => x.Type == "AccessToken").Select(x => x.Value).FirstOrDefault();
        }

        public static string GetNameIdentifier(this HttpContext httpContext)
        {
            return httpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier)
                .Select(x => x.Value).FirstOrDefault();
        }
    }
}
