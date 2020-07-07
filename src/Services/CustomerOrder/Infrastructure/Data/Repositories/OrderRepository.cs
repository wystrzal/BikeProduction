using CustomerOrder.Core.Interfaces;
using CustomerOrder.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrder.Infrastructure.Data.Repositories
{
    public class OrderRepository : BaseRepository<Core.Models.Order>, IBaseRepository<Order>, IOrderRepository
    {
        private readonly DataContext dataContext;

        public OrderRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
    }
}
