using BikeBaseRepository;
using CustomerOrder.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerOrder.Core.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<Order>> GetOrders();
    }
}
