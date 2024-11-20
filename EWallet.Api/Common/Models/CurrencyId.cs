namespace EWallet.Api.Common.Models;

public class CurrencyId(Guid value)
{
    public Guid Value { get; set; } = value;

    /// <summary>
    /// Generates a new unique CurrencyId.
    /// </summary>
    /// <returns>A new CurrencyId instance with a unique GUID.</returns>
    public static CurrencyId NewId() => new CurrencyId(Guid.NewGuid());

    /// <summary>
    /// Creates a CurrencyId from an existing GUID.
    /// </summary>
    /// <param name="value">The GUID value.</param>
    /// <returns>A CurrencyId instance.</returns>
    public static CurrencyId From(Guid value) => new CurrencyId(value);

    /// <summary>
    /// Overrides ToString to return the GUID as a string.
    /// </summary>
    public override string ToString() => Value.ToString();
}