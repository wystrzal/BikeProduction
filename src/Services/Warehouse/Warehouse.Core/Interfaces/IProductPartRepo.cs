using BaseRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Models;

namespace Warehouse.Core.Interfaces
{
    public interface IProductPartRepo : IRepository<ProductsParts>
    {
        Task<List<Part>> GetProductParts(string reference);
        Task<ProductsParts> GetProductPart(string reference, int partId);
    }
}
