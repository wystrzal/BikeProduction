using BikeBaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositories
{
    public class ProductPartRepo : BaseRepository<ProductsParts, DataContext>, IProductPartRepo
    {
        private readonly DataContext dataContext;

        public ProductPartRepo(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<Part>> GetProductParts(string reference)
        {
            return await dataContext.ProductsParts.Include(x => x.Product).Include(x => x.Part)
                .Where(x => x.Product.Reference == reference)
                .Select(x => x.Part).ToListAsync();
        }

        public async Task<ProductsParts> GetProductPart(string reference, int partId)
        {
            return await dataContext.ProductsParts.Include(x => x.Product).Include(x => x.Part)
                .Where(x => x.Product.Reference == reference && x.PartId == partId).FirstOrDefaultAsync();
        }
    }
}
