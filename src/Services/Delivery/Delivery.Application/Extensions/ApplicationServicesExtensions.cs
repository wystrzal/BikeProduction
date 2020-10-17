using BikeHttpClient;
using BikeSortFilter;
using Delivery.Core.Interfaces;
using Delivery.Core.Models;
using Delivery.Core.SearchSpecification;
using Delivery.Infrastructure.Data;
using Delivery.Infrastructure.Repositories;
using Delivery.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Application.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ICustomHttpClient, CustomHttpClient>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICustomerOrderService, CustomerOrderService>();
            services.AddTransient<IPackToDeliveryRepo, PackToDeliveryRepo>();
            services.AddTransient<ISortFilterRepository<PackToDelivery>, SortFilterRepository<PackToDelivery, DataContext>>();
            services.AddTransient<ISearchSortFilterService<PackToDelivery, FilteringData>, 
                SearchSortFilterService<PackToDelivery, FilteringData>>();
            services.AddTransient<ISearchPacksService, SearchPacksService>();
        }
    }
}
