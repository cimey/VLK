using Domain.Entities;
using FluentAssertions;

namespace UnitTest.Domain.Entities
{
    public class StockTests
    {
        [Fact]
        public void Constructor_Should_Initialize_Correctly()
        {
            var id = Guid.NewGuid();
            var stock = new Stock(id, "AAPL", "Apple Inc.", 150m);

            stock.Id.Should().Be(id);
            stock.Symbol.Should().Be("AAPL");
            stock.Name.Should().Be("Apple Inc.");
            stock.CurrentPrice.Should().Be(150m);
        }

        [Fact]
        public void UpdatePrice_Should_Update_CurrentPrice()
        {
            var stock = new Stock(Guid.NewGuid(), "AAPL", "Apple Inc.", 150m);

            stock.UpdatePrice(200m);

            stock.CurrentPrice.Should().Be(200m);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void UpdatePrice_Should_Throw_When_NonPositive(decimal newPrice)
        {
            var stock = new Stock(Guid.NewGuid(), "AAPL", "Apple Inc.", 150m);

            Action act = () => stock.UpdatePrice(newPrice);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Price must be positive.");
        }
    }
}
