using CompanyManagementMVC.Models;
using System.Threading.Tasks;

namespace CompanyManagementMVC.Interfaces
{
    public interface ICookieAuthentication
    {
        Task<bool> SignIn(TokenModel tokenModel);
    }
}
