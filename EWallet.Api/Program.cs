using EWallet.Infrastructure.ExceptionHandler;
using Figgle;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine(FiggleFonts.Standard.Render("Wallet"));

builder.Services
    .AddCustomAuthentication(builder.Configuration)
    .AddCustomAuthorization()
    .AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.AddWalletDbContext();

var app = builder.Build();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();
app.MapWalletsEndpoints();
app.MapCurrencyEndpoints();

app.MapFallback(()
    => Results.NotFound("Route not found"));

app.Run();

public partial class Program
{
}