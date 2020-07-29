using Basket.Core.Interfaces;
using Basket.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Services
{
    public class BasketService : IBasketService
    {
        private readonly IDistributedCache distributedCache;

        public BasketService(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task UpdateBasket(UserBasket userBasket)
        {
            if (userBasket.Products.Count == 1 && userBasket.Products.Any(x => x.Quantity == 1))
            {
                var basket = await GetBasket(userBasket.UserId);

                if (!basket.Any(x => x.Id == userBasket.Products.First().Id))
                {
                    userBasket.Products.AddRange(basket);
                }
            }

            await distributedCache.RemoveAsync(userBasket.UserId);

            string serializeObject = JsonConvert.SerializeObject(userBasket.Products);

            await distributedCache.SetStringAsync(userBasket.UserId, serializeObject, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
            });
        }

        public async Task<List<BasketProduct>> GetBasket(string userId)
        {
            var product = await distributedCache.GetStringAsync(userId);

            if (product == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<BasketProduct>>(product);
        }

        public async Task RemoveBasket(string userId)
        {
            await distributedCache.RemoveAsync(userId);
        }
    }
}
