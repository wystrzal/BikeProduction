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

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product, DataContext>, IProductRepository
    {
        public ProductRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
