using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EWallet.Api.Data;

public class TransactionConfigurations : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(SchemaTable.Transactions);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value,
                value => TransactionId.From(value))
            .HasColumnType(DefaultColumnType.Nvarchar36);

        builder.Property(x => x.SenderWalletId)
            .HasMaxLength(36)

            .IsRequired();

        builder.Property(x => x.ReceiverWalletId)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasColumnType(DefaultColumnType.Decimal)
            .IsRequired();
    }
}