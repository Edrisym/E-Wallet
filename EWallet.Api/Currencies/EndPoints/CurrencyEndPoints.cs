namespace EWallet.Api.Currencies.EndPoints;

public static class CurrencyEndPoints
{
    public static void MapCurrencyEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("/api/v1/currency");


        group.MapPost("/", async ([FromBody] CurrencyDto request, WalletDbContext context) =>
        {
            try
            {
                var currency = Currency.Create(request.Code, request.Name, request.Ratio);
                await context.AddRangeAsync(currency);
                await context.SaveChangesAsync();

                return Results.Created();
            }
            catch (Exception e)
            {
                if (e.InnerException!.Message.Contains("duplicate"))
                    return Results.Conflict(ErrorMessages.DuplicateInput);
                return Results.BadRequest(StatusCodes.Status500InternalServerError);
            }
        }).AllowAnonymous();
    }
}