using Catalog.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Interfaces
{
    public interface ISearchProductService
    {
        Task<List<Product>> GetProducts(bool orderDesc, int skip, int take, FilteringData filteringData);
    }
}
