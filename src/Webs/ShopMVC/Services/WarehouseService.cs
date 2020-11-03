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

        public async Task AddPart(Part part)
        {
            var addPartUrl = $"{baseUrl}part";

            await customHttpClient.PostAsync(addPartUrl, part, token);
        }

        public async Task DeletePart(int partId)
        {
            var deletePartUrl = $"{baseUrl}part/{partId}";

            await customHttpClient.DeleteAsync(deletePartUrl, token);
        }

        public async Task UpdatePart(Part part)
        {
            var updatePartUrl = $"{baseUrl}part";

            await customHttpClient.PutAsync(updatePartUrl, part, token);
        }

        public async Task<List<Part>> GetProductParts(string reference)
        {
            var getProductPartsUrl = $"{baseUrl}product/{reference}/parts";

            var parts = await customHttpClient.GetStringAsync(getProductPartsUrl, token);

            return JsonConvert.DeserializeObject<List<Part>>(parts);
        }

        public async Task DeleteProductPart(string reference, int partId)
        {
            var deleteProductPartUrl = $"{baseUrl}product/{reference}/part/{partId}";

            await customHttpClient.DeleteAsync(deleteProductPartUrl, token);
        }
    }
}
