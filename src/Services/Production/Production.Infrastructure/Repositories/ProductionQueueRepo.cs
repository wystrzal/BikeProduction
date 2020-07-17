using BikeBaseRepository;
using Production.Core.Interfaces;
using Production.Core.Models;
using Production.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.Infrastructure.Repositories
{
    public class ProductionQueueRepo : BaseRepository<ProductionQueue, DataContext>, IProductionQueueRepo
    {
        public ProductionQueueRepo(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
