using BikeSortFilter;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Core.SearchSpecification;
using Catalog.Core.SearchSpecification.SortClasses;
using Catalog.Infrastructure.Services.FilterClasses;
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
        public async Task<List<Product>> GetProducts(int skip, int take, FilteringData filteringData)
        {
            bool orderDesc = false;

            switch (filteringData.Sort)
            {
                case SortEnum.Sort.SortByPrice:
                    sortFilterService.SetConcreteSort<SortByPrice, decimal>();
                    break;
                case SortEnum.Sort.SortByPriceDesc:
                    sortFilterService.SetConcreteSort<SortByPrice, decimal>();
                    orderDesc = true;
                    break;
                default:
                    sortFilterService.SetConcreteSort<SortByName, string>();
                    break;
            }

            sortFilterService.SetConcreteFilter<TestFilter>(filteringData);
            
            return await sortFilterService.Search(orderDesc, skip, take);
        }
    }
}
