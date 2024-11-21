namespace EWallet.Api.Common.Currencies.EndPoints;

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

                return Results.Ok(currency.Id);
            }
            catch (Exception e)
            {
                return e.InnerException!.Message.Contains("duplicate")
                    ? Results.Conflict(ErrorMessages.DuplicateInput)
                    : Results.BadRequest(StatusCodes.Status500InternalServerError);
            }
        }).AllowAnonymous();
    }
}