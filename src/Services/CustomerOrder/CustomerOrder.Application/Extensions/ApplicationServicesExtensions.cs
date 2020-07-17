using CustomerOrder.Core.Interfaces;
using CustomerOrder.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerOrder.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IOrderRepository, OrderRepository>();
        }
    }
}
