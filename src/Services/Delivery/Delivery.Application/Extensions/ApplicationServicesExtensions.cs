using BikeHttpClient;
using Delivery.Core.Interfaces;
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICustomerOrderService, CustomerOrderService>();
            services.AddTransient<IPackToDeliveryRepo, PackToDeliveryRepo>();
        }
    }
}
