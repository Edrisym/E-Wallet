namespace EWallet.Api.Common.Models;

public class Transaction
{
    private Transaction()
    {
    }

    public static Transaction Create(
        string currencyId,
        string senderWalletId,
        string receiverWalletId,
        decimal amount)
    {
        return new Transaction
        {
            Id = TransactionId.NewId(),
            CurrencyId = currencyId,
            SenderWalletId = senderWalletId,
            ReceiverWalletId = receiverWalletId,
            Amount = amount,
            CreatedOnUtc = DateTime.UtcNow
        };
    }

    public TransactionId Id { get; private set; }
    public string CurrencyId { get; private set; }
    public string SenderWalletId { get; private set; }
    public string ReceiverWalletId { get; private set; }
    public decimal Amount { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }
}