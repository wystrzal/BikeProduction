using BikeSortFilter;
using CustomerOrder.Core.Models;
using CustomerOrder.Core.SearchSpecification;
using CustomerOrder.Core.SearchSpecification.FilterClasses;
using CustomerOrder.Core.SearchSpecification.SortClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrder.Infrastructure
{
    public class SearchOrderSerivce
    {
        private readonly ISortFilterService<Order, FilteringData> sortFilterService;

        public SearchOrderSerivce(ISortFilterService<Order, FilteringData> sortFilterService)
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

            return await sortFilterService.Search(orderDesc, filteringData.Skip, filteringData.Take);
        }
    }
}
