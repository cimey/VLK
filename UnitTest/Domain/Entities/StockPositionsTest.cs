using Domain.Entities;
using FluentAssertions;

namespace UnitTest.Domain.Entities
{ 

    public class ClientStockPositionTests
    {
        [Fact]
        public void Add_Should_Increase_Quantity()
        {
            var position = new ClientStockPosition(Guid.NewGuid(), 5, Guid.NewGuid());

            position.Add(3);

            position.Quantity.Should().Be(8);
        }

        [Fact]
        public void Add_Should_Throw_When_Quantity_Is_Zero_Or_Negative()
        {
            var position = new ClientStockPosition(Guid.NewGuid(), 5, Guid.NewGuid());

            Action act1 = () => position.Add(0);
            Action act2 = () => position.Add(-2);

            act1.Should().Throw<InvalidOperationException>()
                .WithMessage("Quantity must be positive.");

            act2.Should().Throw<InvalidOperationException>()
                .WithMessage("Quantity must be positive.");
        }

        [Fact]
        public void Subtract_Should_Decrease_Quantity()
        {
            var position = new ClientStockPosition(Guid.NewGuid(), 5, Guid.NewGuid());

            position.Subtract(2);

            position.Quantity.Should().Be(3);
        }

        [Fact]
        public void Subtract_Should_Throw_When_Quantity_Is_Zero_Or_Negative()
        {
            var position = new ClientStockPosition(Guid.NewGuid(), 5, Guid.NewGuid());

            Action act1 = () => position.Subtract(0);
            Action act2 = () => position.Subtract(-1);

            act1.Should().Throw<InvalidOperationException>()
                .WithMessage("Quantity must be positive.");

            act2.Should().Throw<InvalidOperationException>()
                .WithMessage("Quantity must be positive.");
        }

        [Fact]
        public void Subtract_Should_Throw_When_Quantity_Exceeds_Current()
        {
            var position = new ClientStockPosition(Guid.NewGuid(), 5, Guid.NewGuid());

            Action act = () => position.Subtract(6);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Not enough shares to sell.");
        }
    }

}
