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
            sortFilterService.SetConcreteSort<SortByDate, DateTime>();
        }

        private void SetFiltering(FilteringData filteringData)
        {
            if (filteringData.OrderStatus != 0)
            {
                sortFilterService.SetConcreteFilter<FilterByOrderStatus>(filteringData);
            }

            if (filteringData.UserId != null)
            {
                sortFilterService.SetConcreteFilter<FilterByUserId>(filteringData);
            }
        }
    }
}
