using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using Basket.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Services
{
    public class BasketRedisService : IBasketRedisService
    {
        private readonly IDistributedCache distributedCache;

        public BasketRedisService(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task<UserBasketDto> GetBasket(string userId)
        {
            var basket = await distributedCache.GetStringAsync(userId);

            return basket == null ?
                new UserBasketDto { Products = new List<BasketProduct>() }
                : JsonConvert.DeserializeObject<UserBasketDto>(basket);
        }

        public async Task RemoveBasket(string userId)
        {
            await distributedCache.RemoveAsync(userId);
        }

        public async Task SaveBasket(string userId, UserBasketDto basket)
        {
            await RemoveBasket(userId);

            string serializeObject = JsonConvert.SerializeObject(basket);

            await distributedCache.SetStringAsync(userId, serializeObject, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
            });
        }
    }
}
