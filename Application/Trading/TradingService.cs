using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Model.Responses;

namespace Application.Trading
{
    public class TradingService : ITradingService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IStockOrderRepository _stockOrderRepository;
        private readonly ILogger<TradingService> _logger;

        public TradingService(
            IClientRepository clientRepository,
            IStockRepository stockRepository,
            IStockOrderRepository stockOrderRepository,
            ILogger<TradingService> logger)
        {
            _clientRepository = clientRepository;
            _stockRepository = stockRepository;
            _stockOrderRepository = stockOrderRepository;
            _logger = logger;
        }

        public async Task<StockOrderResponse> BuyStockAsync(Guid clientId, Guid stockId, int shares)
        {
            if (shares <= 0)
                throw new InvalidOperationException("Shares must be greater than zero.");

            var client = await _clientRepository.GetWithPositionsAsync(clientId)
                         ?? throw new InvalidOperationException("Client not found.");

            var stock = await _stockRepository.GetByIdAsync(stockId)
                         ?? throw new InvalidOperationException("Stock not found.");

            _logger.LogInformation("Client {ClientId} is buying {Shares} shares of {Stock}", clientId, shares, stock.Symbol);

            // Execute Buy in domain
            var order = client.BuyStock(stock, shares);

            // Save the StockOrder
            await _stockOrderRepository.AddAsync(order);

            // Persist changes
            await _clientRepository.SaveChangesAsync();

            _logger.LogInformation("Buy completed: Client {ClientId}, Stock {Stock}, Shares {Shares}, Remaining Balance {Balance}",
                clientId, stock.Symbol, shares, client.CashBalance);

            return StockOrderResponse.FromDomain(order);
        }

        public async Task<StockOrderResponse> SellStockAsync(Guid clientId, Guid stockId, int shares)
        {
            if (shares <= 0)
                throw new InvalidOperationException("Shares must be greater than zero.");

            var client = await _clientRepository.GetWithPositionsAsync(clientId)
                         ?? throw new InvalidOperationException("Client not found.");

            var stock = await _stockRepository.GetByIdAsync(stockId)
                         ?? throw new InvalidOperationException("Stock not found.");

            _logger.LogInformation("Client {ClientId} is selling {Shares} shares of {Stock}", clientId, shares, stock.Symbol);

            // Execute Sell in domain
            var order = client.SellStock(stock, shares);

            // Save the StockOrder
            await _stockOrderRepository.AddAsync(order);

            // Persist changes
            await _clientRepository.SaveChangesAsync();

            _logger.LogInformation("Sell completed: Client {ClientId}, Stock {Stock}, Shares {Shares}, New Balance {Balance}",
                clientId, stock.Symbol, shares, client.CashBalance);

            return StockOrderResponse.FromDomain(order);
        }
    }
}
