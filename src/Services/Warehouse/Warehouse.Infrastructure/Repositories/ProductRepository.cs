using BaseRepository;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product, DataContext>, IProductRepository
    {
        public ProductRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
