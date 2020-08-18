using BikeHttpClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShopMVC.Models.Enums.HomeProductEnum;

namespace ShopMVC.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly string baseUrl;
        private readonly ICustomHttpClient customHttpClient;

        public CatalogService(ICustomHttpClient customHttpClient)
        {
            baseUrl = "http://host.docker.internal:5101/api/catalog/";
            this.customHttpClient = customHttpClient;
        }

        public async Task<IEnumerable<SelectListItem>> GetBrandListItem()
        {
            var getBrandsUrl = $"{baseUrl}brands";

            var brands = await customHttpClient.GetStringAsync(getBrandsUrl);

            var deserializedBrands = JsonConvert.DeserializeObject<List<Brand>>(brands);

            var brandListItem = deserializedBrands.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });

            return brandListItem;
        }

        public async Task<List<CatalogProduct>> GetProducts(FilteringData filteringData)
        {
            var getProductsUrl = $"{baseUrl}";

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

            var products = await customHttpClient.GetStringAsync(getProductsUrl, null, queryParams);

            return JsonConvert.DeserializeObject<List<CatalogProduct>>(products);
        }

        public async Task<List<CatalogProduct>> GetHomeProducts(HomeProduct homeProduct)
        {
            var getHomeProductsUrl = $"{baseUrl}home/{homeProduct}";

            var products = await customHttpClient.GetStringAsync(getHomeProductsUrl);

            return JsonConvert.DeserializeObject<List<CatalogProduct>>(products);
        }
    }
}
