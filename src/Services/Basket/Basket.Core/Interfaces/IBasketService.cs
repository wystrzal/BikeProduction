using Basket.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Interfaces
{
    public interface IBasketService
    {
        Task UpdateBasket(UserBasketDto userBasket);
        Task RemoveBasket(string userId);
        Task<UserBasketDto> GetBasket(string userId);
    }
}
