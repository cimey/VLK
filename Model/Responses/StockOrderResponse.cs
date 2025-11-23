using Domain.Entities;

namespace Model.Responses
{
    public class StockOrderResponse
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid StockId { get; set; }
        public string Type { get; set; } = default!;
        public int Shares { get; set; }
        public decimal PricePerShare { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }

        public static StockOrderResponse FromDomain(StockOrder order)
        {
            return new StockOrderResponse
            {
                Id = order.Id,
                ClientId = order.ClientId,
                StockId = order.StockId,
                Type = order.Type.ToString(),
                Shares = order.Shares,
                PricePerShare = order.PricePerShare,
                TotalPrice = order.TotalPrice,
                CreatedAt = order.CreatedAt
            };
        }
    }

    public class StockResponse
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; } = default!;
        public string Name { get; set; } = default!;
        public decimal CurrentPrice { get; set; }

        public static StockResponse FromDomain(Domain.Entities.Stock stock)
        {
            return new StockResponse
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                Name = stock.Name,
                CurrentPrice = stock.CurrentPrice
            };
        }
    }
}
