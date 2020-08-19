using BikeBaseRepository;
using System.Threading.Tasks;
using Warehouse.Core.Models;

namespace Warehouse.Core.Interfaces
{
    public interface IPartRepository : IBaseRepository<Part>
    {
        Task<Part> GetPart(int id);
    }
}
