using Identity.API.Mapping;
using System.Threading.Tasks;

namespace Identity.Core.Interfaces
{
    public interface IAccountService
    {
        Task<string> TryLogin(LoginDto loginDto);
        Task<bool> AddUser(RegisterDto registerDto);
    }
}
