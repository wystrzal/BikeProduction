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

        public async Task<List<LoadingPlace>> GetLoadingPlaces(LoadingPlaceFilteringData filteringData)
        {
            var getLoadingPlaces = $"{baseUrl}loadingPlaces";

            var queryParams = SetLoadingPlaceQueryParams(filteringData);

            var loadingPlaces = await customHttpClient.GetStringAsync(getLoadingPlaces, token, queryParams);

            return JsonConvert.DeserializeObject<List<LoadingPlace>>(loadingPlaces);
        }

        private Dictionary<string, string> SetLoadingPlaceQueryParams(LoadingPlaceFilteringData filteringData)
        {
            var queryParams = new Dictionary<string, string>();

            if (filteringData.LoadingPlaceStatus != 0)
            {
                queryParams.Add("LoadingPlaceStatus", filteringData.LoadingPlaceStatus.ToString());
            }

            return queryParams;
        }

        public async Task<List<PackToDelivery>> GetPacks(PackFilteringData filteringData)
        {
            var getPacksUrl = $"{baseUrl}packs";

            var queryParams = SetPackQueryParams(filteringData);

            var packs = await customHttpClient.GetStringAsync(getPacksUrl, token, queryParams);

            return JsonConvert.DeserializeObject<List<PackToDelivery>>(packs);
        }

        private Dictionary<string, string> SetPackQueryParams(PackFilteringData filteringData)
        {
            var queryParams = new Dictionary<string, string>();

            if (filteringData.PackStatus != 0)
            {
                queryParams.Add("PackStatus", filteringData.PackStatus.ToString());
            }

            return queryParams;
        }

        public async Task<PackToDelivery> GetPack(int packId)
        {
            var getPackUrl = $"{baseUrl}pack/{packId}";

            var pack = await customHttpClient.GetStringAsync(getPackUrl, token);

            return JsonConvert.DeserializeObject<PackToDelivery>(pack);
        }

        public async Task<LoadingPlace> GetLoadingPlace(int loadingPlaceId)
        {
            var getLoadingPlaceUrl = $"{baseUrl}loadingPlace/{loadingPlaceId}";

            var loadingPlace = await customHttpClient.GetStringAsync(getLoadingPlaceUrl, token);

            return JsonConvert.DeserializeObject<LoadingPlace>(loadingPlace);
        }

        public async Task AddLoadingPlace(LoadingPlace loadingPlace)
        {
            var addLoadingPlaceUrl = $"{baseUrl}add/loadingPlace";

            await customHttpClient.PostAsync(addLoadingPlaceUrl, loadingPlace, token);
        }

        public async Task UpdateLoadingPlace(LoadingPlace loadingPlace)
        {
            var updateLoadingPlaceUrl = $"{baseUrl}update/loadingPlace";

            await customHttpClient.PutAsync(updateLoadingPlaceUrl, loadingPlace, token);
        }

        public async Task DeleteLoadingPlace(int loadingPlaceId)
        {
            var deleteLoadingPlaceUrl = $"{baseUrl}loadingPlace/{loadingPlaceId}";

            await customHttpClient.DeleteAsync(deleteLoadingPlaceUrl, token);
        }
    }
}
