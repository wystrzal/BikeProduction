﻿using ShopMVC.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Interfaces
{
    public interface IDeliveryService
    {
        Task<List<PackToDelivery>> GetPacks(PackFilteringData filteringData);
        Task<PackToDelivery> GetPack(int packId);
        Task<List<LoadingPlace>> GetLoadingPlaces(LoadingPlaceFilteringData filteringData);
        Task<LoadingPlace> GetLoadingPlace(int loadingPlaceId);
        Task AddLoadingPlace(LoadingPlace loadingPlace);
    }
}
