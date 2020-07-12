using BikeHttpClient;
using Delivery.Core.Interfaces;
using Delivery.Infrastructure.Data.Repositories;
using Delivery.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Application.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ICustomHttpClient, CustomHttpClient>();
            services.AddTransient<ICustomerOrderService, CustomerOrderService>();
            services.AddTransient<IPackToDeliveryRepo, PackToDeliveryRepo>();
        }
    }
}
