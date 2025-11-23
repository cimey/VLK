using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class StockHistoryConfiguration : IEntityTypeConfiguration<StockHistory>
    {
        public void Configure(EntityTypeBuilder<StockHistory> builder)
        {
            builder.ToTable("StockHistories");

            builder.HasKey(sh => sh.Id);

            builder.Property(sh => sh.Id)
            .ValueGeneratedOnAdd();

            builder.Property(sh => sh.Price)
                            .HasColumnType("decimal(18,2)")
                            .IsRequired();

            builder.Property(sh => sh.EffectiveDateStart)
                .IsRequired();

            builder.Property(sh => sh.EffectiveDateEnd)
                .IsRequired(false);

            builder.HasOne<Stock>()
                .WithMany(s => s.History)
                .HasForeignKey(sh => sh.StockId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
