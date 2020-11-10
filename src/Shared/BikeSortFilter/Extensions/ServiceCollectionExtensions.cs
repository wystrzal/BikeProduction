using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BikeSortFilter.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTransientSortFilter<TEntity, TDataContext, TFilteringData>(this IServiceCollection services)
            where TEntity : class
            where TDataContext : DbContext
            where TFilteringData : class
        {
            services.AddTransient<ISortFilterRepository<TEntity>, SortFilterRepository<TEntity, TDataContext>>();
            services.AddTransient<ISearchSortFilterService<TEntity, TFilteringData>, SearchSortFilterService<TEntity, TFilteringData>>();
        }

        public static void AddSingletonSortFilter<TEntity, TDataContext, TFilteringData>(this IServiceCollection services)
            where TEntity : class
            where TDataContext : DbContext
            where TFilteringData : class
        {
            services.AddSingleton<ISortFilterRepository<TEntity>, SortFilterRepository<TEntity, TDataContext>>();
            services.AddSingleton<ISearchSortFilterService<TEntity, TFilteringData>, SearchSortFilterService<TEntity, TFilteringData>>();
        }

        public static void AddScopedSortFilter<TEntity, TDataContext, TFilteringData>(this IServiceCollection services)
            where TEntity : class
            where TDataContext : DbContext
            where TFilteringData : class
        {
            services.AddScoped<ISortFilterRepository<TEntity>, SortFilterRepository<TEntity, TDataContext>>();
            services.AddScoped<ISearchSortFilterService<TEntity, TFilteringData>, SearchSortFilterService<TEntity, TFilteringData>>();
        }
    }
}
