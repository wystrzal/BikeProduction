using BikeSortFilter;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Core.Models.Enums;
using Catalog.Core.SearchSpecification;
using Catalog.Core.SearchSpecification.FilterClasses;
using Catalog.Core.SearchSpecification.SortClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Catalog.Core.Models.Enums.BikeTypeEnum;
using static Catalog.Core.Models.Enums.ColorsEnum;
using static Catalog.Core.Models.Enums.SortEnum;

namespace Catalog.Infrastructure.Services
{
    public class SearchProductService : ISearchProductService
    {
        private readonly ISearchSortFilterService<Product, FilteringData> sortFilterService;

        public SearchProductService(ISearchSortFilterService<Product, FilteringData> sortFilterService)
        {
            this.sortFilterService = sortFilterService;
        }
        public async Task<List<Product>> GetProducts(FilteringData filteringData)
        {
            bool orderDesc = SetSorting(filteringData.Sort);

            SetFiltering(filteringData);

            return await sortFilterService.Search(orderDesc, filteringData.Skip, filteringData.Take);
        }

        private bool SetSorting(Sort sort)
        {
            bool orderDesc = false;

            switch (sort)
            {
                case Sort.Price_Ascending:
                    sortFilterService.SetConcreteSort<SortByPrice, decimal>();
                    break;
                case Sort.Price_Descending:
                    sortFilterService.SetConcreteSort<SortByPrice, decimal>();
                    orderDesc = true;
                    break;
                case Sort.Oldest_Added:
                    sortFilterService.SetConcreteSort<SortByDate, DateTime>();
                    break;
                case Sort.Latest_Added:
                    sortFilterService.SetConcreteSort<SortByDate, DateTime>();
                    orderDesc = true;
                    break;
                case Sort.The_Most_Popular:
                    sortFilterService.SetConcreteSort<SortByPopularity, int>();
                    orderDesc = true;
                    break;
                case Sort.The_Least_Popular:
                    sortFilterService.SetConcreteSort<SortByPopularity, int>();
                    break;
                default:
                    sortFilterService.SetConcreteSort<SortByName, string>();
                    break;
            }

            return orderDesc;
        }

        private void SetFiltering(FilteringData filteringData)
        {
            if (filteringData.Colors != Colors.All)
                sortFilterService.SetConcreteFilter<ColorFilter>(filteringData);

            if (filteringData.BrandId != 0)
                sortFilterService.SetConcreteFilter<BrandFilter>(filteringData);

            if (filteringData.BikeType != BikeType.All)
                sortFilterService.SetConcreteFilter<TypeFilter>(filteringData);
        }
    }
}
