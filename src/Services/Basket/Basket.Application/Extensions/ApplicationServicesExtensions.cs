using Basket.Core.Interfaces;
using Basket.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Application.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IDistributedCache, RedisCache>();
            services.AddTransient<IBasketRedisService, BasketRedisService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
