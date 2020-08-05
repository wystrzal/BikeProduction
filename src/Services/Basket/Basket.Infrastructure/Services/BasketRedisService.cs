using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Basket.Core.Dtos.Enums.ChangeProductQuantityEnum;

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

            return basket == null ? null : JsonConvert.DeserializeObject<UserBasketDto>(basket);
        }

        public async Task RemoveBasket(string userId)
        {
            await distributedCache.RemoveAsync(userId);
        }

        public async Task SaveBasket(string userId, string serializeObject)
        {
            await distributedCache.SetStringAsync(userId, serializeObject, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
            });
        }
    }
}
