using Domain.Entities;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SeedData
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(VLKContext context)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Fixed GUIDs for stable seed
            var clientId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var appleStockId = Guid.Parse("00000000-0000-0000-0000-000000000101");
            var microsoftStockId = Guid.Parse("00000000-0000-0000-0000-000000000102");
            var teslaStockId = Guid.Parse("00000000-0000-0000-0000-000000000103");

            if (!await context.Clients.AnyAsync(c => c.Id == clientId))
            {
                context.Clients.Add(new Client(clientId, 10000m, "Alice", "user@vlk.com"));
            }
            // Seed Client

            // Seed Stocks
            if (!await context.Stocks.AnyAsync())
            {
                context.Stocks.AddRange(
                    new Stock(appleStockId, "AAPL", "Apple Inc.", 150m),
                    new Stock(microsoftStockId, "MSFT", "Microsoft Corp.", 280m),
                    new Stock(teslaStockId, "TSLA", "Tesla Inc.", 700m)
                );
            }

            await context.SaveChangesAsync();
        }
    }

}
