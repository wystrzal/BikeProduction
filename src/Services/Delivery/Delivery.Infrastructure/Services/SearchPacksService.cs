using BikeSortFilter;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using Delivery.Core.SearchSpecification;
using Delivery.Core.SearchSpecification.FilterClasses;
using Delivery.Core.SearchSpecification.SortClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Delivery.Core.Models.Enums.PackStatusEnum;

namespace Delivery.Infrastructure.Services
{
    public class SearchPacksService : ISearchPacksService
    {
        private readonly ISearchSortFilterService<PackToDelivery, OrderFilteringData> sortFilterService;

        public SearchPacksService(ISearchSortFilterService<PackToDelivery, OrderFilteringData> searchSortFilterService)
        {
            this.sortFilterService = searchSortFilterService;
        }

        public async Task<List<PackToDelivery>> SearchPacks(OrderFilteringData filteringData)
        {
            SetSorting();
            SetFiltering(filteringData);

            return await sortFilterService.Search(true);
        }

        private void SetSorting()
        {
            sortFilterService.SetConcreteSort<SortByDate, DateTime>();
        }

        private void SetFiltering(OrderFilteringData filteringData)
        {
            if (filteringData.PackStatus != 0)
            {
                sortFilterService.SetConcreteFilter<FilterByPackStatus>(filteringData);
            }
        }
    }
}
