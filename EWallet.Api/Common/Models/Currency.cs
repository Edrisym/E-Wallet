namespace EWallet.Api.Common.Models;

public class Currency
{
    private Currency()
    {
    }

    public CurrencyId Id { get; private set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; private set; }
    
    [Required]
    [StringLength(10)]
    public string Code { get; private set; }

    [Required]
    public decimal Ratio { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    public static Currency Create(string code, string name, decimal ratio)
    {
        if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(name)) throw new ArgumentNullException();
        if (!IsValidRatio(ratio)) InvalidCurrencyRatioException.Throw(ratio);

        return new Currency
        {
            Id = CurrencyId.NewId(),
            Code = code,
            Name = name,
            Ratio = ratio,
            CreatedOnUtc = DateTime.UtcNow
        };
    }


    private static bool IsValidRatio(decimal ratio)
    {
        return decimal.IsPositive(ratio) && ratio >= 1.0m;
    }
}