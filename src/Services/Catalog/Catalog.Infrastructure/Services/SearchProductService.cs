using BikeSortFilter;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Core.SearchSpecification;
using Catalog.Core.SearchSpecification.FilterClasses;
using Catalog.Core.SearchSpecification.SortClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Catalog.Core.Models.ColorsEnum;

namespace Catalog.Infrastructure.Services
{
    public class SearchProductService : ISearchProductService
    {
        private readonly ISortFilterService<Product, FilteringData> sortFilterService;

        public SearchProductService(ISortFilterService<Product, FilteringData> sortFilterService)
        {
            this.sortFilterService = sortFilterService;
        }
        public async Task<List<Product>> GetProducts(FilteringData filteringData)
        {
            bool orderDesc = false;

            switch (filteringData.Sort)
            {
                case SortEnum.Sort.Price_Ascending:
                    sortFilterService.SetConcreteSort<SortByPrice, decimal>();
                    break;
                case SortEnum.Sort.Price_Descending:
                    sortFilterService.SetConcreteSort<SortByPrice, decimal>();
                    orderDesc = true;
                    break;
                case SortEnum.Sort.Oldest_Added:
                    sortFilterService.SetConcreteSort<SortByDate, DateTime>();
                    break;
                case SortEnum.Sort.Latest_Added:
                    sortFilterService.SetConcreteSort<SortByDate, DateTime>();
                    orderDesc = true;
                    break;
                case SortEnum.Sort.The_Most_Popular:
                    sortFilterService.SetConcreteSort<SortByPopularity, int>();
                    orderDesc = true;
                    break;
                case SortEnum.Sort.The_Least_Popular:
                    sortFilterService.SetConcreteSort<SortByPopularity, int>();
                    break;
                default:
                    sortFilterService.SetConcreteSort<SortByName, string>();
                    break;
            }

            if (filteringData.Colors != Colors.All)
            {
                sortFilterService.SetConcreteFilter<ColorFilter>(filteringData);
            }

            if (filteringData.BrandId != 0)
            {
                sortFilterService.SetConcreteFilter<BrandFilter>(filteringData);
            }

            
            return await sortFilterService.Search(orderDesc, filteringData.Skip, filteringData.Take);
        }
    }
}
