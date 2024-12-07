namespace EWallet.Api.Common.Models.User;

public class UserId(Guid value)
{
    public Guid Value { get; set; } = value;

    /// <summary>
    /// Generates a new unique CurrencyId.
    /// </summary>
    /// <returns>A new CurrencyId instance with a unique GUID.</returns>
    public static UserId NewId() => new UserId(Guid.NewGuid());

    /// <summary>
    /// Creates a CurrencyId from an existing GUID.
    /// </summary>
    /// <param name="value">The GUID value.</param>
    /// <returns>A CurrencyId instance.</returns>
    public static UserId From(Guid value) => new UserId(value);

    /// <summary>
    /// Overrides ToString to return the GUID as a string.
    /// </summary>
    public override string ToString() => Value.ToString();
}