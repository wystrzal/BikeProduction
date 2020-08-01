﻿using Basket.Core.Interfaces;
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

        public async Task UpdateBasket(UserBasketDto userBasket)
        {
            if (userBasket.Products.Count == 1 && userBasket.Products.Any(x => x.Quantity == 1))
            {
                var basket = await GetBasket(userBasket.UserId);

                foreach (var basketProduct in basket.Products)
                {
                    if (basketProduct.Id == userBasket.Products.First().Id)
                    {
                        basket.Products.Remove(basketProduct);
                    }
                }

                userBasket.Products.AddRange(basket.Products);
            }

            foreach (var product in userBasket.Products)
            {
                userBasket.TotalPrice += (product.Price * product.Quantity);
            }

            await distributedCache.RemoveAsync(userBasket.UserId);

            string serializeObject = JsonConvert.SerializeObject(userBasket);

            await distributedCache.SetStringAsync(userBasket.UserId, serializeObject, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
            });
        }

        public async Task<UserBasketDto> GetBasket(string userId)
        {
            var basket = await distributedCache.GetStringAsync(userId);

            if (basket == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<UserBasketDto>(basket);
        }

        public async Task RemoveBasket(string userId)
        {
            await distributedCache.RemoveAsync(userId);
        }
    }
}
