using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EWallet.Api.Data;

public class CurrencyConfigurations : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable(SchemaTable.Currencies);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value,
                value => CurrencyId.From(value))
            .HasColumnType(DefaultColumnType.Nvarchar36);

        builder.Property(x => x.Ratio)
            .IsRequired()
            .HasColumnType(DefaultColumnType.Decimal);

        builder.Property(x => x.CreatedOnUtc)
            .IsRequired();

        builder.Property(x => x.ModifiedOnUtc)
            .IsRequired(false);

        builder.Property(x => x.Code)
            .HasMaxLength(10) //  BASEDGODS,HOTDOGE -- BTC,USD,EUR
            .IsUnicode(false);

        builder.Property(x => x.Name) // Dollar,Euro,Lira
            .HasMaxLength(100)
            .IsRequired()
            .IsUnicode(false);

        builder.HasIndex(c => c.Code)
            .HasDatabaseName("IX_Currencies_Code")
            .IsClustered(false);

        builder.HasIndex(e => new { e.Code, e.Name })
            .IsUnique();
    }
}