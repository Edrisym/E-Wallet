namespace EWallet.Api.Models;

public enum WalletStatus
{
    /// <summary>
    /// The wallet is fully operational and can be used for all transactions.
    /// </summary>
    Active = 1,

    /// <summary>
    /// The wallet is temporarily suspended. No transactions can be made until resolved.
    /// </summary>
    Suspended = 2,

    /// <summary>
    /// The wallet is deactivated and cannot be used for any transactions.
    /// </summary>
    Deactivated = 3,

    /// <summary>
    /// The wallet is newly created and awaiting activation.
    /// </summary>
    PendingActivation = 4,

    /// <summary>
    /// The wallet is locked due to security concerns (e.g., failed login attempts).
    /// </summary>
    Locked = 5,

    /// <summary>
    /// The wallet is frozen due to legal or compliance reasons.
    /// </summary>
    Frozen = 6,

    /// <summary>
    /// The wallet has been closed permanently. No further transactions are allowed.
    /// </summary>
    Closed = 7,

    /// <summary>
    /// The wallet has expired after a specific period, making it unusable.
    /// </summary>
    Expired = 8,

    /// <summary>
    /// The wallet is inactive and cannot perform any transactions.
    /// </summary>
    Inactive = 9,

    /// <summary>
    /// The wallet is under review by administrators or the system for any reason.
    /// </summary>
    UnderReview = 10,

    /// <summary>
    /// The wallet is in the process of being closed, and all actions are pending.
    /// </summary>
    PendingClosure = 11,

    /// <summary>
    /// The wallet has passed verification and can be used for full transactions.
    /// </summary>
    Verified = 12
}