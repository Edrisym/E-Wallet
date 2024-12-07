namespace EWallet.Api.Common.Models;

public class Transaction
{
    private Transaction()
    {
    }

    public static Transaction Create(
        CurrencyId currencyId,
        WalletId senderWalletId,
        WalletId receiverWalletId,
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
    public CurrencyId CurrencyId { get; private set; }
    public WalletId SenderWalletId { get; private set; }
    public WalletId ReceiverWalletId { get; private set; }
    public decimal Amount { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }
}