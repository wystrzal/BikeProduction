using BikeSortFilter;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Core.SearchSpecification;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Catalog.Core.Models.Enums.BikeTypeEnum;
using static Catalog.Core.Models.Enums.ColorsEnum;
using static Catalog.Core.Models.Enums.SortEnum;

namespace Catalog.Infrastructure.Services
{
    public class SearchProductsService : ISearchProductsService
    {
        private readonly ISearchSortFilterService<Product, FilteringData> sortFilterService;

        public SearchProductsService(ISearchSortFilterService<Product, FilteringData> sortFilterService)
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
                    sortFilterService.SetConcreteSort(x => x.Price);
                    break;
                case Sort.Price_Descending:
                    sortFilterService.SetConcreteSort(x => x.Price);
                    orderDesc = true;
                    break;
                case Sort.Oldest_Added:
                    sortFilterService.SetConcreteSort(x => x.DateAdded);
                    break;
                case Sort.Latest_Added:
                    sortFilterService.SetConcreteSort(x => x.DateAdded);
                    orderDesc = true;
                    break;
                case Sort.The_Most_Popular:
                    sortFilterService.SetConcreteSort(x => x.Popularity);
                    orderDesc = true;
                    break;
                case Sort.The_Least_Popular:
                    sortFilterService.SetConcreteSort(x => x.Popularity);
                    break;
                default:
                    sortFilterService.SetConcreteSort(x => x.ProductName);
                    break;
            }

            return orderDesc;
        }

        private void SetFiltering(FilteringData filteringData)
        {
            if (filteringData.Colors != Colors.All)
            {
                sortFilterService.SetConcreteFilter(x => x.Colors == filteringData.Colors);
            }

            if (filteringData.BrandId != 0)
            {
                sortFilterService.SetConcreteFilter(x => x.BrandId == filteringData.BrandId);
            }

            if (filteringData.BikeType != BikeType.All)
            {
                sortFilterService.SetConcreteFilter(x => x.BikeType == filteringData.BikeType);
            }
        }
    }
}
