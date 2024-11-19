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
    public int StatusId { get; private set; }

    public CurrencyId CurrencyId { get; set; }
    public Currency Currency { get; private set; }
    public DateTime ModifiedOnUtc { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    private static readonly Dictionary<WalletStatus, List<WalletStatus>> StatusTransitions = new()
    {
        { WalletStatus.UnderReview, new List<WalletStatus> { WalletStatus.PendingActivation, WalletStatus.Closed } },
        { WalletStatus.PendingActivation, new List<WalletStatus> { WalletStatus.Active, WalletStatus.Closed } },
        { WalletStatus.Active, new List<WalletStatus> { WalletStatus.Suspended, WalletStatus.Closed } },
        { WalletStatus.Suspended, new List<WalletStatus> { WalletStatus.Active, WalletStatus.Closed } },
        { WalletStatus.Closed, new List<WalletStatus>() }
    };

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
            StatusId = (int)WalletStatus.UnderReview,
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


    private Wallet SwitchStatus(WalletStatus newStatus, Action<Wallet>? operation = null)
    {
        var currentStatus = Enum.Parse<WalletStatus>(Status);

        if (!StatusTransitions.TryGetValue(currentStatus, out var allowedTransitions) ||
            !allowedTransitions.Contains(newStatus))
        {
            throw new InvalidOperationException($"Cannot transition from {currentStatus} to {newStatus}.");
        }

        Status = newStatus.ToString();
        StatusId = (int)newStatus;
        ModifiedOnUtc = DateTime.UtcNow;

        operation?.Invoke(this);

        return this;
    }

    public Wallet Activate()
    {
        return SwitchStatus(WalletStatus.Active);
    }

    public Wallet PendActivation()
    {
        return SwitchStatus(WalletStatus.PendingActivation);
    }

    public Wallet Suspend()
    {
        return SwitchStatus(WalletStatus.Suspended);
    }

    public Wallet Close()
    {
        return SwitchStatus(WalletStatus.Closed);
    }
}