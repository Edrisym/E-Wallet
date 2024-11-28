using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EWallet.Api.Data;

public class WalletConfigurations : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable(SchemaTable.Wallets);

        builder.HasKey(w => w.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value,
                value => WalletId.From(value))
            .HasColumnType(DefaultColumnType.Nvarchar36);

        builder.Property(w => w.Balance)
            .IsRequired()
            .HasColumnType(DefaultColumnType.Decimal);

        builder.Property(w => w.StatusId)
            .IsRequired();

        builder.Property(w => w.CreatedOnUtc)
            .IsRequired();

        builder.Property(w => w.ModifiedOnUtc)
            .IsRequired(false);

        builder.HasOne(w => w.Currency)
            .WithMany()
            .HasForeignKey(w => w.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.CurrencyId)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value,
                value => CurrencyId.From(value))
            .HasColumnType(DefaultColumnType.Nvarchar36);

        builder.HasIndex(w => w.StatusId)
            .HasDatabaseName("IX_Wallets_StatusId")
            .IsClustered(false);
    }
}