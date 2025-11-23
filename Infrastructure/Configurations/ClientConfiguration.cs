using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.CashBalance)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder
                .HasMany<ClientStockPosition>(x=> x.Positions)
                .WithOne()
                .HasForeignKey(x => x.ClientId);
        }
    }
}
