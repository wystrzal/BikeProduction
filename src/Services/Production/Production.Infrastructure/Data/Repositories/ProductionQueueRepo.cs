using BikeBaseRepository;
using Production.Core.Interfaces;
using Production.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.Infrastructure.Data.Repositories
{
    public class ProductionQueueRepo : BaseRepository<ProductionQueue, DataContext>, IProductionQueueRepo
    {
        public ProductionQueueRepo(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
