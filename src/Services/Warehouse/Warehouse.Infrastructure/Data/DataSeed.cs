﻿using System.Collections.Generic;
using System.Linq;
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
                    new Product {Reference = "2", Quantity = 5, ProductName = "Black Bike Hidse"},
                    new Product {Reference = "3", Quantity = 5, ProductName = "Orange-Blue Mgise"},
                    new Product {Reference = "4", Quantity = 5, ProductName = "Orange-White G-Sier"},
                    new Product {Reference = "5", Quantity = 5, ProductName = "Green Siget"},
                    new Product {Reference = "6", Quantity = 5, ProductName = "White Xuder"},
                    new Product {Reference = "7", Quantity = 5, ProductName = "Orange-Black X-Biser"},
                    new Product {Reference = "8", Quantity = 5, ProductName = "Red Dihier"},
                    new Product {Reference = "9", Quantity = 5, ProductName = "Pink Kisge"},
                    new Product {Reference = "10", Quantity = 5, ProductName = "Turquoise Losige"},
                    new Product {Reference = "11", Quantity = 5, ProductName = "Yellow Bisue"},
                    new Product {Reference = "12", Quantity = 5, ProductName = "White Dilis"}
                };

                await dataContext.Products.AddRangeAsync(products);
            }

            if (!dataContext.Parts.Any())
            {
                var parts = new List<Part>()
                {
                    new Part {Reference = "9000", PartName = "Frame", Quantity = 5, StoragePlaceId = 1 },
                    new Part {Reference = "9001", PartName = "Circle", Quantity = 5, StoragePlaceId = 2 },
                    new Part {Reference = "9002", PartName = "Saddle", Quantity = 5, StoragePlaceId = 3 },
                    new Part {Reference = "9003", PartName = "Wheel", Quantity = 5, StoragePlaceId = 4 },
                    new Part {Reference = "9004", PartName = "Chain", Quantity = 5, StoragePlaceId = 5 },
                    new Part {Reference = "9005", PartName = "Pedal", Quantity = 5, StoragePlaceId = 6 },
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
                    new ProductsParts {PartId = 6, ProductId = 2},

                    new ProductsParts {PartId = 1, ProductId = 3},
                    new ProductsParts {PartId = 2, ProductId = 3},
                    new ProductsParts {PartId = 3, ProductId = 3},
                    new ProductsParts {PartId = 4, ProductId = 3},
                    new ProductsParts {PartId = 5, ProductId = 3},
                    new ProductsParts {PartId = 6, ProductId = 3},

                    new ProductsParts {PartId = 1, ProductId = 4},
                    new ProductsParts {PartId = 2, ProductId = 4},
                    new ProductsParts {PartId = 3, ProductId = 4},
                    new ProductsParts {PartId = 4, ProductId = 4},
                    new ProductsParts {PartId = 5, ProductId = 4},
                    new ProductsParts {PartId = 6, ProductId = 4},

                    new ProductsParts {PartId = 1, ProductId = 5},
                    new ProductsParts {PartId = 2, ProductId = 5},
                    new ProductsParts {PartId = 3, ProductId = 5},
                    new ProductsParts {PartId = 4, ProductId = 5},
                    new ProductsParts {PartId = 5, ProductId = 5},
                    new ProductsParts {PartId = 6, ProductId = 5},

                    new ProductsParts {PartId = 1, ProductId = 6},
                    new ProductsParts {PartId = 2, ProductId = 6},
                    new ProductsParts {PartId = 3, ProductId = 6},
                    new ProductsParts {PartId = 4, ProductId = 6},
                    new ProductsParts {PartId = 5, ProductId = 6},
                    new ProductsParts {PartId = 6, ProductId = 6},

                    new ProductsParts {PartId = 1, ProductId = 7},
                    new ProductsParts {PartId = 2, ProductId = 7},
                    new ProductsParts {PartId = 3, ProductId = 7},
                    new ProductsParts {PartId = 4, ProductId = 7},
                    new ProductsParts {PartId = 5, ProductId = 7},
                    new ProductsParts {PartId = 6, ProductId = 7},

                    new ProductsParts {PartId = 1, ProductId = 8},
                    new ProductsParts {PartId = 2, ProductId = 8},
                    new ProductsParts {PartId = 3, ProductId = 8},
                    new ProductsParts {PartId = 4, ProductId = 8},
                    new ProductsParts {PartId = 5, ProductId = 8},
                    new ProductsParts {PartId = 6, ProductId = 8},

                    new ProductsParts {PartId = 1, ProductId = 9},
                    new ProductsParts {PartId = 2, ProductId = 9},
                    new ProductsParts {PartId = 3, ProductId = 9},
                    new ProductsParts {PartId = 4, ProductId = 9},
                    new ProductsParts {PartId = 5, ProductId = 9},
                    new ProductsParts {PartId = 6, ProductId = 9},

                    new ProductsParts {PartId = 1, ProductId = 10},
                    new ProductsParts {PartId = 2, ProductId = 10},
                    new ProductsParts {PartId = 3, ProductId = 10},
                    new ProductsParts {PartId = 4, ProductId = 10},
                    new ProductsParts {PartId = 5, ProductId = 10},
                    new ProductsParts {PartId = 6, ProductId = 10},

                    new ProductsParts {PartId = 1, ProductId = 11},
                    new ProductsParts {PartId = 2, ProductId = 11},
                    new ProductsParts {PartId = 3, ProductId = 11},
                    new ProductsParts {PartId = 4, ProductId = 11},
                    new ProductsParts {PartId = 5, ProductId = 11},
                    new ProductsParts {PartId = 6, ProductId = 11},

                    new ProductsParts {PartId = 1, ProductId = 12},
                    new ProductsParts {PartId = 2, ProductId = 12},
                    new ProductsParts {PartId = 3, ProductId = 12},
                    new ProductsParts {PartId = 4, ProductId = 12},
                    new ProductsParts {PartId = 5, ProductId = 12},
                    new ProductsParts {PartId = 6, ProductId = 12},
                };

                await dataContext.ProductsParts.AddRangeAsync(productsParts);
            }

            await dataContext.SaveChangesAsync();
        }
    }
}
