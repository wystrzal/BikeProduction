using BikeBaseRepository;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;

namespace Warehouse.Infrastructure.Data.Repositories
{
    public class PartRepository : BaseRepository<Part, DataContext>, IPartRepository
    {
        public PartRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
