

namespace EWallet.Api.Common.Wallets.EndPoints;

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
                var wallet = Wallet.Create(request.Balance, currencyId);
                await context.AddAsync(wallet);
                await context.SaveChangesAsync();

                return Results.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Results.BadRequest(StatusCodes.Status500InternalServerError);
            }
        }).AllowAnonymous();
    }
}