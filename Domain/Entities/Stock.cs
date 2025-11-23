namespace Domain.Entities
{
    public class Stock
    {
        private Stock() { }

        public Stock(Guid id, string symbol, string name, decimal currentPrice)
        {
            Id = id;
            Symbol = symbol;
            Name = name;
            CurrentPrice = currentPrice;

            History.Add(new StockHistory(Id, currentPrice));
        }

        public Guid Id { get; private set; }
        public string Symbol { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public decimal CurrentPrice { get; private set; }

        public List<StockHistory> History { get; private set; } = new List<StockHistory>();

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0) throw new InvalidOperationException("Price must be positive.");

            var current = History.FirstOrDefault(x => x.EffectiveDateEnd == null);

            if (current != null)
            {
                current.UpdateEndDate();
            }
            History.Add(new StockHistory(Id, newPrice));
            CurrentPrice = newPrice;
        }
    }
}
