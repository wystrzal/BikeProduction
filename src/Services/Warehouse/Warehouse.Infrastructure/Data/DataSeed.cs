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
            if (!dataContext.Products.Any())
            {
                var products = new List<Product>()
                {
                    new Product {Reference = "1", ProductName = "Sigwe"},
                    new Product {Reference = "2", ProductName = "Hidse"},
                    new Product {Reference = "3", ProductName = "Mgise"},
                    new Product {Reference = "4", ProductName = "G-Sier"},
                    new Product {Reference = "5", ProductName = "Siget"},
                    new Product {Reference = "6", ProductName = "Xuder"},
                    new Product {Reference = "7", ProductName = "X-Biser"},
                    new Product {Reference = "8", ProductName = "Dihier"},
                    new Product {Reference = "9", ProductName = "Kisge"},
                    new Product {Reference = "10", ProductName = "Losige"},
                    new Product {Reference = "11", ProductName = "Bisue"},
                    new Product {Reference = "12", ProductName = "Dilis"}
                };

                await dataContext.Products.AddRangeAsync(products);
            }

            if (!dataContext.Parts.Any())
            {
                var parts = new List<Part>()
                {
                    new Part {Reference = "9000", PartName = "Frame", Quantity = 50, QuantityForProduction = 1 },
                    new Part {Reference = "9001", PartName = "Saddle", Quantity = 50, QuantityForProduction = 1 },
                    new Part {Reference = "9002", PartName = "Wheel", Quantity = 50, QuantityForProduction = 2 },
                    new Part {Reference = "9003", PartName = "Chain", Quantity = 50, QuantityForProduction = 1 },
                    new Part {Reference = "9004", PartName = "Pedal", Quantity = 50, QuantityForProduction = 2 },
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

                    new ProductsParts {PartId = 1, ProductId = 2},
                    new ProductsParts {PartId = 2, ProductId = 2},
                    new ProductsParts {PartId = 3, ProductId = 2},
                    new ProductsParts {PartId = 4, ProductId = 2},
                    new ProductsParts {PartId = 5, ProductId = 2},

                    new ProductsParts {PartId = 1, ProductId = 3},
                    new ProductsParts {PartId = 2, ProductId = 3},
                    new ProductsParts {PartId = 3, ProductId = 3},
                    new ProductsParts {PartId = 4, ProductId = 3},
                    new ProductsParts {PartId = 5, ProductId = 3},

                    new ProductsParts {PartId = 1, ProductId = 4},
                    new ProductsParts {PartId = 2, ProductId = 4},
                    new ProductsParts {PartId = 3, ProductId = 4},
                    new ProductsParts {PartId = 4, ProductId = 4},
                    new ProductsParts {PartId = 5, ProductId = 4},

                    new ProductsParts {PartId = 1, ProductId = 5},
                    new ProductsParts {PartId = 2, ProductId = 5},
                    new ProductsParts {PartId = 3, ProductId = 5},
                    new ProductsParts {PartId = 4, ProductId = 5},
                    new ProductsParts {PartId = 5, ProductId = 5},

                    new ProductsParts {PartId = 1, ProductId = 6},
                    new ProductsParts {PartId = 2, ProductId = 6},
                    new ProductsParts {PartId = 3, ProductId = 6},
                    new ProductsParts {PartId = 4, ProductId = 6},
                    new ProductsParts {PartId = 5, ProductId = 6},

                    new ProductsParts {PartId = 1, ProductId = 7},
                    new ProductsParts {PartId = 2, ProductId = 7},
                    new ProductsParts {PartId = 3, ProductId = 7},
                    new ProductsParts {PartId = 4, ProductId = 7},
                    new ProductsParts {PartId = 5, ProductId = 7},

                    new ProductsParts {PartId = 1, ProductId = 8},
                    new ProductsParts {PartId = 2, ProductId = 8},
                    new ProductsParts {PartId = 3, ProductId = 8},
                    new ProductsParts {PartId = 4, ProductId = 8},
                    new ProductsParts {PartId = 5, ProductId = 8},

                    new ProductsParts {PartId = 1, ProductId = 9},
                    new ProductsParts {PartId = 2, ProductId = 9},
                    new ProductsParts {PartId = 3, ProductId = 9},
                    new ProductsParts {PartId = 4, ProductId = 9},
                    new ProductsParts {PartId = 5, ProductId = 9},

                    new ProductsParts {PartId = 1, ProductId = 10},
                    new ProductsParts {PartId = 2, ProductId = 10},
                    new ProductsParts {PartId = 3, ProductId = 10},
                    new ProductsParts {PartId = 4, ProductId = 10},
                    new ProductsParts {PartId = 5, ProductId = 10},

                    new ProductsParts {PartId = 1, ProductId = 11},
                    new ProductsParts {PartId = 2, ProductId = 11},
                    new ProductsParts {PartId = 3, ProductId = 11},
                    new ProductsParts {PartId = 4, ProductId = 11},
                    new ProductsParts {PartId = 5, ProductId = 11},

                    new ProductsParts {PartId = 1, ProductId = 12},
                    new ProductsParts {PartId = 2, ProductId = 12},
                    new ProductsParts {PartId = 3, ProductId = 12},
                    new ProductsParts {PartId = 4, ProductId = 12},
                    new ProductsParts {PartId = 5, ProductId = 12},
                };

                await dataContext.ProductsParts.AddRangeAsync(productsParts);
            }

            await dataContext.SaveChangesAsync();
        }
    }
}
