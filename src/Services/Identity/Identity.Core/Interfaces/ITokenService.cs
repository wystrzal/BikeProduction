using Identity.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Identity.Core.Interfaces
{
    public interface ITokenService
    {
        Task<TokenModel> GenerateToken(User user, UserManager<User> userManager);
    }
}
