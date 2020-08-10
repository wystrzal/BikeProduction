using BikeBaseRepository;
using BikeSortFilter;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
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
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISortFilterService<Product, FilteringData>, SortFilterService<Product, FilteringData>>();
            services.AddTransient<IBaseRepository<Product>, BaseRepository<Product, DataContext>>();
            services.AddTransient<ISearchProductService, SearchProductService>();
        }
    }
}
