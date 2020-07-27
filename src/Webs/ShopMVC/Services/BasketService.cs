using BikeHttpClient;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor httpContextAccessor;

        public BasketService(ICustomHttpClient customHttpClient, IHttpContextAccessor httpContextAccessor)
        {
            baseUrl = "http://host.docker.internal:5105/api/basket/";
            this.customHttpClient = customHttpClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<BasketProduct>> GetBasket(string userId)
        {
            var getBasketUrl = baseUrl + userId;

            var token = httpContextAccessor.HttpContext.User.Claims
                .Where(x => x.Type == "AccessToken").Select(x => x.Value).FirstOrDefault();

            var basketProducts = await customHttpClient.GetStringAsync(getBasketUrl, token);

            return JsonConvert.DeserializeObject<IEnumerable<BasketProduct>>(basketProducts);
        }
    }
}
