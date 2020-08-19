using BikeHttpClient;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopMVC.Extensions;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.Dtos;
using ShopMVC.Models.ViewModels;
using System.Threading.Tasks;

namespace ShopMVC.Services
{
    public class BasketService : IBasketService
    {
        private readonly string baseUrl;
        private readonly ICustomHttpClient customHttpClient;
        private readonly string userId;
        private readonly string token;

        public BasketService(ICustomHttpClient customHttpClient, IHttpContextAccessor httpContextAccessor)
        {
            baseUrl = "http://host.docker.internal:5105/api/basket/";
            this.customHttpClient = customHttpClient;
            userId = httpContextAccessor.HttpContext.GetNameIdentifier();
            token = httpContextAccessor.HttpContext.GetToken();
        }

        public async Task<UserBasketViewModel> GetBasket()
        {
            var getBasketUrl = $"{baseUrl}{userId}";

            var basketProducts = await customHttpClient.GetStringAsync(getBasketUrl, token);

            return JsonConvert.DeserializeObject<UserBasketViewModel>(basketProducts);
        }

        public async Task<int> GetBasketQuantity()
        {
            var getBasketQuantityUrl = $"{baseUrl}{userId}/quantity";

            var basketQuantity = await customHttpClient.GetStringAsync(getBasketQuantityUrl, token);

            return JsonConvert.DeserializeObject<int>(basketQuantity);
        }

        public async Task ClearBasket()
        {
            var clearBasketUrl = $"{baseUrl}{userId}";

            await customHttpClient.DeleteAsync(clearBasketUrl, token);
        }

        public async Task RemoveProduct(int productId)
        {
            var removeProductUrl = $"{baseUrl}{userId}/product/{productId}";

            await customHttpClient.DeleteAsync(removeProductUrl, token);
        }

        public async Task AddProduct(BasketProduct basketProduct)
        {
            var addProductDto = new AddProductDto
            {
                Product = basketProduct,
                UserId = userId
            };

            var addProductUrl = $"{baseUrl}add/product";

            await customHttpClient.PostAsync(addProductUrl, addProductDto, token);
        }

        public async Task ChangeProductQuantity(ChangeProductQuantityDto changeProductQuantityDto)
        {
            changeProductQuantityDto.UserId = userId;

            var changeProductQuantityUrl = $"{baseUrl}change/quantity";

            await customHttpClient.PostAsync(changeProductQuantityUrl, changeProductQuantityDto, token);
        }
    }
}
