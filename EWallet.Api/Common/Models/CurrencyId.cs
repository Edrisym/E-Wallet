namespace EWallet.Api.Common.Models;

public class CurrencyId
{
    private Guid Value { get; set; }

    private CurrencyId(Guid value)
    {
        Value = value;
    }

    /// <summary>
    /// Generates a new unique CurrencyId.
    /// </summary>
    /// <returns>A new CurrencyId instance with a unique GUID.</returns>
    public static CurrencyId NewId()
    {
        return new CurrencyId(Guid.NewGuid());
    }

    /// <summary>
    /// Creates a CurrencyId from an existing GUID.
    /// </summary>
    /// <param name="value">The GUID value.</param>
    /// <returns>A CurrencyId instance.</returns>
    public static CurrencyId From(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("CurrencyId cannot be an empty GUID.", nameof(value));

        return new CurrencyId(value);
    }

    /// <summary>
    /// Overrides ToString to return the GUID as a string.
    /// </summary>
    public override string ToString()
    {
        return Value.ToString();
    }
}