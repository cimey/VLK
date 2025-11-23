using Application.Trading;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTest.Application
{
    public class TradingServiceTests
    {
        private readonly Mock<IClientRepository> _clientRepoMock = new();
        private readonly Mock<IStockRepository> _stockRepoMock = new();
        private readonly Mock<IStockOrderRepository> _orderRepoMock = new();
        private readonly Mock<ILogger<TradingService>> _loggerMock = new();

        private TradingService CreateService() =>
            new TradingService(_clientRepoMock.Object, _stockRepoMock.Object, _orderRepoMock.Object, _loggerMock.Object);

        [Fact]
        public async Task BuyStockAsync_Should_Create_Order_And_Update_Cash()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var stockId = Guid.NewGuid();
            var stock = new Stock(stockId, "AAPL", "Apple", 100m);
            var client = new Client(clientId, 500m, "Alice", "user@vlk.com");

            _clientRepoMock.Setup(r => r.GetWithPositionsAsync(clientId)).ReturnsAsync(client);
            _stockRepoMock.Setup(r => r.GetByIdAsync(stockId)).ReturnsAsync(stock);

            var service = CreateService();

            // Act
            var result = await service.BuyStockAsync(clientId, stockId, 2);

            // Assert
            result.ClientId.Should().Be(clientId);
            result.StockId.Should().Be(stockId);
            result.Shares.Should().Be(2);
            result.TotalPrice.Should().Be(200m);
            client.CashBalance.Should().Be(300m); // 500 - 200

            _orderRepoMock.Verify(r => r.AddAsync(It.IsAny<StockOrder>()), Times.Once);
            _clientRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task SellStockAsync_Should_Create_Order_And_Update_Cash_And_Remove_Position_If_Zero()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var stockId = Guid.NewGuid();
            var stock = new Stock(stockId, "AAPL", "Apple", 50m);
            var client = new Client(clientId, 100m, "Alice", "user@vlk.com");
            client.BuyStock(stock, 2); // cash 100 - 200? let's adjust initial cash

            _clientRepoMock.Setup(r => r.GetWithPositionsAsync(clientId)).ReturnsAsync(client);
            _stockRepoMock.Setup(r => r.GetByIdAsync(stockId)).ReturnsAsync(stock);

            var service = CreateService();

            // Act
            var result = await service.SellStockAsync(clientId, stockId, 2);

            // Assert
            result.Type.Should().Be(OrderType.Sell.ToString());
            client.Positions.Should().BeEmpty();
            client.CashBalance.Should().Be(100m); // initial + sell proceeds = 300

            _orderRepoMock.Verify(r => r.AddAsync(It.IsAny<StockOrder>()), Times.Once);
            _clientRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
