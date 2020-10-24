using BikeHttpClient;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShopMVC.Areas.Admin.Models;
using ShopMVC.Extensions;
using ShopMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Services
{
    public class ProductionService : IProductionService
    {
        private readonly string baseUrl;
        private readonly string token;
        private readonly ICustomHttpClient customHttpClient;

        public ProductionService(ICustomHttpClient customHttpClient, IHttpContextAccessor httpContextAccessor)
        {
            baseUrl = "http://host.docker.internal:5102/api/production/";
            token = httpContextAccessor.HttpContext.GetToken();
            this.customHttpClient = customHttpClient;
        }

        public async Task<List<ProductionQueue>> GetProductionQueues()
        {
            var getProductionQueuesUrl = $"{baseUrl}";

            var productionQueues = await customHttpClient.GetStringAsync(getProductionQueuesUrl, token);

            return JsonConvert.DeserializeObject<List<ProductionQueue>>(productionQueues);
        }
    }
}
