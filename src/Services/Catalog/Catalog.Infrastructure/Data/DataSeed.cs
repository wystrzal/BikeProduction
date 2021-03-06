﻿using Catalog.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Catalog.Core.Models.Enums.BikeTypeEnum;
using static Catalog.Core.Models.Enums.ColorsEnum;

namespace Catalog.Infrastructure.Data
{
    public static class DataSeed
    {
        public static async Task AddSeed(DataContext dataContext)
        {
            if (!dataContext.Brands.Any())
            {
                var brands = new List<Brand>()
                {
                    new Brand { Name = "Brand-One" },
                    new Brand { Name = "Brand-X" },
                    new Brand { Name = "Brand-Y" }
                };

                await dataContext.Brands.AddRangeAsync(brands);
            }

            if (!dataContext.Products.Any())
            {
                var products = new List<Product>()
                {
                    new Product {Reference = "1", BikeType = BikeType.Road_Bike, Popularity = 5, BrandId = 1, Price = 150, ProductName = "Sigwe", Colors = Colors.Red, PhotoUrl="https://images.unsplash.com/photo-1517949908114-71669a64d885?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=750&q=80"},
                    new Product {Reference = "2", BikeType = BikeType.Fitness_Bike, Popularity = 10, BrandId = 1, Price = 50, ProductName = "Hidse", Colors = Colors.Black, PhotoUrl="https://images.unsplash.com/photo-1532298229144-0ec0c57515c7?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=708&q=80"},
                    new Product {Reference = "3", BikeType = BikeType.Mountain_Bike, Popularity = 5, BrandId = 2, Price = 250, ProductName = "Mgise", Colors = Colors.Blue, PhotoUrl="https://images.unsplash.com/photo-1507035895480-2b3156c31fc8?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=750&q=80"},
                    new Product {Reference = "4", BikeType = BikeType.Mountain_Bike, Popularity = 1, BrandId = 3, Price = 350, ProductName = "G-Sier", Colors = Colors.White, PhotoUrl="https://images.unsplash.com/photo-1485965120184-e220f721d03e?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=750&q=80"},
                    new Product {Reference = "5", BikeType = BikeType.Road_Bike, Popularity = 55, BrandId = 1, Price = 50, ProductName = "Siget", Colors = Colors.Green, PhotoUrl="https://images.unsplash.com/photo-1569943228307-a66beab7cd96?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=719&q=80"},
                    new Product {Reference = "6", BikeType = BikeType.Mountain_Bike, Popularity = 5, BrandId = 3, Price = 100, ProductName = "Xuder", Colors = Colors.White, PhotoUrl="https://images.unsplash.com/photo-1505705694340-019e1e335916?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=889&q=80"},
                    new Product {Reference = "7", BikeType = BikeType.Fitness_Bike, Popularity = 15, BrandId = 2, Price = 200, ProductName = "X-Biser", Colors = Colors.Black, PhotoUrl="https://images.unsplash.com/photo-1571333250630-f0230c320b6d?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=750&q=80"},
                    new Product {Reference = "8", BikeType = BikeType.E_Bike, Popularity = 2, BrandId = 1, Price = 400, ProductName = "Dihier", Colors = Colors.Red, PhotoUrl="https://images.unsplash.com/photo-1523740856324-f2ce89135981?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=798&q=80"},
                    new Product {Reference = "9", BikeType = BikeType.Road_Bike, Popularity = 22, BrandId = 1, Price = 500, ProductName = "Kisge", Colors = Colors.Pink, PhotoUrl="https://images.unsplash.com/photo-1475524795093-57e7009e0b24?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=750&q=80"},
                    new Product {Reference = "10", BikeType = BikeType.Road_Bike, Popularity = 1, BrandId = 3, Price = 150, ProductName = "Losige", Colors = Colors.Turquoise, PhotoUrl="https://images.unsplash.com/photo-1553978458-e039e4a68999?ixlib=rb-1.2.1&auto=format&fit=crop&w=750&q=80"},
                    new Product {Reference = "11", BikeType = BikeType.Mountain_Bike, Popularity = 5, BrandId = 1, Price = 120, ProductName = "Bisue", Colors = Colors.Yellow, PhotoUrl="https://images.unsplash.com/photo-1573505820103-bdca39cc6ddc?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=500&q=60"},
                    new Product {Reference = "12", BikeType = BikeType.Fitness_Bike, Popularity = 100, BrandId = 2, Price = 270, ProductName = "Dilis", Colors = Colors.White, PhotoUrl="https://images.unsplash.com/photo-1501857333393-a4963b840b49?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=500&q=60"}
                };

                await dataContext.Products.AddRangeAsync(products);
            }



            await dataContext.SaveChangesAsync();
        }
    }
}
