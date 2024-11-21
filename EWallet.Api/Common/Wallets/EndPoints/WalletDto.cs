namespace EWallet.Api.Common.Wallets.EndPoints;

public record WalletDto(decimal Balance, Guid CurrencyId);