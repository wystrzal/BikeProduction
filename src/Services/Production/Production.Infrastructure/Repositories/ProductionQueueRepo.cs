using BaseRepository;
using Production.Core.Interfaces;
using Production.Core.Models;
using Production.Infrastructure.Data;

namespace Production.Infrastructure.Repositories
{
    public class ProductionQueueRepo : Repository<ProductionQueue, DataContext>, IProductionQueueRepo
    {
        public ProductionQueueRepo(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
