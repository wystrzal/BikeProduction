﻿using BikeSortFilter;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using Delivery.Core.SearchSpecification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Delivery.Infrastructure.Services
{
    public class SearchLoadingPlacesService : ISearchLoadingPlacesService
    {
        private readonly ISearchSortFilterService<LoadingPlace, LoadingPlaceFilteringData> sortFilterService;

        public SearchLoadingPlacesService(ISearchSortFilterService<LoadingPlace, LoadingPlaceFilteringData> sortFilterService)
        {
            this.sortFilterService = sortFilterService;
        }

        public async Task<List<LoadingPlace>> SearchLoadingPlaces(LoadingPlaceFilteringData filteringData)
        {
            SetFilters(filteringData);

            return await sortFilterService.Search();
        }

        private void SetFilters(LoadingPlaceFilteringData filteringData)
        {
            if (filteringData.LoadingPlaceStatus != 0)
            {
                sortFilterService.SetConcreteFilter(x => x.LoadingPlaceStatus == filteringData.LoadingPlaceStatus);
            }
        }
    }
}
