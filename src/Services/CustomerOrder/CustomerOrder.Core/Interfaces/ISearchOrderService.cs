using CustomerOrder.Core.Models;
using CustomerOrder.Core.SearchSpecification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrder.Core.Interfaces
{
    public interface ISearchOrderService
    {
        Task<List<Order>> GetOrders(FilteringData filteringData);
    }
}
