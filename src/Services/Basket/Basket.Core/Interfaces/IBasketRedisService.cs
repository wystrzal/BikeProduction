using Basket.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Interfaces
{
    public interface IBasketRedisService
    {
        Task RemoveBasket(string userId);
        Task<UserBasketDto> GetBasket(string userId);
        Task SaveBasket(string userId, string serializeObject);
    }
}
