using Application.Clients;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTest.Application
{
    public class ClientServiceTests
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly Mock<ILogger<ClientService>> _loggerMock;
        private readonly ClientService _service;

        public ClientServiceTests()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _loggerMock = new Mock<ILogger<ClientService>>();
            _service = new ClientService(_clientRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetClientsAsync_ShouldReturnMappedClientDtos()
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client(Guid.NewGuid(), 1000m, "John","john@test.com")  ,
                new Client(Guid.NewGuid(), 500m, "Mary", "mary@test.com")
            };

            _clientRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(clients);

            // Act
            var result = await _service.GetClientsAsync();

            // Assert
            result.Should().HaveCount(2);
            result.First().Email.Should().Be("john@test.com");
            result.Last().Name.Should().Be("Mary");
        }

        [Fact]
        public async Task GetClientStatusAsync_ShouldReturnClientStatus()
        {
            // Arrange
            var stockId = Guid.NewGuid();

            var stock = new Stock(stockId, "AAPL", "Apple", 150);
            var client = new Client(Guid.NewGuid(), 1000m, "John", "john@test.com");


            client.BuyStock(stock, 2);
         

            _clientRepositoryMock
                .Setup(r => r.GetWithPositionsAsync(client.Id))
                .ReturnsAsync(client);

            // Act
            var result = await _service.GetClientStatusAsync(client.Id);

            // Assert
            result.ClientId.Should().Be(client.Id);
            result.CashBalance.Should().Be(1000m - stock.CurrentPrice * 2);
            result.StockPositions.Should().HaveCount(1);

            var pos = result.StockPositions.First();
            pos.StockId.Should().Be(stockId);
            pos.Shares.Should().Be(2);
        }

        [Fact]
        public async Task GetClientStatusAsync_ShouldThrow_WhenClientDoesNotExist()
        {
            // Arrange
            var clientId = Guid.NewGuid();

            _clientRepositoryMock
                .Setup(r => r.GetWithPositionsAsync(clientId))
                .ReturnsAsync((Client?)null);

            // Act
            var action = async () => await _service.GetClientStatusAsync(clientId);

            // Assert
            await action.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Client not found.");
        }
    }
}
