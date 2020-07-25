using BikeHttpClient;
using Newtonsoft.Json;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Services
{
    public class BasketService : IBasketService
    {
        private readonly string baseUrl;
        private readonly ICustomHttpClient customHttpClient;

        public BasketService(ICustomHttpClient customHttpClient)
        {
            baseUrl = "http://host.docker.internal:5105/api/basket/";
            this.customHttpClient = customHttpClient;
        }

        public async Task<IEnumerable<BasketProduct>> GetBasket(string userId)
        {
            var basketProducts = await customHttpClient.GetStringAsync(baseUrl);

            return JsonConvert.DeserializeObject<IEnumerable<BasketProduct>>(basketProducts);
        }
    }
}
