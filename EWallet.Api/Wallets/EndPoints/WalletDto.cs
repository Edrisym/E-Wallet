namespace EWallet.Api.Wallets.EndPoints;

public record WalletDto(
    [Required] decimal Balance,
    [Required] Guid CurrencyId);