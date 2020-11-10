using Delivery.Core.Models;
using Delivery.Core.SearchSpecification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Delivery.Core.Interfaces
{
    public interface ISearchPacksService
    {
        Task<List<PackToDelivery>> SearchPacks(OrderFilteringData filteringData);
    }
}
