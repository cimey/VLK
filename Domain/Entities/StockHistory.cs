namespace Domain.Entities
{
    public class StockHistory
    {

        private StockHistory() { }

        public StockHistory(Guid stockId, decimal price)
        {
            Price = price;
            StockId = stockId;
            EffectiveDateStart = DateTimeOffset.UtcNow;
        }
        public int Id { get; private set; }

        public Guid StockId { get; private set; }

        public decimal Price { get; private set; }

        public DateTimeOffset EffectiveDateStart { get; private set; }

        public DateTimeOffset? EffectiveDateEnd { get; private set; }


        public void UpdateEndDate()
        {
            EffectiveDateEnd = DateTimeOffset.UtcNow;
        }
    }
}
