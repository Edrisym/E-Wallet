var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCustomAuthentication(builder.Configuration)
    .AddCustomAuthorization();

builder.AddWalletDbContext();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", async ([FromBody] WalletDto request, WalletDbContext context) =>
{
    try
    {
        var wallet = Wallet.Create(request.Balance, request.CurrencyId);
        await context.AddAsync(wallet);
        await context.SaveChangesAsync();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }

    return Results.Ok();
});

app.Run();