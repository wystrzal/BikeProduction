﻿using Catalog.Core.Models;
using Catalog.Core.SearchSpecification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Core.Interfaces
{
    public interface ISearchProductService
    {
        Task<List<Product>> GetProducts(FilteringData filteringData);
    }
}
