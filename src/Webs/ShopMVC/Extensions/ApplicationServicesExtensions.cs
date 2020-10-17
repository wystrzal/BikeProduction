using BikeHttpClient;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ShopMVC.Filters;
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
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IDeliveryService, DeliveryService>();
            services.AddTransient<ICustomHttpClient, CustomHttpClient>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ModelErrorsResultFilter>();
            services.AddTransient<ICookieAuthentication, CookieAuthentication>();
        }
    }
}
