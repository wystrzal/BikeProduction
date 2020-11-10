using ShopMVC.Models;
using System.Threading.Tasks;

namespace ShopMVC.Interfaces
{
    public interface ICookieAuthentication
    {
        Task SignIn(TokenModel tokenModel);
    }
}
