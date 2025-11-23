using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class StockOrderConfiguration : IEntityTypeConfiguration<StockOrder>
    {
        public void Configure(EntityTypeBuilder<StockOrder> builder)
        {
            builder.ToTable("StockOrders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ClientId)
                .IsRequired();

            builder.Property(x => x.StockId)
                .IsRequired();

            builder.Property(x => x.Type)
                .HasConversion<int>() // store enum as int
                .IsRequired();

            builder.Property(x => x.Shares)
                .IsRequired();

            builder.Property(x => x.PricePerShare)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Ignore(x => x.TotalPrice); // computed property, not stored in DB

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.ExecutedAt)
                .IsRequired(false);
        }
    }
}
