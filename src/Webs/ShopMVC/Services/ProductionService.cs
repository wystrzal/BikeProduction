using BikeHttpClient;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Production.Core.Models;
using ShopMVC.Areas.Admin.Models;
using ShopMVC.Extensions;
using ShopMVC.Interfaces;
using System.Collections.Generic;
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

        public async Task<List<ProductionQueue>> GetProductionQueues(ProductionQueueFilteringData filteringData)
        {
            var getProductionQueuesUrl = $"{baseUrl}";

            var queryParams = SetQueryParams(filteringData);

            var productionQueues = await customHttpClient.GetStringAsync(getProductionQueuesUrl, token, queryParams);

            return JsonConvert.DeserializeObject<List<ProductionQueue>>(productionQueues);
        }

        private Dictionary<string, string> SetQueryParams(ProductionQueueFilteringData filteringData)
        {
            var queryParams = new Dictionary<string, string>();

            if (filteringData.ProductionStatus != 0)
            {
                queryParams.Add("ProductionStatus", filteringData.ProductionStatus.ToString());
            }

            return queryParams;
        }

        public async Task ConfirmProduction(int productionId)
        {
            var confirmProductionUrl = $"{baseUrl}confirm/{productionId}";

            await customHttpClient.PostAsync(confirmProductionUrl, (ProductionQueue)null, token);
        }

        public async Task StartCreatingProducts(int productionId)
        {
            var startCreatingProductsUrl = $"{baseUrl}start/{productionId}";

            await customHttpClient.PostAsync(startCreatingProductsUrl, (ProductionQueue)null, token);
        }

        public async Task FinishProduction(int productionId)
        {
            var finishProductionUrl = $"{baseUrl}finish/{productionId}";

            await customHttpClient.PostAsync(finishProductionUrl, (ProductionQueue)null, token);
        }
    }
}
