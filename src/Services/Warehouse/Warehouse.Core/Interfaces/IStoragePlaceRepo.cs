using BikeBaseRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Models;

namespace Warehouse.Core.Interfaces
{
    public interface IStoragePlaceRepo : IBaseRepository<StoragePlace>
    {
        Task<IEnumerable<StoragePlace>> GetStoragePlaces();
    }
}
