using BikeBaseRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositories
{
    public class StoragePlaceRepo : BaseRepository<StoragePlace, DataContext>, IStoragePlaceRepo
    {
        private readonly DataContext dataContext;

        public StoragePlaceRepo(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<IEnumerable<StoragePlace>> GetStoragePlaces()
        {
            return await dataContext.StoragePlaces.Include(x => x.Part).ToListAsync();
        }
    }
}
