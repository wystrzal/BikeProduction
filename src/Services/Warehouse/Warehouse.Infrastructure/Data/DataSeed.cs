using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.Models;

namespace Warehouse.Infrastructure.Data
{
    public static class DataSeed
    {
        public static async Task AddSeed(DataContext dataContext)
        {
            if (!dataContext.StoragePlaces.Any())
            {
                var storagePlaces = new List<StoragePlace>
                {
                    new StoragePlace { Name = "A-0-0" },
                    new StoragePlace { Name = "A-0-1" },
                    new StoragePlace { Name = "A-0-2" },
                    new StoragePlace { Name = "B-0-1" },
                    new StoragePlace { Name = "B-0-2" },
                    new StoragePlace { Name = "B-0-3" },
                };

                await dataContext.StoragePlaces.AddRangeAsync(storagePlaces);
            }

            if (!dataContext.Products.Any())
            {
                var products = new List<Product>()
                {
                    new Product {Reference = "1", Quantity = 5, ProductName = "Red Bike Sigwe"},
                    new Product {Reference = "2", Quantity = 5, ProductName = "Black Bike Hidse"}
                };

                await dataContext.Products.AddRangeAsync(products);
            }

            if (!dataContext.Parts.Any())
            {
                var parts = new List<Part>()
                {
                    new Part {Reference = "15", PartName = "Frame", Quantity = 5, StoragePlaceId = 1 },
                    new Part {Reference = "16", PartName = "Circle", Quantity = 5, StoragePlaceId = 2 },
                    new Part {Reference = "17", PartName = "Saddle", Quantity = 5, StoragePlaceId = 3 },
                    new Part {Reference = "18", PartName = "Wheel", Quantity = 5, StoragePlaceId = 4 },
                    new Part {Reference = "19", PartName = "Chain", Quantity = 5, StoragePlaceId = 5 },
                    new Part {Reference = "20", PartName = "Pedal", Quantity = 5, StoragePlaceId = 6 },
                };

                await dataContext.Parts.AddRangeAsync(parts);
            }

            if (!dataContext.ProductsParts.Any())
            {
                var productsParts = new List<ProductsParts>()
                {
                    new ProductsParts {PartId = 1, ProductId = 1},
                    new ProductsParts {PartId = 2, ProductId = 1},
                    new ProductsParts {PartId = 3, ProductId = 1},
                    new ProductsParts {PartId = 4, ProductId = 1},
                    new ProductsParts {PartId = 5, ProductId = 1},
                    new ProductsParts {PartId = 6, ProductId = 1},
                    new ProductsParts {PartId = 1, ProductId = 2},
                    new ProductsParts {PartId = 2, ProductId = 2},
                    new ProductsParts {PartId = 3, ProductId = 2},
                    new ProductsParts {PartId = 4, ProductId = 2},
                    new ProductsParts {PartId = 5, ProductId = 2},
                    new ProductsParts {PartId = 6, ProductId = 2}
                };

                await dataContext.ProductsParts.AddRangeAsync(productsParts);
            }

            await dataContext.SaveChangesAsync();
        }
    }
}
