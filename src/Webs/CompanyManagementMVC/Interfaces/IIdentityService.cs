using CompanyManagementMVC.Models.Dtos;
using System.Net.Http;
using System.Threading.Tasks;

namespace CompanyManagementMVC.Interfaces
{
    public interface IIdentityService
    {
        Task<HttpResponseMessage> Login(LoginDto loginDto);
    }
}
