﻿using Delivery.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DataContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=mssqlserver;Database=DeliveryDb;User Id=sa;Password=P@ssword");
            }
        }

        public DbSet<LoadingPlace> LoadingPlaces { get; set; }
        public DbSet<PackToDelivery> PacksToDelivery { get; set; }
    }
}
