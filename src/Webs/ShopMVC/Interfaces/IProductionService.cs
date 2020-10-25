using Production.Core.Models;
using ShopMVC.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Interfaces
{
    public interface IProductionService
    {
        Task<List<ProductionQueue>> GetProductionQueues(ProductionQueueFilteringData filteringData);
        Task ConfirmProduction(int productionId);
    }
}
