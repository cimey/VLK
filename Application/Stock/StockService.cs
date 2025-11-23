using Domain.Repositories;
using Model.Requests;
using Model.Responses;

namespace Application.Stock
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<IEnumerable<StockResponse>> GetStocksAsync()
        {
            var stocks = await _stockRepository.GetAllAsync();

            return stocks.Select(StockResponse.FromDomain).ToList();
        }

        public async Task<bool> UpdateStockAsync(UpdateStockRequest request)
        {
            var stock = await _stockRepository.GetByIdAsync(request.StockId);

            if (stock == null)
            {
                throw new ArgumentException("Stock not found");
            }

            stock.UpdatePrice(request.Price);
            await _stockRepository.SaveChangesAsync();

            return true;
        }
    }
}
