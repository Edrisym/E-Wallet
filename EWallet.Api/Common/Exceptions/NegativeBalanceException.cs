namespace EWallet.Api.Common.Exceptions;

public class NegativeBalanceException : Exception
{
    private new const string Message = "The balance {0} is not valid. Balance must be a non-negative value.";

    private NegativeBalanceException(decimal balance) : base(string.Format(Message, balance))
    {
    }

    [DoesNotReturn]
    public static void Throw(decimal balance)
    {
        throw new NegativeBalanceException(balance);
    }
}