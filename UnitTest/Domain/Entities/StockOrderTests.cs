using Domain.Entities;
using FluentAssertions;

namespace UnitTest.Domain.Entities
{
    public class StockOrderTests
    {
        [Fact]
        public void Constructor_Should_Create_BuyOrder_Correctly()
        {
            var clientId = Guid.NewGuid();
            var stockId = Guid.NewGuid();
            int shares = 5;
            decimal pricePerShare = 100m;

            var order = new StockOrder(clientId, stockId, OrderType.Buy, shares, pricePerShare);

            order.Id.Should().NotBe(Guid.Empty);
            order.ClientId.Should().Be(clientId);
            order.StockId.Should().Be(stockId);
            order.Type.Should().Be(OrderType.Buy);
            order.Shares.Should().Be(shares);
            order.PricePerShare.Should().Be(pricePerShare);
            order.TotalPrice.Should().Be(shares * pricePerShare);
            order.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            order.ExecutedAt.Should().BeNull();
        }

        [Fact]
        public void Constructor_Should_Create_SellOrder_Correctly()
        {
            var clientId = Guid.NewGuid();
            var stockId = Guid.NewGuid();
            int shares = 3;
            decimal pricePerShare = 150m;

            var order = new StockOrder(clientId, stockId, OrderType.Sell, shares, pricePerShare);

            order.Type.Should().Be(OrderType.Sell);
            order.TotalPrice.Should().Be(3 * 150m);
        }

        [Fact]
        public void Constructor_Should_Throw_When_Shares_Is_Zero()
        {
            var clientId = Guid.NewGuid();
            var stockId = Guid.NewGuid();

            Action act = () => new StockOrder(clientId, stockId, OrderType.Buy, 0, 100m);

            act.Should().Throw<ArgumentException>()
                .WithMessage("Shares must be greater than zero.*")
                .And.ParamName.Should().Be("shares");
        }

        [Fact]
        public void Constructor_Should_Throw_When_Shares_Is_Negative()
        {
            var clientId = Guid.NewGuid();
            var stockId = Guid.NewGuid();

            Action act = () => new StockOrder(clientId, stockId, OrderType.Sell, -5, 100m);

            act.Should().Throw<ArgumentException>()
                .WithMessage("Shares must be greater than zero.*")
                .And.ParamName.Should().Be("shares");
        }

        [Fact]
        public void ExecutedAt_Can_Be_Set_After_Creation()
        {
            var order = new StockOrder(Guid.NewGuid(), Guid.NewGuid(), OrderType.Buy, 2, 50m);

            order.ExecutedAt.Should().BeNull();

            var executedTime = DateTime.UtcNow;
            order.ExecutedAt = executedTime;

            order.ExecutedAt.Should().Be(executedTime);
        }
    }
}
