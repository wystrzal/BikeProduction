using BikeBaseRepository;
using BikeSortFilter;
using BikeSortFilter.Extensions;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Core.SearchSpecification;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using Catalog.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransientSortFilter<Product, DataContext, FilteringData>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<ISearchProductsService, SearchProductsService>();
            services.AddTransient<IChangeProductsPopularityService, ChangeProductsPopularityService>();
        }
    }
}
