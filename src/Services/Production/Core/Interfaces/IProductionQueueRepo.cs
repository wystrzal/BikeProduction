using Production.Core.Models;
using Production.Infrastructure.Data;
using Production.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.Core.Interfaces
{
    public interface IProductionQueueRepo : IBaseRepository<ProductionQueue>
    {
    }
}
