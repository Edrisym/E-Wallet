var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCustomAuthentication(builder.Configuration)
    .AddCustomAuthorization();

builder.AddWalletDbContext();

var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();


app.MapWalletsEndpoints();
app.MapFallback(() => Results.NotFound("Route not found"));

app.Run();