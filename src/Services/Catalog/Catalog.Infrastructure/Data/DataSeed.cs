using Catalog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public static class DataSeed
    {
        public static async Task AddSeed(DataContext dataContext)
        {
            if (!dataContext.Products.Any())
            {
                var products = new List<Product>()
                {
                    new Product {Reference = "1", Price = 50, ProductName = "Red Bike Sigwe", PhotoUrl="https://unsplash.com/photos/0ahqRV1sJJ4"},
                    new Product {Reference = "2", Price = 50, ProductName = "Black Bike Hidse", PhotoUrl="https://unsplash.com/photos/yjAFnkLtKY0"}
                };

                await dataContext.Products.AddRangeAsync(products);
            }

            

            await dataContext.SaveChangesAsync();
        }
    }
}
