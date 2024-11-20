public class WalletDbContext(DbContextOptions<WalletDbContext> options) : DbContext(options)
{
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Currency> Currencies { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>(builder =>
        {
            builder.ToTable("Currencies");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                    value => CurrencyId.From(value))
                .HasColumnType("NVARCHAR(36)");


            builder.Property(x => x.CreatedOnUtc)
                .IsRequired();

            builder.Property(x => x.ModifiedOnUtc);

            builder.Property(x => x.Code)
                .HasMaxLength(10) //BASEDGODS,HOTDOGE
                .IsUnicode(false);

            builder.Property(x => x.Name) // BTC,USD,EUR
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasIndex(c => c.Code)
                .HasDatabaseName("IX_Currencies_Code")
                .IsClustered(false);
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.ToTable("Wallets");

            entity.HasKey(w => w.Id);

            entity.Property(w => w.Balance)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            entity.Property(w => w.StatusId)
                .IsRequired();

            entity.Property(w => w.CreatedOnUtc)
                .IsRequired();

            entity.Property(w => w.ModifiedOnUtc);

            entity.HasOne(w => w.Currency)
                .WithMany()
                .HasForeignKey(w => w.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(x => x.CurrencyId)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                    value => CurrencyId.From(value))
                .HasColumnType("NVARCHAR(36)");

            entity.HasIndex(w => w.StatusId)
                .HasDatabaseName("IX_Wallets_StatusId")
                .IsClustered(false);
        });
    }
}