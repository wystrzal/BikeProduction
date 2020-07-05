using Identity.Application.Commands;
using System.Threading.Tasks;

namespace Identity.Core.Interfaces
{
    public interface IAccountService
    {
        Task<string> TryLogin(TryLoginCommand command);
        Task AddUser(AddUserCommand command);
    }
}
