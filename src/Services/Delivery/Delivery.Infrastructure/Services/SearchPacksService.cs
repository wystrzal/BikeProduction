using BikeSortFilter;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using Delivery.Core.SearchSpecification;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            sortFilterService.SetConcreteSort(x => x.Date);
        }

        private void SetFiltering(OrderFilteringData filteringData)
        {
            if (filteringData.PackStatus != 0)
            {
                sortFilterService.SetConcreteFilter(x => x.PackStatus == filteringData.PackStatus);
            }
        }
    }
}
