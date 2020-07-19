using BikeBaseRepository;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Core.Models;

namespace Warehouse.Core.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
    }
}
