using Domain.Entities;

namespace Domain.Repositories
{
    public interface IStockRepository : IRepositoryBase<Stock, Guid> { }

    public interface IStockOrderRepository : IRepositoryBase<StockOrder, Guid>
    {
        Task<List<StockOrder>> GetOrdersByClientAsync(Guid clientId);
    }

    public interface IClientStockPositionRepository : IRepositoryBase<ClientStockPosition, Guid>
    {
        Task<ClientStockPosition?> GetPositionAsync(Guid clientId, Guid stockId);
    }
}
