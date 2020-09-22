using BikeSortFilter;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using CustomerOrder.Core.SearchSpecification;
using CustomerOrder.Core.SearchSpecification.FilterClasses;
using CustomerOrder.Core.SearchSpecification.SortClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrder.Infrastructure.Services
{
    public class SearchOrderService : ISearchOrderService
    {
        private readonly ISearchSortFilterData<Order, FilteringData> sortFilterService;

        public SearchOrderService(ISearchSortFilterData<Order, FilteringData> sortFilterService)
        {
            this.sortFilterService = sortFilterService;
        }

        public async Task<List<Order>> GetOrders(FilteringData filteringData)
        {
            bool orderDesc = true;

            sortFilterService.SetConcreteSort<SortByDate, DateTime>();

            if (filteringData.OrderStatus != 0)
            {
                sortFilterService.SetConcreteFilter<FilterByOrderStatus>(filteringData);
            }

            if (filteringData.UserId != null)
            {
                sortFilterService.SetConcreteFilter<FilterByUserId>(filteringData);
            }

            return await sortFilterService.Search(orderDesc);
        }
    }
}
