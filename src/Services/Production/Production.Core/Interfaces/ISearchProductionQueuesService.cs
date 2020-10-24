using Production.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Production.Core.Interfaces
{
    public interface ISearchProductionQueuesService
    {
        Task<List<ProductionQueue>> SearchProductionQueues(ProductionQueueFilteringData filteringData);
    }
}
