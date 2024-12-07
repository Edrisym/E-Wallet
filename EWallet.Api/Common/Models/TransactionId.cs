namespace EWallet.Api.Common.Models;

public class TransactionId(Guid value)
{
    public Guid Value { get; set; } = value;

    /// <summary>
    /// Generates a new unique TransactionId.
    /// </summary>
    /// <returns>A new TransactionId instance with a unique GUID.</returns>
    public static TransactionId NewId() => new TransactionId(Guid.NewGuid());

    /// <summary>
    /// Creates a TransactionId from an existing GUID.
    /// </summary>
    /// <param name="value">The GUID value.</param>
    /// <returns>A TransactionId instance.</returns>
    public static TransactionId From(Guid value) => new TransactionId(value);

    /// <summary>
    /// Overrides ToString to return the GUID as a string.
    /// </summary>
    public override string ToString() => Value.ToString();
}