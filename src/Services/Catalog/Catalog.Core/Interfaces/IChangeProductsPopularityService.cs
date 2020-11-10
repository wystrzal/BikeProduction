using Catalog.Core.Models.MessagingModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Core.Interfaces
{
    public interface IChangeProductsPopularityService
    {
        Task ChangeProductsPopularity(List<OrderItem> orderItems, bool increasePopularity);
    }
}
