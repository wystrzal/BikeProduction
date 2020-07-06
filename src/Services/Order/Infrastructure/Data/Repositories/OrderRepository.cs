using Order.Core.Interfaces;
using Order.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Infrastructure.Data.Repositories
{
    public class OrderRepository : BaseRepository<Orders>, IBaseRepository<Orders>, IOrderRepository
    {
        private readonly DataContext dataContext;

        public OrderRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
    }
}
