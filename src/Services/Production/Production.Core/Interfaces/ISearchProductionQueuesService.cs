using Production.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Production.Core.Interfaces
{
    public interface ISearchProductionQueuesService
    {
        Task<List<ProductionQueue>> SearchProductionQueues(ProductionQueueFilteringData filteringData);
    }
}
