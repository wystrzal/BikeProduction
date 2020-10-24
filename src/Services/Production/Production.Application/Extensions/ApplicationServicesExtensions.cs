using BikeSortFilter.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Production.Core.Interfaces;
using Production.Core.Models;
using Production.Infrastructure.Data;
using Production.Infrastructure.Repositories;
using Production.Infrastructure.Services;

namespace Production.Application.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IProductionQueueRepo, ProductionQueueRepo>();
            services.AddTransient<ISearchProductionQueuesService, SearchProductionQueuesService>();
            services.AddTransientSortFilter<ProductionQueue, DataContext, ProductionQueueFilteringData>();
        }
    }
}
