using BaseRepository;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using CustomerOrder.Infrastructure.Data;

namespace CustomerOrder.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order, DataContext>, IOrderRepository
    {
        private readonly DataContext dataContext;

        public OrderRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
    }
}
