using BikeBaseRepository;
using Catalog.Core.Exceptions;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Catalog.Core.Models.Enums.HomeProductEnum;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product, DataContext>, IProductRepository
    {
        private readonly DataContext dataContext;
        private readonly ILogger<ProductRepository> logger;

        public ProductRepository(DataContext dataContext, ILogger<ProductRepository> logger) : base(dataContext)
        {
            this.dataContext = dataContext;
            this.logger = logger;
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

        public async Task<Product> GetProductByReference(string reference)
        {
            var product = await dataContext.Products.Where(x => x.Reference == reference).FirstOrDefaultAsync();

            if (product == null)
            {
                var exception = new ProductNotFoundException();
                logger.LogError($"{exception.Message}");
                throw exception;
            }

            return product;
        }
    }
}
