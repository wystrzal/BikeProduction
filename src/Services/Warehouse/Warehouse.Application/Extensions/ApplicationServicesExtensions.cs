﻿using Microsoft.Extensions.DependencyInjection;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Warehouse.Infrastructure.Data.Repositories;

namespace Warehouse.Application.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IProductPartRepo, ProductPartRepo>();
        }
    }
}
