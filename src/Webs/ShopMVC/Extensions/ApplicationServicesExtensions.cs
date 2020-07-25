using BikeHttpClient;
using Microsoft.Extensions.DependencyInjection;
using ShopMVC.Interfaces;
using ShopMVC.Services;

namespace ShopMVC.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ICatalogService, CatalogService>();
            services.AddTransient<IBasketService, BasketService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICustomHttpClient, CustomHttpClient>();
        }
    }
}
