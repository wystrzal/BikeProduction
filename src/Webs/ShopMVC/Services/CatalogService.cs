using BikeHttpClient;
using Newtonsoft.Json;
using ShopMVC.Interfaces;
using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            var products = await customHttpClient.GetStringAsync(getProductsUrl, null, queryParams);

            return JsonConvert.DeserializeObject<List<CatalogProduct>>(products);
        }
    }
}
