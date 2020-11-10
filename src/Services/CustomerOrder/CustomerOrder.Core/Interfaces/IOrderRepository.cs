using BaseRepository;
using CustomerOrder.Core.Models;

namespace CustomerOrder.Core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}
