using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ClientStockPositionConfiguration : IEntityTypeConfiguration<ClientStockPosition>
    {
        public void Configure(EntityTypeBuilder<ClientStockPosition> builder)
        {
            builder.ToTable("ClientStockPositions");

            builder.HasKey(x => new { x.ClientId, x.StockId });

            builder.Property(x => x.Quantity)
                .IsRequired();
        }
    }
}
