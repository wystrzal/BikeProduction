using Microsoft.Extensions.DependencyInjection;
using Production.Core.Interfaces;
using Production.Infrastructure.Data.Repositories;

namespace Production.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IProductionQueueRepo, ProductionQueueRepo>();
        }
    }
}
