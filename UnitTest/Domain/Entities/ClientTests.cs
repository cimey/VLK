using Domain.Entities;
using FluentAssertions;

namespace UnitTest.Domain.Entities
{


    public class ClientTests
    {
        private readonly Stock _appleStock = new Stock(Guid.NewGuid(), "AAPL", "Apple Inc.", 150m);

        [Fact]
        public void BuyStock_Should_Add_New_Position_When_Not_Exist()
        {
            var client = new Client(Guid.NewGuid(), 1000m, "Alice", "user@vlk.com");

            var order = client.BuyStock(_appleStock, 2);

            client.Positions.Should().HaveCount(1);
            client.CashBalance.Should().Be(700m); // 1000 - (150*2)
            order.Type.Should().Be(OrderType.Buy);
            order.Shares.Should().Be(2);
        }

        [Fact]
        public void BuyStock_Should_Add_To_Existing_Position()
        {
            var client = new Client(Guid.NewGuid(), 1000m, "Alice", "user@vlk.com");

            client.BuyStock(_appleStock, 2);
            client.BuyStock(_appleStock, 3);

            client.Positions.Should().HaveCount(1);
            client.Positions[0].Quantity.Should().Be(5);
            client.CashBalance.Should().Be(1000 - 150 * 5);
        }

        [Fact]
        public void BuyStock_Should_Throw_When_Insufficient_Funds()
        {
            var client = new Client(Guid.NewGuid(), 100m, "Alice", "user@vlk.com");

            Action act = () => client.BuyStock(_appleStock, 2); // cost 300

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Insufficient cash balance.");
        }

        [Fact]
        public void SellStock_Should_Subtract_Quantity_And_Add_Cash()
        {
            var client = new Client(Guid.NewGuid(), 500m, "Alice", "user@vlk.com");

            client.BuyStock(_appleStock, 2); // cash 500-300 = 200
            var order = client.SellStock(_appleStock, 1);

            client.Positions[0].Quantity.Should().Be(1);
            client.CashBalance.Should().Be(200 + 150); // 350
            order.Type.Should().Be(OrderType.Sell);
            order.Shares.Should().Be(1);
        }

        [Fact]
        public void SellStock_Should_Remove_Position_When_Quantity_Becomes_Zero()
        {
            var client = new Client(Guid.NewGuid(), 500m, "Alice", "user@vlk.com");

            client.BuyStock(_appleStock, 2);
            client.SellStock(_appleStock, 2);

            client.Positions.Should().BeEmpty();
            client.CashBalance.Should().Be(500); // 500 - 300 + 300
        }

        [Fact]
        public void SellStock_Should_Throw_When_Not_Enough_Shares()
        {
            var client = new Client(Guid.NewGuid(), 500m, "Alice", "user@vlk.com");

            client.BuyStock(_appleStock, 1);

            Action act = () => client.SellStock(_appleStock, 2);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Not enough shares to sell.");
        }

        [Fact]
        public void GetPosition_Should_Return_Correct_Position()
        {
            var client = new Client(Guid.NewGuid(), 1000m, "Alice", "user@vlk.com");
            client.BuyStock(_appleStock, 3);

            var position = client.GetPosition(_appleStock.Id);

            position.Should().NotBeNull();
            position!.Quantity.Should().Be(3);
        }

        [Fact]
        public void GetPosition_Should_Return_Null_If_Not_Owned()
        {
            var client = new Client(Guid.NewGuid(), 1000m, "Alice", "user@vlk.com");

            var position = client.GetPosition(Guid.NewGuid());

            position.Should().BeNull();
        }
    }
}
