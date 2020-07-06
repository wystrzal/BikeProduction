using Microsoft.Extensions.DependencyInjection;
using Order.Core.Interfaces;
using Order.Infrastructure.Data.Repositories;

namespace Order.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IOrderRepository, OrderRepository>();
        }
    }
}
