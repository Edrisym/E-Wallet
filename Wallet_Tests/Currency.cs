namespace Wallet_Tests;

public class Currency
{
    private Currency()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public decimal Ratio { get; private set; }

    public DateTime ModifiedOnUtc { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    public static Currency Create(string code, string name, decimal ratio) => new()
    {
        Id = Guid.NewGuid(),
        Code = code,
        Name = name,
        Ratio = ratio,
        CreatedOnUtc = DateTime.UtcNow
    };
}