using BikeHttpClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ShopMVC.Extensions;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using ShopMVC.Models.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShopMVC.Models.Enums.HomeProductEnum;

namespace ShopMVC.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly string baseUrl;
        private readonly string token;
        private readonly ICustomHttpClient customHttpClient;

        public CatalogService(ICustomHttpClient customHttpClient, IHttpContextAccessor httpContextAccessor)
        {
            baseUrl = "http://host.docker.internal:5101/api/catalog/";
            this.customHttpClient = customHttpClient;
            token = httpContextAccessor.HttpContext.GetToken();
        }

        public async Task<IEnumerable<SelectListItem>> GetBrandListItem()
        {
            var getBrandsUrl = $"{baseUrl}brands";

            var brands = await customHttpClient.GetStringAsync(getBrandsUrl);

            var deserializedBrands = JsonConvert.DeserializeObject<List<Brand>>(brands);

            var brandListItem = deserializedBrands.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });

            return brandListItem;
        }

        public async Task<List<CatalogProduct>> GetProducts(CatalogFilteringData filteringData)
        {
            var getProductsUrl = $"{baseUrl}";

            var queryParams = SetQueryParams(filteringData);

            var products = await customHttpClient.GetStringAsync(getProductsUrl, null, queryParams);

            return JsonConvert.DeserializeObject<List<CatalogProduct>>(products);
        }

        private Dictionary<string, string> SetQueryParams(CatalogFilteringData filteringData)
        {
            var queryParams = new Dictionary<string, string>
            {
                ["Take"] = filteringData.Take.ToString(),
                ["Skip"] = filteringData.Skip.ToString()
            };

            if (filteringData.Sort != 0)
            {
                queryParams.Add("Sort", filteringData.Sort.ToString());
            }

            if (filteringData.Colors != 0)
            {
                queryParams.Add("Colors", filteringData.Colors.ToString());
            }

            if (filteringData.BrandId != 0)
            {
                queryParams.Add("BrandId", filteringData.BrandId.ToString());
            }

            if (filteringData.BikeType != 0)
            {
                queryParams.Add("BikeType", filteringData.BikeType.ToString());
            }

            return queryParams;
        }

        public async Task<List<CatalogProduct>> GetHomeProducts(HomeProduct homeProduct)
        {
            var getHomeProductsUrl = $"{baseUrl}home/{homeProduct}";

            var products = await customHttpClient.GetStringAsync(getHomeProductsUrl);

            return JsonConvert.DeserializeObject<List<CatalogProduct>>(products);
        }

        public async Task<CatalogProduct> GetProduct(int productId)
        {
            var getProductUrl = $"{baseUrl}{productId}";

            var product = await customHttpClient.GetStringAsync(getProductUrl);

            return JsonConvert.DeserializeObject<CatalogProduct>(product);
        }

        public async Task AddProduct(CatalogProduct product)
        {
            var addProductUrl = $"{baseUrl}";

            await customHttpClient.PostAsync(addProductUrl, product, token);
        }

        public async Task UpdateProduct(CatalogProduct product)
        {
            var updateProductUrl = $"{baseUrl}";

            await customHttpClient.PutAsync(updateProductUrl, product, token);
        }

        public async Task DeleteProduct(int productId)
        {
            var deleteProductUrl = $"{baseUrl}{productId}";

            await customHttpClient.DeleteAsync(deleteProductUrl, token);
        }
    }
}
