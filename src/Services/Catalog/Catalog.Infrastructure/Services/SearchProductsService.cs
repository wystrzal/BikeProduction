using BikeSortFilter;
using Catalog.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrastructure.Services
{
    public class SearchProductsService
    {
        private readonly ISortFilterService<Product> sortFilterService;

        public SearchProductsService(ISortFilterService<Product> sortFilterService)
        {
            this.sortFilterService = sortFilterService;
        }


    }
}
