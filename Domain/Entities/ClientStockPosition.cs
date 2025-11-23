namespace Domain.Entities
{
    public class ClientStockPosition
    {
        private ClientStockPosition() { }

        public ClientStockPosition(Guid stockId, int quantity, Guid clientId)
        {
            StockId = stockId;
            Quantity = quantity;
            ClientId = clientId;
        }

        public Guid StockId { get; private set; }
        public Stock Stock { get; private set; }
        public Guid ClientId { get; private set; }
        public int Quantity { get; private set; }

        public void Add(int qty)
        {
            if (qty <= 0) throw new InvalidOperationException("Quantity must be positive.");
            Quantity += qty;
        }

        public void Subtract(int qty)
        {
            if (qty <= 0) throw new InvalidOperationException("Quantity must be positive.");
            if (Quantity < qty) throw new InvalidOperationException("Not enough shares to sell.");
            Quantity -= qty;
        }
    }

}
