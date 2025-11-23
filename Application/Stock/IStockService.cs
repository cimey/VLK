using Model.Requests;
using Model.Responses;

namespace Application.Stock
{
    public interface IStockService
    {
        Task<IEnumerable<StockResponse>> GetStocksAsync();

        Task<bool> UpdateStockAsync(UpdateStockRequest updateStockRequest);
    }
}
