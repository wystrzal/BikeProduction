using BikeBaseRepository;
using Production.Core.Interfaces;
using Production.Core.Models;
using Production.Infrastructure.Data;

namespace Production.Infrastructure.Repositories
{
    public class ProductionQueueRepo : BaseRepository<ProductionQueue, DataContext>, IProductionQueueRepo
    {
        public ProductionQueueRepo(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
