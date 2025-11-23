namespace Domain.Entities
{
    public enum OrderType
    {
        Buy,
        Sell
    }

    public class StockOrder
    {
        private StockOrder() { }

        public StockOrder(Guid clientId, Guid stockId, OrderType type, int shares, decimal pricePerShare)
        {
            if (shares <= 0)
                throw new ArgumentException("Shares must be greater than zero.", nameof(shares));

            Id = Guid.NewGuid();
            ClientId = clientId;
            StockId = stockId;
            Type = type;
            Shares = shares;
            PricePerShare = pricePerShare;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }

        public Guid ClientId { get; private set; }
        public Guid StockId { get; private set; }

        public OrderType Type { get; private set; }

        public int Shares { get; private set; }
        public decimal PricePerShare { get; private set; }
        public decimal TotalPrice => Shares * PricePerShare;

        public DateTime CreatedAt { get; private set; }
        public DateTime? ExecutedAt { get; set; } 
    }
}
