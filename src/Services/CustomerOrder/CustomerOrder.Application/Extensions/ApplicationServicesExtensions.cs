using BikeBaseRepository;
using BikeSortFilter;
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
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ISearchOrderService, SearchOrderService>();
            services.AddTransient<IBaseRepository<Order>, BaseRepository<Order, DataContext>>();
            services.AddTransient<ISearchSortFilterData<Order, FilteringData>, SearchSortFilterData<Order, FilteringData>>();
        }
    }
}
