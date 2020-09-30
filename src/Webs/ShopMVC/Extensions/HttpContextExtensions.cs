using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace ShopMVC.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetToken(this HttpContext httpContext)
        {
            return httpContext.User.Claims
                .Where(x => x.Type == ClaimTypes.Authentication).Select(x => x.Value).FirstOrDefault();
        }

        public static string GetNameIdentifier(this HttpContext httpContext)
        {
            return httpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier)
                .Select(x => x.Value).FirstOrDefault();
        }
    }
}
