using ShopMVC.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopMVC.Interfaces
{
    public interface IIdentityService
    {
        Task<HttpResponseMessage> Login(LoginDto loginDto);
        Task<HttpResponseMessage> Register(RegisterDto registerDto);
    }
}
