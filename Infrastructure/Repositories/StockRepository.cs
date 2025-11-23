using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StockRepository : RepositoryBase<Stock, Guid>, IStockRepository
    {
        public StockRepository(VLKContext context) : base(context) { }

        public override Task<List<Stock>> GetAllAsync()
        {
            return _dbSet.Include(x => x.History).ToListAsync();
        }

        public override async Task<Stock?> GetByIdAsync(Guid id)
        {
            return await _dbSet.Include(x => x.History).FirstOrDefaultAsync(x => x.Id == id);
        }
    }

    public class StockOrderRepository : RepositoryBase<StockOrder, Guid>, IStockOrderRepository
    {
        public StockOrderRepository(VLKContext context) : base(context) { }

        public async Task<List<StockOrder>> GetOrdersByClientAsync(Guid clientId)
        {
            return await _dbSet
                .Where(o => o.ClientId == clientId)
                .ToListAsync();
        }
    }
}
