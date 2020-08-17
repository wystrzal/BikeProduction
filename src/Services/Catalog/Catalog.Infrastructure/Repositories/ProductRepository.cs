using BikeBaseRepository;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Catalog.Core.Models.Enums.HomeProductEnum;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product, DataContext>, IProductRepository
    {
        private readonly DataContext dataContext;

        public ProductRepository(DataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<Product>> GetHomePageProducts(HomeProduct homeProduct)
        {
            if (homeProduct == HomeProduct.NewProduct)
            {
                return await dataContext.Products.OrderByDescending(x => x.DateAdded).Take(10).ToListAsync();
            }
            else
            {
                return await dataContext.Products.OrderByDescending(x => x.Popularity).Take(10).ToListAsync();
            }
        }
    }
}
