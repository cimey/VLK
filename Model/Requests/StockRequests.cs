namespace Model.Requests
{
    public class BuyStockRequest
    {
        public Guid ClientId { get; set; }
        public Guid StockId { get; set; }
        public int Shares { get; set; }
    }

    public class SellStockRequest
    {
        public Guid ClientId { get; set; }
        public Guid StockId { get; set; }
        public int Shares { get; set; }
    }



    public class UpdateStockRequest
    {
        public Guid StockId { get; set; }
        public decimal Price { get; set; }
    }
}
