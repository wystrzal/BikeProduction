using CustomerOrder.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrder.Infrastructure.Data
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
                optionsBuilder.UseSqlServer("Server=mssqlserver;Database=CustomerOrderDb;User Id=sa;Password=P@ssword");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>(order =>
            {
                order.HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
