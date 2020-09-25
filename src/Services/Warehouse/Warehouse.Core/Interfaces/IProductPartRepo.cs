using BikeBaseRepository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Models;

namespace Warehouse.Core.Interfaces
{
    public interface IProductPartRepo : IBaseRepository<ProductsParts>
    {
        Task<List<Part>> GetPartsForProduction(string reference);
    }
}
