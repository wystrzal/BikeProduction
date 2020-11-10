using BikeSortFilter;
using Production.Core.Interfaces;
using Production.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Production.Infrastructure.Services
{
    public class SearchProductionQueuesService : ISearchProductionQueuesService
    {
        private readonly ISearchSortFilterService<ProductionQueue, ProductionQueueFilteringData> sortFilterService;

        public SearchProductionQueuesService(ISearchSortFilterService<ProductionQueue, ProductionQueueFilteringData> sortFilterService)
        {
            this.sortFilterService = sortFilterService;
        }

        public async Task<List<ProductionQueue>> SearchProductionQueues(ProductionQueueFilteringData filteringData)
        {
            SetSorting();
            SetFiltering(filteringData);
            return await sortFilterService.Search();
        }

        private void SetSorting()
        {
            sortFilterService.SetConcreteSort(x => x.ProductionStatus);
        }

        private void SetFiltering(ProductionQueueFilteringData filteringData)
        {
            if (filteringData.ProductionStatus != 0)
            {
                sortFilterService.SetConcreteFilter(x => x.ProductionStatus == filteringData.ProductionStatus);
            }
        }
    }
}
