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
    public class WarehouseService : IWarehouseService
    {
        private readonly string baseUrl;
        private readonly string token;
        private readonly ICustomHttpClient customHttpClient;

        public WarehouseService(ICustomHttpClient customHttpClient, IHttpContextAccessor httpContextAccessor)
        {
            baseUrl = "http://host.docker.internal:5103/api/warehouse/";
            token = httpContextAccessor.HttpContext.GetToken();
            this.customHttpClient = customHttpClient;
        }

        public async Task<List<Part>> GetParts()
        {
            var getPartsUrl = $"{baseUrl}parts";

            var parts = await customHttpClient.GetStringAsync(getPartsUrl, token);

            return JsonConvert.DeserializeObject<List<Part>>(parts);
        }

        public async Task<Part> GetPart(int partId)
        {
            var getPartUrl = $"{baseUrl}part/{partId}";

            var part = await customHttpClient.GetStringAsync(getPartUrl, token);

            return JsonConvert.DeserializeObject<Part>(part);
        }
    }
}
