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

        public async Task<List<CatalogProduct>> GetProducts(int take, int skip)
        {
            var getProductsUrl = $"{baseUrl}{take}/{skip}";

            var products = await customHttpClient.GetStringAsync(getProductsUrl);

            return JsonConvert.DeserializeObject<List<CatalogProduct>>(products);
        }
    }
}
