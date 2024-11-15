namespace EWallet.Api.Common.Models;

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

    public static Currency Create(string code, string name, decimal ratio)
    {
        if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException();
        }

        if (decimal.IsNegative(ratio))
        {
            throw new ArgumentOutOfRangeException();
        }

        return new Currency
        {
            Id = Guid.NewGuid(),
            Code = code,
            Name = name,
            Ratio = ratio,
            CreatedOnUtc = DateTime.UtcNow
        };
    }
}