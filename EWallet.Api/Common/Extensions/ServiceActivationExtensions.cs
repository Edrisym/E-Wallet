namespace EWallet.Api.Common.Extensions;

public static class ServiceActivationExtensions
{
    public static void AddWalletDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContextFactory<WalletDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration
                    .GetConnectionString(StaticData.ConnectionStrings.Default)));
    }
}