using ShopMVC.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMVC.Interfaces
{
    public interface IWarehouseService
    {
        Task<List<Part>> GetParts();
        Task<Part> GetPart(int partId);
        Task AddPart(Part part);
        Task DeletePart(int partId);
        Task UpdatePart(Part part);
        Task<List<Part>> GetProductParts(string reference);
    }
}
