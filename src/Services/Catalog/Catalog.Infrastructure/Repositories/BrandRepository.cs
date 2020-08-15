using BikeBaseRepository;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using Catalog.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrastructure.Repositories
{
    public class BrandRepository : BaseRepository<Brand, DataContext>, IBrandRepository
    {
        public BrandRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
