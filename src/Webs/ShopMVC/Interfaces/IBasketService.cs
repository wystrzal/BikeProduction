using ShopMVC.Models;
using ShopMVC.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopMVC.Interfaces
{
    public interface IBasketService
    {
        Task<IEnumerable<BasketProduct>> GetBasket();
        Task UpdateBasket(List<BasketProduct> basketProducts);
    }
}
