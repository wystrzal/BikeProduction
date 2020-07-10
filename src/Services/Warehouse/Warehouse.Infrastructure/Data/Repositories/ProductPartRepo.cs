using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.Interfaces;
using Warehouse.Core.Models;

namespace Warehouse.Infrastructure.Data.Repositories
{
    public class ProductPartRepo : BaseRepository<ProductsParts>, IBaseRepository<ProductsParts>, IProductPartRepo
    {
        private readonly DataContext dataContext;

        public ProductPartRepo(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<Part>> GetPartsForCheckAvailability(string reference)
        {
            return await dataContext.ProductsParts.Include(x => x.Product).Include(x => x.Part)
                .Where(x => x.Product.Reference == reference)
                .Select(x => x.Part).ToListAsync();
        }
    }
}
