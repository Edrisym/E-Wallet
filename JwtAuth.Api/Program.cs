var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<AuthService>();
var app = builder.Build();
app.UseHttpsRedirection();
app.MapPost("/login", ([FromBody] User? user, AuthService service) =>
{
    if (user != null)
    {
        return service.Create(user);
    }

    user = new User(
        1,
        "edris",
        "Ghafouri",
        "EdrisMoavenGhafouri@gmail.com",
        "q1w2e3r4t5",
        ["developer"]);
    return service.Create(user);
});

app.Run();


public class AuthService
{
    public string Create(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var privateKey = Encoding.UTF8.GetBytes(Configuration.PrivateKey);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(privateKey),
            SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = GenerateClaims(user)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(User user)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim("id", user.Id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Name, user.Username));
        ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

        foreach (var role in user.Roles)
            ci.AddClaim(new Claim(ClaimTypes.Role, role));

        return ci;
    }
}

public static class Configuration
{
    public static string PrivateKey => "bAafd@A7d9#@F4*V!LHZs#ebKQrkE6pad2f3kj34c3dXy@";
}

public record User(
    int Id,
    string Username,
    string Name,
    string Email,
    string Password,
    string[] Roles);