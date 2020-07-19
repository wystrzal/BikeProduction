using BikeBaseRepository;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product, DataContext>, IProductRepository
    {
        public ProductRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
