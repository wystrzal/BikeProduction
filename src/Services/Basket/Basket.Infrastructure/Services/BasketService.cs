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
            if (userBasket.Products.Any(x => x.Quantity > 1))
            {
                var changeProductsPrice = userBasket.Products.Where(x => x.Quantity > 1).ToList();

                foreach (var product in changeProductsPrice)
                {
                    product.Price *= product.Quantity;
                }
            }

            List<BasketProduct> products = await GetBasket(userBasket.UserId);

            if (products == null)
            {
                string serializeObject = JsonConvert.SerializeObject(userBasket.Products);

                await distributedCache.SetStringAsync(userBasket.UserId, serializeObject, new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
                });
            }
            else
            {
                foreach (var product in products)
                {
                    var productToUpdate = userBasket.Products.Where(x => x.Id == product.Id).FirstOrDefault();

                    if (productToUpdate != null) 
                    {
                        productToUpdate.Quantity += product.Quantity;
                        productToUpdate.Price += product.Price;
                    } 
                    else
                    {
                        userBasket.Products.Add(product);
                    }
                }

                await distributedCache.RemoveAsync(userBasket.UserId);

                string serializeObject = JsonConvert.SerializeObject(userBasket.Products);

                await distributedCache.SetStringAsync(userBasket.UserId, serializeObject, new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
                });
            } 
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
