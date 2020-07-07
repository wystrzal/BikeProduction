using Production.Core.Interfaces;
using Production.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.Infrastructure.Data.Repositories
{
    public class ProductionQueueRepo : BaseRepository<ProductionQueue>, IProductionQueueRepo
    {
        public ProductionQueueRepo(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
