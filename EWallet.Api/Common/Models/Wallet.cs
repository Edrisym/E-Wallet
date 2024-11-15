using EWallet.Api.Common.Exceptions;

namespace EWallet.Api.Common.Models;

public class Wallet
{
    private Wallet()
    {
    }

    private const decimal InitialBalance = 1.0m;
    public Guid Id { get; private set; }
    public decimal Balance { get; private set; }
    public string Status { get; private set; }

    public CurrencyId CurrencyId { get; set; }
    public Currency Currency { get; private set; }
    public DateTime ModifiedOnUtc { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }


    public static Wallet Create(decimal balance, Currency currency)
    {
        if (decimal.IsNegative(balance))
            NegativeBalanceException.Throw(balance);
        else if (InitialBalance > balance)
            InsufficientInitialBalanceException.Throw(balance);

        ArgumentNullException.ThrowIfNull(currency);
        if (!IsValidRatio(currency))
            InvalidCurrencyRatioException.Throw(currency.Ratio);

        return new Wallet
        {
            Id = Guid.NewGuid(),
            Balance = balance,
            Currency = currency,
            CurrencyId = currency.Id,
            Status = WalletStatus.UnderReview.ToString(),
            CreatedOnUtc = DateTime.UtcNow,
        };
    }


    public void Deposit(decimal amount)
    {
        // TODO -- ***is it ok to check on zero values**
        if (decimal.IsNegative(amount) || amount == decimal.Zero)
            NegativeBalanceException.Throw(amount);
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (decimal.IsNegative(amount) || decimal.Equals(amount, decimal.Zero))
        {
            throw new InvalidDataException();
        }

        if (Balance < amount)
            InsufficientFundsException.Throw(amount);

        Balance -= amount;
    }

    private static bool IsValidRatio(Currency currency)
    {
        return decimal.IsPositive(currency.Ratio) && currency.Ratio > 1.0m;
    }


    // TODO -- come up with a better solution
    private static readonly Dictionary<WalletStatus, List<WalletStatus>> StatusTransitions = new()
    {
        { WalletStatus.UnderReview, new List<WalletStatus> { WalletStatus.PendingActivation } },
        { WalletStatus.PendingActivation, new List<WalletStatus> { WalletStatus.Verified } },
        { WalletStatus.Verified, new List<WalletStatus> { WalletStatus.Active } },
        { WalletStatus.Active, new List<WalletStatus> { WalletStatus.Suspended, WalletStatus.Locked } },
        { WalletStatus.Suspended, new List<WalletStatus> { WalletStatus.Closed, WalletStatus.Deactivated } },
        { WalletStatus.Closed, new List<WalletStatus> { WalletStatus.Deactivated } },
        { WalletStatus.Deactivated, new List<WalletStatus> { WalletStatus.Expired } },
        { WalletStatus.Expired, new List<WalletStatus> { WalletStatus.Closed } },
        { WalletStatus.PendingClosure, new List<WalletStatus> { WalletStatus.Closed } },
        { WalletStatus.Inactive, new List<WalletStatus> { WalletStatus.PendingActivation, WalletStatus.Suspended } },
        { WalletStatus.Frozen, new List<WalletStatus> { WalletStatus.Locked, WalletStatus.Suspended } }
    };


    // TODO no good
    public void ChangeStatus(WalletStatus newStatus)
    {
        var currentStatus = Enum.Parse<WalletStatus>(Status);


        if (StatusTransitions.TryGetValue(currentStatus, out var transition))
        {
            if (transition.Contains(newStatus))
            {
                Status = newStatus.ToString();
            }
            else
            {
                throw new InvalidOperationException(
                    $"Cannot change status from {currentStatus} to {newStatus.ToString()}. Invalid transition.");
            }
        }
        else
        {
            throw new InvalidOperationException(
                $"No valid transitions available from the current status: {currentStatus.ToString()}.");
        }
    }
}