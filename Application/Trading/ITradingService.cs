using Domain.Entities;
using Model.Dtos;
using Model.Responses;

namespace Application.Trading
{
    public interface ITradingService
    {
        Task<StockOrderResponse> BuyStockAsync(Guid clientId, Guid stockId, int shares);
        Task<StockOrderResponse> SellStockAsync(Guid clientId, Guid stockId, int shares);
    }
}
