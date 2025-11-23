namespace Domain.Entities
{
    public class Client
    {


        private Client() { }

        public Client(Guid id, decimal cashBalance, string name, string email)
        {
            Id = id;
            CashBalance = cashBalance;
            Name = name;
            Email = email;
        }

        public Guid Id { get; private set; }
        public decimal CashBalance { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public List<ClientStockPosition> Positions { get; private set; } = new();

        public StockOrder BuyStock(Stock stock, int quantity)
        {
            if (quantity <= 0)
                throw new InvalidOperationException("Quantity must be positive.");

            var cost = stock.CurrentPrice * quantity;

            if (cost > CashBalance)
                throw new InvalidOperationException("Insufficient cash balance.");

            // Deduct cash
            CashBalance -= cost;

            // Update stock position
            var existing = Positions.FirstOrDefault(p => p.StockId == stock.Id);
            if (existing == null)
                Positions.Add(new ClientStockPosition(stock.Id, quantity, Id));
            else
                existing.Add(quantity);

            // Create and return StockOrder
            var order = new StockOrder(Id, stock.Id, OrderType.Buy, quantity, stock.CurrentPrice);
            order.ExecutedAt = DateTime.UtcNow; // mark as executed immediately
            return order;
        }

        public StockOrder SellStock(Stock stock, int quantity)
        {
            if (quantity <= 0)
                throw new InvalidOperationException("Quantity must be positive.");

            var position = Positions.FirstOrDefault(p => p.StockId == stock.Id)
                           ?? throw new InvalidOperationException("Client does not own this stock.");

            if (position.Quantity < quantity)
                throw new InvalidOperationException("Not enough shares to sell.");

            // Update stock position
            position.Subtract(quantity);

            // Remove position if shares become 0
            if (position.Quantity == 0)
                Positions.Remove(position);

            // Add cash from selling
            var profit = stock.CurrentPrice * quantity;
            CashBalance += profit;

            // Create and return StockOrder
            var order = new StockOrder(Id, stock.Id, OrderType.Sell, quantity, stock.CurrentPrice);
            order.ExecutedAt = DateTime.UtcNow; // mark as executed immediately
            return order;
        }

        public ClientStockPosition? GetPosition(Guid stockId) =>
            Positions.FirstOrDefault(x => x.StockId == stockId);
    }
}
