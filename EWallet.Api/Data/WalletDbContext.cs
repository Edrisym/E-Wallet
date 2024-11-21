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
        });

        modelBuilder.Entity<Wallet>(builder =>
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
        });
    }
}