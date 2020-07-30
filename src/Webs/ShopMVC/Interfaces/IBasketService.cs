using ShopMVC.Models;
using ShopMVC.Models.Dtos;
using ShopMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopMVC.Interfaces
{
    public interface IBasketService
    {
        Task<UserBasketViewModel> GetBasket();
        Task UpdateBasket(List<BasketProduct> basketProducts);
    }
}
