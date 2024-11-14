
namespace E_Wallet.Models;

public class Wallet
{
    private Wallet()
    {
    }

    private const decimal InitialBalance = 1.0m;
    public Guid Id { get; private set; }
    public decimal Balance { get; private set; }

    public Currency Currency { get; private set; }

    public static Wallet Create(decimal balance, Currency currency)
    {
        if (decimal.IsNegative(balance))
        {
            NegativeBalanceException.Throw(balance);
        }
        else if (InitialBalance > balance)
        {
            InsufficientInitialBalanceException.Throw(balance);
        }

        ArgumentNullException.ThrowIfNull(currency);
        if (!IsValidRatio(currency)) InvalidCurrencyRatioException.Throw(currency.Ratio);

        return new Wallet
        {
            Id = Guid.NewGuid(),
            Balance = balance,
            Currency = currency,
        };
    }


    public void Deposit(decimal amount)
    {
        // TODO -- ***is it ok to check on zero values**
        if (decimal.IsNegative(amount) || amount == decimal.Zero)
        {
            NegativeBalanceException.Throw(amount);
        }

        Balance += amount;
    }

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
        return decimal.IsPositive(currency.Ratio) && currency.Ratio > 1.0m;
    }
}