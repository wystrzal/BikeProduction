using BikeBaseRepository;
using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using CustomerOrder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerOrder.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order, DataContext>, IOrderRepository
    {
        private readonly DataContext dataContext;

        public OrderRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<Order>> GetOrders()
        {
            return await dataContext.Orders.Include(x => x.OrderItems).ToListAsync();
        }
    }
}
