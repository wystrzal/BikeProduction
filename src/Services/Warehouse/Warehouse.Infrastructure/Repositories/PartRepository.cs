using BikeBaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositories
{
    public class PartRepository : BaseRepository<Part, DataContext>, IPartRepository
    {
        private readonly DataContext dataContext;

        public PartRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
    }
}
