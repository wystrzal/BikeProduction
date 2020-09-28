using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopMVC.Interfaces
{
    public interface ICookieAuthentication
    {
        Task SignIn(TokenModel tokenModel);
    }
}
