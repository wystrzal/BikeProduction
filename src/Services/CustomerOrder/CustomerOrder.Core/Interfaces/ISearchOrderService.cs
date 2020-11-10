using CustomerOrder.Core.Models;
using CustomerOrder.Core.SearchSpecification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerOrder.Core.Interfaces
{
    public interface ISearchOrderService
    {
        Task<List<Order>> GetOrders(FilteringData filteringData);
    }
}
