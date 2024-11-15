namespace EWallet.Api.Common.Exceptions;

public class InvalidCurrencyRatioException(decimal ratio) : Exception(string.Format(Message, ratio))
{
    private new const string Message = "The currency ratio {0} is invalid. It must be positive and equal to 1.0.";

    [DoesNotReturn]
    public static void Throw(decimal balance)
    {
        throw new InvalidCurrencyRatioException(balance);
    }
}