using Microsoft.AspNetCore.Authorization;

namespace EWallet.Api.Common.Wallet.Endpoints;

public static class WalletEndPoints
{
    public static void MapWalletsEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("/api/v1/wallets");

        group.MapPost("/", async ([FromBody] WalletDto request, WalletDbContext context) =>
        {
            try
            {
                var currencyId = CurrencyId.From(request.CurrencyId);
                var wallet = Models.Wallet.Create(request.Balance, currencyId);
                await context.AddAsync(wallet);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Results.Ok();
        }).AllowAnonymous();
    }
}