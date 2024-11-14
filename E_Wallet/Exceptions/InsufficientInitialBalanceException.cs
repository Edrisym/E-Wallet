namespace E_Wallet.Exceptions;

public class InsufficientInitialBalanceException(decimal balance) :
    Exception(string.Format(Message, balance))
{
    private new const string Message =
        "Insufficient balance {0}. Initial balance is insufficient. Please meet the required minimum.";

    [DoesNotReturn]
    public static void Throw(decimal balance)
    {
        throw new InsufficientInitialBalanceException(balance);
    }
}