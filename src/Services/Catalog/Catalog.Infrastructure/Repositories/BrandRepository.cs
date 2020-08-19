using BikeBaseRepository;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Repositories
{
    public class BrandRepository : BaseRepository<Brand, DataContext>, IBrandRepository
    {
        public BrandRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
