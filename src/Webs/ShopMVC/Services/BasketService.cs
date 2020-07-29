using BikeHttpClient;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopMVC.Extensions;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
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

        public async Task<UserBasketDto> GetBasket()
        {
            var userId = httpContextAccessor.HttpContext.GetNameIdentifier();

            var getBasketUrl = baseUrl + userId;

            var token = httpContextAccessor.HttpContext.GetToken();

            var basketProducts = await customHttpClient.GetStringAsync(getBasketUrl, token);

            return JsonConvert.DeserializeObject<UserBasketDto>(basketProducts);
        }

        public async Task UpdateBasket(List<BasketProduct> basketProducts)
        {
            var userBasketDto = new UserBasketDto()
            {
                Products = basketProducts,
                UserId = httpContextAccessor.HttpContext.GetNameIdentifier()
            };

            var token = httpContextAccessor.HttpContext.GetToken();

            await customHttpClient.PostAsync(baseUrl, userBasketDto, token);
        }
    }
}
