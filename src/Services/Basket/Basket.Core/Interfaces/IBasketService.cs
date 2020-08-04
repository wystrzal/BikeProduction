using Basket.Core.Dtos;
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
        Task RemoveProduct(string userId, int productId);
        Task<int> GetBasketQuantity(string userId);
    }
}
