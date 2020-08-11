using BikeBaseRepository;
using Catalog.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
    }
}
