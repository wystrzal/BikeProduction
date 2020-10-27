using Microsoft.EntityFrameworkCore;
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
                optionsBuilder.UseSqlServer("Server=mssqlserver;Database=WarehouseDb;User Id=sa;Password=P@ssword");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductsParts>(pp =>
            {
                pp.HasKey(x => new { x.PartId, x.ProductId });

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
    }
}
