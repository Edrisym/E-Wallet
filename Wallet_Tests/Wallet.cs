using Wallet_Tests.Exceptions;
using Xunit.Sdk;

namespace Wallet_Tests;

public class Wallet
{
    private Wallet()
    {
    }

    public Guid Id { get; private set; }
    public decimal Balance { get; private set; }

    public Currency Currency { get; private set; }

    public static Wallet Create(decimal balance, Currency currency)
    {
        if (decimal.IsNegative(balance)) NegativeBalanceException.Throw(balance);
        ArgumentNullException.ThrowIfNull(currency);
        if (!IsValidRatio(currency)) InvalidCurrencyRatioException.Throw(currency.Ratio);
        
        return new Wallet
        {
            Id = Guid.NewGuid(),
            Balance = balance,
            Currency = currency,
        };
    }


    //public void Deposit(decimal amount)
    // {
    //     //TODO
    //     if (decimal.IsNegative(balance))
    //     {
    //         NegativeBalanceException.Throw(balance);
    //     }
    //
    //     Balance += amount;
    // }

    public void Withdraw(decimal amount)
    {
        if (Balance < amount)
        {
            InsufficientFundsException.Throw(amount);
        }

        Balance -= amount;
    }

    private static bool IsValidRatio(Currency currency)
    {
        return decimal.IsPositive(currency.Ratio) && decimal.Equals(currency.Ratio, 1.0m);
    }
}