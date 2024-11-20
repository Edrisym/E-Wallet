

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCustomAuthentication(builder.Configuration)
    .AddCustomAuthorization();

builder.AddWalletDbContext();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();