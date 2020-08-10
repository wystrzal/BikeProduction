using BikeSortFilter;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Infrastructure.Services.FilterClasses;
using Catalog.Infrastructure.Services.SortClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Services
{
    public class SearchProductService : ISearchProductService
    {
        private readonly ISortFilterService<Product, FilteringData> sortFilterService;

        public SearchProductService(ISortFilterService<Product, FilteringData> sortFilterService)
        {
            this.sortFilterService = sortFilterService;
        }
        public async Task<List<Product>> GetProducts(bool orderDesc, int skip, int take, FilteringData filteringData)
        {
            sortFilterService.SetConcreteFilter(typeof(TestFilter), filteringData);
            sortFilterService.SetConcreteSort<Type, int>(typeof(TestSort));

            return await sortFilterService.Search(orderDesc, skip, take);
        }
    }
}
