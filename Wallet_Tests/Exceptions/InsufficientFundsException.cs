namespace Wallet_Tests;

public class InsufficientFundsException(decimal amount) : Exception(string.Format(Message, amount))
{
    private new const string Message =
        "Insufficient funds. You requested a withdrawal of {0}, but the balance is insufficient.";


    public static void Throw(decimal amount)
    {
        throw new InsufficientFundsException(amount);
    }
}