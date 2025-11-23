using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Symbol)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.CurrentPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

          builder
          .HasMany<StockHistory>(x => x.History)
          .WithOne()
          .HasForeignKey(x => x.StockId);
        }
    }
}
