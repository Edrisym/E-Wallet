public class WalletDbContext(DbContextOptions<WalletDbContext> options) : DbContext(options)
{
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Currency> Currencies { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>(builder =>
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
                .HasMaxLength(10) //BASEDGODS,HOTDOGE
                .IsUnicode(false);

            builder.Property(x => x.Name) // BTC,USD,EUR
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(false);

            builder.HasIndex(c => c.Code)
                .HasDatabaseName("IX_Currencies_Code")
                .IsClustered(false);
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.ToTable(SchemaTable.Wallets);

            entity.HasKey(w => w.Id);

            entity.Property(w => w.Balance)
                .IsRequired()
                .HasColumnType(DefaultColumnType.Decimal);

            entity.Property(w => w.StatusId)
                .IsRequired();

            entity.Property(w => w.CreatedOnUtc)
                .IsRequired();

            entity.Property(w => w.ModifiedOnUtc)
                .IsRequired(false);

            entity.HasOne(w => w.Currency)
                .WithMany()
                .HasForeignKey(w => w.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(x => x.CurrencyId)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                    value => CurrencyId.From(value))
                .HasColumnType(DefaultColumnType.Nvarchar36);

            entity.HasIndex(w => w.StatusId)
                .HasDatabaseName("IX_Wallets_StatusId")
                .IsClustered(false);
        });
    }
}