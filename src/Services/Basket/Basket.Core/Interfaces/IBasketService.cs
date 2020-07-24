using Basket.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Interfaces
{
    public interface IBasketService
    {
        Task UpdateBasket(UserBasket userBasket);
        Task RemoveBasket(string userId);
        Task<List<Product>> GetBasket(string userId);
    }
}
