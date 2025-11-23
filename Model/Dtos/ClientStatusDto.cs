namespace Model.Dtos
{
    public class ClientStatusDto : ClientDto
    {
        public decimal CashBalance { get; set; }
        public List<StockPositionDto> StockPositions { get; set; } = new();
    }

    public class StockPositionDto
    {
        public Guid StockId { get; set; }

        public int Shares { get; set; }

        public decimal StockPrice { get; set; }
        public string StockName { get; set; }

        public decimal TotalValue => StockPrice * Shares;
    }

    public class ClientDto
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
