using Production.Core.Models;
using ShopMVC.Areas.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMVC.Interfaces
{
    public interface IProductionService
    {
        Task<List<ProductionQueue>> GetProductionQueues(ProductionQueueFilteringData filteringData);
        Task ConfirmProduction(int productionId);
        Task StartCreatingProducts(int productionId);
        Task FinishProduction(int productionId);
    }
}
