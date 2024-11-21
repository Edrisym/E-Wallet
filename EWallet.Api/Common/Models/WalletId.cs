namespace EWallet.Api.Common.Models;

public class WalletId(Guid value)
{
    public Guid Value { get; set; } = value;

    /// <summary>
    /// Generates a new unique CurrencyId.
    /// </summary>
    /// <returns>A new CurrencyId instance with a unique GUID.</returns>
    public static WalletId NewId() => new WalletId(Guid.NewGuid());

    /// <summary>
    /// Creates a CurrencyId from an existing GUID.
    /// </summary>
    /// <param name="value">The GUID value.</param>
    /// <returns>A CurrencyId instance.</returns>
    public static WalletId From(Guid value) => new WalletId(value);

    /// <summary>
    /// Overrides ToString to return the GUID as a string.
    /// </summary>
    public override string ToString() => Value.ToString();
}