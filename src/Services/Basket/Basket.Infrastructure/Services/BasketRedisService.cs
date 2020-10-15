using Basket.Core.Dtos;
using Basket.Core.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
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
            ValidateUserId(userId);

            var basket = await distributedCache.GetStringAsync(userId);

            return basket == null ? null : JsonConvert.DeserializeObject<UserBasketDto>(basket);
        }

        public async Task RemoveBasket(string userId)
        {
            ValidateUserId(userId);

            await distributedCache.RemoveAsync(userId);
        }

        public async Task SaveBasket(string userId, UserBasketDto basket)
        {
            ValidateUserBasket(basket);

            await RemoveBasket(userId);

            string serializeObject = JsonConvert.SerializeObject(basket);

            await distributedCache.SetStringAsync(userId, serializeObject, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
            });
        }

        private void ValidateUserBasket(UserBasketDto basket)
        {
            if (basket == null)
            {
                throw new ArgumentNullException("UserBasketDto");
            }
        }

        private void ValidateUserId(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("UserID");
            }
        }
    }
}
