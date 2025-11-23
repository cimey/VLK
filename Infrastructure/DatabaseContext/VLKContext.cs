using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseContext
{
    public class VLKContext : DbContext
    {
        public VLKContext(DbContextOptions<VLKContext> options) : base(options) { }

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<ClientStockPosition> ClientStockPositions => Set<ClientStockPosition>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<StockOrder> StockOrders => Set<StockOrder>();

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new ClientStockPositionConfiguration());
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new StockOrderConfiguration()); 
            modelBuilder.ApplyConfiguration(new StockHistoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
