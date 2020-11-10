using BaseRepository;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Catalog.Core.Models.Enums.HomeProductEnum;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product, DataContext>, IProductRepository
    {
        private readonly DataContext dataContext;

        public ProductRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<Product>> GetHomePageProducts(HomeProduct homeProduct)
        {
            switch (homeProduct)
            {
                case HomeProduct.NewProduct:
                    return await dataContext.Products.OrderByDescending(x => x.DateAdded).Take(10).ToListAsync();
                case HomeProduct.PopularProduct:
                    return await dataContext.Products.OrderByDescending(x => x.Popularity).Take(10).ToListAsync();
                default:
                    return null;
            }
        }
    }
}
