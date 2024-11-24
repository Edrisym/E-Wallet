namespace EWallet.Api.Wallets.EndPoints;

public record WalletDto(decimal Balance, Guid CurrencyId);