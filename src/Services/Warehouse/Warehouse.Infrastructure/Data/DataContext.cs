using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Core.Models;

namespace Warehouse.Infrastructure.Data
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
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BikeProductionWarehouseDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductsParts>(pp =>
            {
                pp.HasKey(x => new { x.PartId, x.ProductId } );

                pp.HasOne(x => x.Product)
                .WithMany(x => x.ProductsParts)
                .HasForeignKey(x => x.ProductId);

                pp.HasOne(x => x.Part)
                .WithMany(x => x.ProductsParts)
                .HasForeignKey(x => x.PartId);
            });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<ProductsParts> ProductsParts { get; set; }
        public DbSet<StoragePlace> StoragePlaces { get; set; }
    }
}
