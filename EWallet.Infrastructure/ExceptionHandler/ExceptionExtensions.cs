namespace EWallet.Infrastructure.ExceptionHandler;

public static class ExceptionExtensions
{
    private const string ErrorCodeKey = "ErrorCode";

    /// <summary>
    /// Adds or updates the error code for the exception.
    /// </summary>
    /// <param name="exception">The exception to enrich.</param>
    /// <param name="errorCode">The error code to add. Defaults to "GenericError".</param>
    public static void AddErrorCode(this Exception exception, string errorCode = "GenericError")
    {
        if (exception == null)
        {
            throw new ArgumentNullException(nameof(exception), "Exception cannot be null.");
        }

        exception.Data[ErrorCodeKey] = errorCode;
    }

    /// <summary>
    /// Retrieves the error code from the exception, if it exists.
    /// </summary>
    /// <param name="exception">The exception to retrieve the error code from.</param>
    /// <returns>The error code, or null if none exists.</returns>
    public static string? GetErrorCode(this Exception exception)
    {
        if (exception == null)
        {
            throw new ArgumentNullException(nameof(exception), "Exception cannot be null.");
        }

        return exception.Data.Contains(ErrorCodeKey) ? exception.Data[ErrorCodeKey]?.ToString() : null;
    }
}