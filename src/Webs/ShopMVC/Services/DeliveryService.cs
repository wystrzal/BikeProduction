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
    public class DeliveryService : IDeliveryService
    {
        private readonly string baseUrl;
        private readonly string token;
        private readonly ICustomHttpClient customHttpClient;

        public DeliveryService(ICustomHttpClient customHttpClient, IHttpContextAccessor httpContextAccessor)
        {
            this.customHttpClient = customHttpClient;
            token = httpContextAccessor.HttpContext.GetToken();
            baseUrl = "http://host.docker.internal:5104/api/delivery/";
        }

        public async Task<List<PackToDelivery>> GetPacks(PackFilteringData filteringData)
        {
            var getPacksUrl = $"{baseUrl}packs";

            var queryParams = SetQueryParams(filteringData);

            var packs = await customHttpClient.GetStringAsync(getPacksUrl, token, queryParams);

            return JsonConvert.DeserializeObject<List<PackToDelivery>>(packs);
        }

        private Dictionary<string, string> SetQueryParams(PackFilteringData filteringData)
        {
            var queryParams = new Dictionary<string, string>();

            if (filteringData.PackStatus != 0)
            {
                queryParams.Add("PackStatus", filteringData.PackStatus.ToString());
            }

            return queryParams;
        }
    }
}
