using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.Models;

namespace Warehouse.Core.Interfaces
{
    public interface IProductPartRepo : IBaseRepository<ProductsParts>
    {
        Task<List<Part>> GetPartsForCheckAvailability(string reference);
    }
}
