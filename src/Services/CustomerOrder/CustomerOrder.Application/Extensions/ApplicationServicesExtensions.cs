using BikeBaseRepository;
using BikeSortFilter;
using BikeSortFilter.Extensions;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using CustomerOrder.Core.SearchSpecification;
using CustomerOrder.Infrastructure.Data;
using CustomerOrder.Infrastructure.Repositories;
using CustomerOrder.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerOrder.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransientSortFilter<Order, DataContext, FilteringData>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ISearchOrderService, SearchOrderService>();
        }
    }
}
