using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ClientRepository : RepositoryBase<Client, Guid>, IClientRepository
    {
        public ClientRepository(VLKContext context) : base(context) { }

        public async Task<Client?> GetWithPositionsAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Positions)
                .ThenInclude(p => p.Stock)
                .AsSplitQuery()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }

    public class ClientStockPositionRepository : RepositoryBase<ClientStockPosition, Guid>, IClientStockPositionRepository
    {
        public ClientStockPositionRepository(VLKContext context) : base(context) { }

        public async Task<ClientStockPosition?> GetPositionAsync(Guid clientId, Guid stockId)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.ClientId == clientId && p.StockId == stockId);
        }
    }
}
