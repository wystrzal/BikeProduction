using BaseRepository;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositories
{
    public class PartRepository : Repository<Part, DataContext>, IPartRepository
    {
        private readonly DataContext dataContext;

        public PartRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
    }
}
