using Microsoft.EntityFrameworkCore;
using Production.Core.Models;

namespace Production.Infrastructure.Data
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
                optionsBuilder.UseSqlServer("Server=mssqlserver;Database=ProductionDb;User Id=sa;Password=P@ssword");
            }
        }

        public DbSet<ProductionQueue> ProductionQueues { get; set; }
    }
}
