using BikeSortFilter;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using CustomerOrder.Core.SearchSpecification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerOrder.Infrastructure.Services
{
    public class SearchOrderService : ISearchOrderService
    {
        private readonly ISearchSortFilterService<Order, FilteringData> sortFilterService;

        public SearchOrderService(ISearchSortFilterService<Order, FilteringData> sortFilterService)
        {
            this.sortFilterService = sortFilterService;
        }

        public async Task<List<Order>> GetOrders(FilteringData filteringData)
        {
            bool orderDesc = true;

            SetSorting();

            SetFiltering(filteringData);

            return await sortFilterService.Search(orderDesc);
        }

        private void SetSorting()
        {
            sortFilterService.SetConcreteSort(x => x.OrderDate);
        }

        private void SetFiltering(FilteringData filteringData)
        {
            if (filteringData.OrderStatus != 0)
            {
                sortFilterService.SetConcreteFilter(x => x.OrderStatus == filteringData.OrderStatus);
            }

            if (filteringData.UserId != null)
            {
                sortFilterService.SetConcreteFilter(x => x.UserId == filteringData.UserId);
            }
        }
    }
}
