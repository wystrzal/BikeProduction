﻿using BaseRepository;
using Catalog.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Catalog.Core.Models.Enums.HomeProductEnum;

namespace Catalog.Core.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetHomePageProducts(HomeProduct homeProduct);
    }
}
