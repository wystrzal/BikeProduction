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
    public class BasketService : IBasketService
    {
        private readonly IDistributedCache distributedCache;

        public BasketService(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task AddProduct(AddProductDto addProductDto)
        {
            var basket = await GetBasket(addProductDto.UserId);

            var basketProduct = basket.Products.Where(x => x.Id == addProductDto.Product.Id).FirstOrDefault();

            if (basketProduct != null)
            {
                basketProduct.Quantity += addProductDto.Product.Quantity;
            } 
            else
            {
                basket.Products.Add(addProductDto.Product);
            }

            basket.TotalPrice += (addProductDto.Product.Price * addProductDto.Product.Quantity);

            await distributedCache.RemoveAsync(addProductDto.UserId);

            string serializeObject = JsonConvert.SerializeObject(basket);

            await distributedCache.SetStringAsync(addProductDto.UserId, serializeObject, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
            });
        }

        public async Task ChangeProductQuantity(ChangeProductQuantityDto changeProductQuantityDto)
        {
            var basket = await GetBasket(changeProductQuantityDto.UserId);

            var basketProduct = basket.Products.Where(x => x.Id == changeProductQuantityDto.ProductId).FirstOrDefault();

            if (basketProduct == null)
            {
                return;
            }

            if (changeProductQuantityDto.ChangeQuantityAction == ChangeQuantityAction.Plus)
            {
                basketProduct.Quantity++;
                basket.TotalPrice += basketProduct.Price;
            }
            else
            {
                basketProduct.Quantity--;
                basket.TotalPrice -= basketProduct.Price;
            }

            if (basketProduct.Quantity <= 0)
            {
                basket.Products.Remove(basketProduct);
            }

            await distributedCache.RemoveAsync(changeProductQuantityDto.UserId);

            string serializeObject = JsonConvert.SerializeObject(basket);

            await distributedCache.SetStringAsync(basket.UserId, serializeObject, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
            });
        }

        public async Task<UserBasketDto> GetBasket(string userId)
        {
            var basket = await distributedCache.GetStringAsync(userId);

            return basket == null ? null : JsonConvert.DeserializeObject<UserBasketDto>(basket);
        }

        public async Task<int> GetBasketQuantity(string userId)
        {
            var basket = await GetBasket(userId);

            return basket == null ? 0 : basket.Products.Count;
        }

        public async Task RemoveBasket(string userId)
        {
            await distributedCache.RemoveAsync(userId);
        }

        public async Task RemoveProduct(string userId, int productId)
        {
            var basket = await GetBasket(userId);

            if (basket != null && basket.Products.Count > 0)
            {
                var productToRemove = basket.Products.Where(x => x.Id == productId).FirstOrDefault();

                basket.TotalPrice -= (productToRemove.Price * productToRemove.Quantity);

                basket.Products.Remove(productToRemove);

                string serializeObject = JsonConvert.SerializeObject(basket);

                await distributedCache.SetStringAsync(basket.UserId, serializeObject, new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
                });
            }
        }

    }
}
